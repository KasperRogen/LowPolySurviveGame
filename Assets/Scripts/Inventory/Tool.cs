using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Tool", menuName = "Inventory/Tool")]
public class Tool : Item
{
    public int WoodHarvestRate;
    public int WoodHarvestLoss;

    public int StoneHarvestRate;
    public int StoneHarvesetLoss;

    public int FleshHarvestRate;
    public int FleshHarvestLoss;

    public int Durability;
    public int Damage;

    [Tooltip("The first person prefab, \"The hands and the axe they are holding\"")]
    public GameObject FPPrefab;
}
