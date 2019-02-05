using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour {


    Inventory inventory;

	// Use this for initialization
	void Start () {
        inventory = GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.DropItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.DropItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.DropItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.DropItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory.DropItem(4);
        }
    }


    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    }




}
