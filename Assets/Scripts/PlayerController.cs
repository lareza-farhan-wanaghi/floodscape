using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public EventSystem eventSystem;
    [HideInInspector] public GraphicRaycaster graphicRaycaster;
    [HideInInspector] public bool isMoveable = true;
    [HideInInspector] public InteractManager interactManager;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        interactManager = FindObjectOfType<InteractManager>();
        anim = GetComponent<Animator>();
        agent.speed = 5f;

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
        if (isMoveable && Input.GetMouseButton(0) && !IsPointerOverUI())
        {   
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            agent.SetDestination(mouseWorldPosition);
        }

        if(agent.remainingDistance > agent.stoppingDistance && agent.velocity.sqrMagnitude > 0.5f){
            if(!anim.GetBool("move")) {
                anim.SetBool("move", true);
            }
            Vector3 direction = agent.velocity.normalized;
            float angle = Vector3.SignedAngle(agent.transform.forward, direction, Vector3.up);

            if (angle > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (angle < 0)
            {
                spriteRenderer.flipX = true;
            }
        }else{
            if(anim.GetBool("move")) anim.SetBool("move", false);
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
