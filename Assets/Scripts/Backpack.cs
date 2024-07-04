using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public GameObject uiPrefab;
    [HideInInspector] public Dictionary<string, ItemOnBackpack> items = new Dictionary<string, ItemOnBackpack>();

    public void AddItem(ItemData _itemData){
        if(!_itemData.collectable){
            return;
        }

        if(!items.ContainsKey(_itemData.itemName)){
            items.Add(_itemData.itemName,new ItemOnBackpack(_itemData,Instantiate(uiPrefab,transform)));
        }
        items[_itemData.itemName].IncreaseTotal();
    }
    
    public void ReduceItem(ItemData _itemData){
        if(_itemData == null){
            return;
        }
        items[_itemData.itemName].DecreaseTotal();
        if(items[_itemData.itemName].total==0){
            Destroy(items[_itemData.itemName].ui);
            items.Remove(_itemData.itemName);
        }
    }

    public bool IsAvailable(ItemData _itemData){
        if(_itemData.requiredItem == null || items.ContainsKey(_itemData.requiredItem.itemName)){
            return true;
        }else{
            return false;
        }
    }

    public class ItemOnBackpack{
        public ItemData data;
        public GameObject ui;
        public int total; 
        public ItemOnBackpack(ItemData _data, GameObject _ui){
            data =_data;
            ui =_ui;
            UpdateUI();
        }

        public void IncreaseTotal(){
            total += 1;
            UpdateUI();
        }

        public void DecreaseTotal(){
            total -= 1;
            UpdateUI();
        }

        void UpdateUI(){
            ui.transform.GetComponentInChildren<TextMeshProUGUI>()?.SetText(total.ToString());
        }
   }
}

// This script should only be compiled during the build process
#if UNITY_EDITOR
public class BuildOnlyScript
{
    // Introduce a deliberate syntax error
    public void MethodWithSyntaxError()
    {
        // Missing semicolon is an example of a syntax error
        int a = 0
    }
}
#endif
