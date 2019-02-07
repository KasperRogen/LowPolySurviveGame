using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    Item item;
    public Image icon;
    public Inventory inventory;


    GameObject currentUI;
    Vector3 oldPosition;

    private bool isGrabbed = false;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }


    public void Update()
    {
        if (isGrabbed)
        {
            icon.rectTransform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isGrabbed = true;   
    }

    

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Dropping " + currentUI.name);
        isGrabbed = false;
        if (currentUI == null) { 
            Debug.Log("False");
            inventory.Remove(item);
            icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        }
        else {
            icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("True");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerEnter.gameObject);
        currentUI = eventData.pointerEnter.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited");
        currentUI = null;
    }
}
