using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventoryUIHandler : InventoryUIHandler
{
    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerScript.activeInventory;
        base.Start();
    }
}
