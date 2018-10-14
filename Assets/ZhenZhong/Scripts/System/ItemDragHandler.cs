using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Slot m_slot;
    private Item m_item;

    private Vector3 m_defaultPosition;

    private Slot m_targetSlot;

    void Start()
    {
        m_defaultPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_slot = GetComponent<Slot>();

        if(!m_slot.IsEmpty)
        {
            transform.position = Input.mousePosition;

            m_item = m_slot.GetItem();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!FindTargetSlot())
        {
            transform.localPosition = m_defaultPosition;
            return;
        }

        if (m_targetSlot)
        {
            // target slot has no item, then we can add the item to the target slot.
            if (m_targetSlot.Items.Count <= 0)
            {
                m_targetSlot.AddItem(m_item);

                // Swap the item image between two slots
                m_slot.ClearSlot();              
            }

            // else if both slots have items, swap them without adding the item to the target slot.
            else
            {
                Item targetItem = m_targetSlot.GetItem();

                if (targetItem.maxSize == m_item.maxSize)
                {
                    m_targetSlot.ClearSlot();

                    m_targetSlot.AddItem(m_item);

                    m_slot.ClearSlot();

                    m_slot.AddItem(targetItem);
                }
            }

            transform.localPosition = m_defaultPosition;
        }
    }

    private bool FindTargetSlot()
    {
        if(!m_slot || m_slot && m_slot.IsEmpty)
        {
            return false;
        }

        if (FindTargetSlot(m_item.tag))
        {
            return true;
        }

        return false;
    }

    private bool FindTargetSlot(string tag)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        foreach (var go in raycastResults)
        {
            // Tag = item tag, Target tag = slot tag
            // weapon slot -> sword slot
            if (m_slot.tag == "WeaponSlot" && tag == "Sword" && go.gameObject.tag == "SwordSlot")
            {
                m_targetSlot = go.gameObject.GetComponent<Slot>();
                return true;
            }

            // sword slot -> weapon slot
            else if(m_slot.tag == "SwordSlot" && tag == "Sword" && go.gameObject.tag == "WeaponSlot")
            {
                m_targetSlot = go.gameObject.GetComponent<Slot>();
                return true;
            }

            else if (m_slot.tag == "WeaponSlot" && tag == "Fu" && go.gameObject.tag == "FuSlot")
            {
                m_targetSlot = go.gameObject.GetComponent<Slot>();
                return true;
            }

            else if (m_slot.tag == "FuSlot" && tag == "Fu" && go.gameObject.tag == "WeaponSlot")
            {
                m_targetSlot = go.gameObject.GetComponent<Slot>();
                return true;
            }
        }

        return false;
    }
}
