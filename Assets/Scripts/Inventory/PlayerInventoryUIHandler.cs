using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUIHandler : InventoryUIHandler
{



    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerScript.playerInventory;
        base.Start();
    }




}
