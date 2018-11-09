using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : Inventory
{
   // private bool m_oneClicked = false;

    private bool m_timeRunning;

    private float m_doubleClickTimer;

    //this is how long in seconds to allow for a double click
    //private float m_delay = 1.0f;

    public Transform sword;
    public Transform sword2;
    public Transform fu;
    public Transform fu2;

    protected override void Awake()
    {
        base.Awake();

        // Debug
        base.AddItem(fu.GetComponent<Item>());
        base.AddItem(sword.GetComponent<Item>());
        base.AddItem(sword2.GetComponent<Item>());
        base.AddItem(fu2.GetComponent<Item>());
    }

    protected override void CreateLayOut()
    {
        m_slots = new List<GameObject>();

        m_hoverYOffset = slotSize * 0.01f;

        m_emptySlots = totalSlots;

        int columns = totalSlots / rows;

        m_inventoryWidth = columns * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        m_inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        // Get the RectTransform of the Inventory object and initialize the size of it.
        m_inventoryRect = GetComponent<RectTransform>();
        m_inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_inventoryWidth);
        m_inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_inventoryHeight);

        // Add slots into the Inventory
        // Rows
        for (int y = 0; y < rows; y++)
        {
            // Columns
            for (int x = 0; x < columns; x++)
            {
                // Create a new slot using the slot Prefab.
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "WeaponSlot";
                newSlot.tag = "WeaponSlot";

                // Set the new slot's parent as the Inventory's parent (Canvas).
                // Otherwise, it will not be displayed onto the screen.
                newSlot.transform.SetParent(this.transform.parent);

                // Set position of the slot.
                slotRect.localPosition = m_inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),
                                                                                    -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                // Add this new slot to the list
                m_slots.Add(newSlot);
            }
        }
    }

    //protected virtual void doubleClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        if (!m_oneClicked)
    //        {
    //            m_oneClicked = true;

    //            // save the current timer
    //            m_doubleClickTimer = Time.time;

    //            // TODO: do one click things
    //        }

    //        else
    //        {
    //            // Found a double click, reset
    //            m_oneClicked = false;

    //            // TODO: do double click things
    //        }
    //    }

    //    // At this point if the one click is still true
    //    // we should reset it. It's not a double click, and it passes the 
    //    // effectiveness of the current first click
    //    if (m_oneClicked)
    //    {
    //        if (Time.time - m_doubleClickTimer > m_delay)
    //        {
    //            m_oneClicked = false;
    //        }
    //    }
    //}
}
