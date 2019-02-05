using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

    public enum InventoryItemType
    {
        single,
        stackable
    }

    public string ItemName;
    public Sprite ItemSprite;
    public InventoryItemType ItemType;
    public int MaxItemStackSize;
    public int ItemStackSize;


    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void OnMouseDown()
    {
        Debug.Log("GOT ME");
        player.GetComponent<Inventory>().AddItem(this.gameObject);
    }


}
