using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is the Inventory class for Player to manage his Items.
    It is an UI feature for displaying items, add items, remove items, etc.

    Right now the player can buy items from the shop by clicking the items,
    or trigger the object on the map.
*/
public class Inventory : MonoBehaviour
{
    private RectTransform m_inventoryRect;

    private float m_inventoryWidth;

    private float m_inventoryHeight;

    // This is the container to store all the slots in the inventory.
    protected List<GameObject> m_slots;

    private int m_emptySlots;

    // Set them at the Inspector
    public int totalSlots;

    public int rows;

    public float slotPaddingLeft;
    public float slotPaddingTop;

    public float slotSize;

    public GameObject slotPrefab;

    void Awake()
    {
        // Initialize the inventory
        CreateLayOut();
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    protected void CreateLayOut()
    {
        m_slots = new List<GameObject>();

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
        for(int y = 0; y < rows; y++)
        {
            // Columns
            for(int x = 0; x < columns; x++)
            {
                // Create a new slot using the slot Prefab.
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

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

    // This is to check if an item can be stacked at the same slot.
    // Currently, all items will not be stacked.
    public bool AddItem(Item item)
    {
        if(item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }

        return false;
    }

    // Find an empty slot and add the new item in.
    private bool PlaceEmpty(Item item)
    {
        if(m_emptySlots > 0)
        {
            foreach(GameObject slot in m_slots)
            {
                Slot currentSlot = slot.GetComponent<Slot>();

                if(currentSlot.IsEmpty())
                {
                    currentSlot.AddItem(item);
                    m_emptySlots--;
                    return true;
                }
            }
        }

        return false;
    }
}
