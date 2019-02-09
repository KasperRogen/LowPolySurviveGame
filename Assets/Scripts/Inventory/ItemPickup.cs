using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item;

    public override void Interact()
    {
        PickUp();
    }


    void PickUp()
    {

        Debug.Log("Picking up " + item.name);

        if (item.pickupToActiveInventory)
        {
            if(PlayerScript.activeInventory.Add(item))
            {
                Destroy(gameObject);
                return;
            }
            else if (PlayerScript.playerInventory.Add(item))
            {
                Destroy(gameObject);
                return;
            }
        }

        if (PlayerScript.playerInventory.Add(item))
        {
            Destroy(gameObject);
            return;
        }
        else if (PlayerScript.activeInventory.Add(item))
        {
            Destroy(gameObject);
            return;
        }



    }

}
