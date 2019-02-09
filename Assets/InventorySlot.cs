using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    Item item;
    public int index;
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
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        InventorySlot slot = null;

        foreach(RaycastResult result in raycastResults)
        {
            if(result.gameObject.GetComponent<InventorySlot>() != null)
            {
                slot = result.gameObject.GetComponent<InventorySlot>();
            }
        }

        isGrabbed = false;


        if (!EventSystem.current.IsPointerOverGameObject())
        {
            inventory.RemoveAtIndex(index, true);
            icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;
        }

        else if (slot != null && slot.item == null)
        {
            slot.inventory.AddAtIndex(item, slot.index);

            inventory.RemoveAtIndex(index, false);
        }

        icon.rectTransform.anchoredPosition = transform.parent.GetComponent<RectTransform>().anchoredPosition;

        icon.raycastTarget = true;
    }

}
