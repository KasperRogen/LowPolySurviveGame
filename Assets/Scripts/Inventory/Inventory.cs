using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public int space;


    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;




    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Not enough room in inventory.");
            return false;
        }

        if (item.isDefaultItem == false)
        items.Add(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if(OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }





}
