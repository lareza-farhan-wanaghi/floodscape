using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Backpack : MonoBehaviour
{
   [SerializeField] GameObject uiPrefab;
    Dictionary<string, ItemOnBackpack> items = new Dictionary<string, ItemOnBackpack>();

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
        if(_itemData.requiredItem == null){
            return;
        }

        items[_itemData.requiredItem.itemName].DecreaseTotal();
        if(items[_itemData.requiredItem.itemName].total==0){
            Destroy(items[_itemData.requiredItem.itemName].ui);
            items.Remove(_itemData.requiredItem.itemName);
        }
    }

    public bool IsAvailable(ItemData _itemData){
        if(_itemData.requiredItem == null || items.ContainsKey(_itemData.requiredItem.itemName)){
            return true;
        }else{
            return false;
        }
    }
    class ItemOnBackpack{
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
            ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(total.ToString());
        }
   }
}
