using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    public int space;


    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;


    public void Start()
    {
        for (int i = 0; i < space; i++)
            items.Add(null);
    }


    public bool Add(Item item)
    {

        for (int i = 0; i < space; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                if (OnItemChangedCallback != null)
                    OnItemChangedCallback.Invoke();
                return true;
            }
        }

        Debug.Log("Not enough room in inventory.");
        return false;
    }



    public bool AddAtIndex(Item item, int index)
    {

        if(items[index] != null)
        {
            return false;
        } else
        {
            items[index] = item;
            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
            return true;
        }
    }



    public void Remove(Item item, bool instantiate)
    {
        for(int i = 0; i < space; i++)
        {
            if(items[i] == item)
            {
                if(instantiate)
                    items[i].Instantiate(transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);


                items[i] = null;

                if (OnItemChangedCallback != null)
                    OnItemChangedCallback.Invoke();
            }
        }


    }


    public void RemoveAtIndex(int index, bool instantiate)
    {
        if (instantiate)
            items[index].Instantiate(transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);

        items[index] = null;

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();

    }





}
