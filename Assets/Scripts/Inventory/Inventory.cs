using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int slotCount;

    public GameObject[] slots;

	// Use this for initialization
	void Start () {
        slots = new GameObject[slotCount];
	}
	

    public void AddItem(GameObject ToAdd)
    {
        InventoryItem item = ToAdd.GetComponent<InventoryItem>();

        if(item.ItemType == InventoryItem.InventoryItemType.stackable)
        {
            foreach(GameObject GO in slots)
            {
                if (GO == null)
                    continue;

                InventoryItem currentItem = GO.GetComponent<InventoryItem>();

                if(currentItem.ItemName == item.ItemName && currentItem.ItemStackSize < currentItem.MaxItemStackSize)
                {
                    int deltaStackSize = currentItem.MaxItemStackSize - currentItem.ItemStackSize;
                    if(item.ItemStackSize > deltaStackSize)
                    {
                        currentItem.ItemStackSize += deltaStackSize;
                        item.ItemStackSize -= deltaStackSize;
                    } else
                    {
                        currentItem.ItemStackSize += item.ItemStackSize;
                        Destroy(ToAdd);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slotCount; i++)
        {
            if(slots[i] == null)
            {
                slots[i] = ToAdd;
                ToAdd.transform.parent = transform;
                ToAdd.SetActive(false);
                return;
            }
        }


    }

    public void DropItem(int index)
    {
        InventoryItem item = slots[index].GetComponent<InventoryItem>();
        GameObject GO = Instantiate(item.gameObject, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);
        GO.GetComponent<InventoryItem>().ItemStackSize = 1;
        GO.transform.parent = null;
        GO.SetActive(true);
        GO.name = item.ItemName;
        GO.transform.localScale = Vector3.one;

        if (--item.ItemStackSize == 0)
        {
            Destroy(slots[index]);
            slots[index] = null;
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
