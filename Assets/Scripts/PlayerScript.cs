using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour {



    [Tooltip("The range the player can interact with- and pickup stuff")]
    public float reachRange = 5f;
    private Camera cam;
    public GameObject PlayerInventoryUI;
    bool isUIOpen = false;

    [SerializeField]
    public static Inventory playerInventory;
    public static Inventory activeInventory;

    public delegate void OnUIToggled();
    public static OnUIToggled OnUIToggledCallback;


    // Use this for initialization
    void Start () {
        playerInventory = GetComponent<PlayerInventory>();
        activeInventory = GetComponent<ActiveInventory>();

        cam = Camera.main.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetButtonDown("Inventory"))
        {
            PlayerInventoryUI.SetActive(!PlayerInventoryUI.activeSelf);

            isUIOpen = !isUIOpen;

            if (OnUIToggledCallback != null)
                OnUIToggledCallback.Invoke();
        }



        if(isUIOpen == false) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, reachRange))
            {
                Interactable interact = hit.transform.GetComponent<Interactable>();
                if (interact != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("PickUp");
                        interact.Interact();
                    }
                }
            }
        }



    }



    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    }




}
