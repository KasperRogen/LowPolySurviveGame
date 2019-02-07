using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    Item item;
    public Image icon;
    public Inventory inventory;


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
        icon.raycastTarget = false;
    }

    

    public void OnPointerUp(PointerEventData eventData)
    {
        isGrabbed = false;
        if (!EventSystem.current.IsPointerOverGameObject()) { 
            inventory.Remove(item);
            icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        }
        else {
            icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        }
        icon.raycastTarget = true;
    }

}
