using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake() {
        if (instance != null)
        {
            Debug.LogWarning("More than one Inventory instance");
            return;
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();
    [SerializeField] int space = 20;

    public bool AddItem(Item item){
        if(!item.isDefaultItem){
            if(items.Count >= space){
                Debug.Log("Inventory Full");
                return false;
            }
            items.Add(item);
        }
        return true;
    }

    public void RemoveItem(Item item){
        items.Remove(item);
    }

}
