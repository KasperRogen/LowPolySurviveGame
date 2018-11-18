using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public enum ResourceType
    {
        FOOD,
        WOOD,
        STONE
    }

    public ResourceType type;

    public float resourceAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
