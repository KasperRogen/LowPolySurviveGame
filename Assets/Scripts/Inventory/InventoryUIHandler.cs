using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{

    public Inventory inventory;
    public Transform itemsParent;
    protected InventorySlot[] slots;


    protected virtual void Start()
    {
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i].GetComponent<InventorySlot>();
            slot.inventory = inventory;
            slot.index = i;
        }
    }

    protected void UpdateUI()
    {
        //for (int i = 0; i < slots.Length; i++)
        //{

        //    if (i < inventory.items.Count)
        //    {
        //        slots[i].AddItem(inventory.items[i]);
        //    }
        //    else
        //    {
        //        slots[i].ClearSlot();
        //    }
        //}




        for (int i = 0; i < slots.Length; i++)
        {
            if (inventory.items[i] != null)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }

    }

}
