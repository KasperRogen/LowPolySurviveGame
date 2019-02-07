using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public string description = "Some Description";
    public bool pickupToActiveInventory = false;
    public Sprite icon = null;
    public bool isDefaultItem = false;


}

