using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public EventSystem eventSystem;
    [HideInInspector] public GraphicRaycaster graphicRaycaster;
    [HideInInspector] public bool isMoveable = true;
    [HideInInspector] public InteractManager interactManager;

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        interactManager = FindObjectOfType<InteractManager>();
    }

    void Start()
    {
        eventSystem = EventSystem.current;
        graphicRaycaster = FindObjectOfType<GraphicRaycaster>();
        agent.updateRotation=false;
        agent.updateUpAxis=false;
        ResetPosition();
    }

    void Update()
    {
        Move();
    }
    
    public void Move(){
        if (isMoveable && Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            agent.SetDestination(mouseWorldPosition);
        }
    }
    
    public void ResetPosition(){
        agent.enabled = false;
        transform.position = Vector3.zero;
        agent.enabled = true;
        agent.SetDestination(Vector3.zero);
    }

    public void ToggleIsMoveable(bool _isMoveable){
        isMoveable = _isMoveable;
    }

    void OnTriggerEnter2D(Collider2D col){
        interactManager.SetInteractable(col.gameObject.GetComponent<InteractableItem>());
    }

    void OnTriggerExit2D(Collider2D col){
        interactManager.SetUninteractable();
    }
    
    bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        return results.Count > 0;
    }

    public bool GetIsMoveable(){
        return isMoveable;
    }
}
