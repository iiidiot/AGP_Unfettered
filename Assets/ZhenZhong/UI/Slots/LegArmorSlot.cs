using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegArmorSlot : Inventory
{
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

                newSlot.name = "LegArmorSlot";
                newSlot.tag = "LegArmorSlot";

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
}
