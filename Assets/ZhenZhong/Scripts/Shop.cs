using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Shop class is a class that allows players to buy/sell items.
    Buying items will adding them to the player's inventory.
*/
public class Shop : MonoBehaviour
{
    public Inventory inventory;

    // TODO: Hard code some items for now.
    public Transform health;
    public Transform energy;

    public GameObject slotPrefab;

    // Set them at the Inspector
    public int totalSlots;

    public int rows;

    public float slotPaddingLeft;
    public float slotPaddingTop;

    public float slotSize;

    //private List<Item> m_items;

    private RectTransform m_shopRect;
    private float m_shopWidth;
    private float m_shopHeight;

    // This is the container to store all the slots in the inventory.
    private List<GameObject> m_slots;

    private static int m_emptySlots;

    void Awake()
    {
        CreateLayOut();

        //m_items = new List<Item>();

        // TODO: need to make it more dynamic, right now it's hard coded.
        Item healthItem = health.GetComponent<Item>();
        AddItem(healthItem);

        Item energyItem = energy.GetComponent<Item>();
        AddItem(energyItem);

        HandleInput();
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

        m_shopWidth = columns * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        m_shopHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        // Get the RectTransform of the Inventory object and initialize the size of it.
        m_shopRect = GetComponent<RectTransform>();
        m_shopRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_shopWidth);
        m_shopRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_shopHeight);

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

                newSlot.name = "Slot";

                // Set the new slot's parent as the Inventory's parent (Canvas).
                // Otherwise, it will not be displayed onto the screen.
                newSlot.transform.SetParent(this.transform.parent);

                // Set position of the slot.
                slotRect.localPosition = m_shopRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x),
                                                                               -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                // Add this new slot to the list
                m_slots.Add(newSlot);
            }
        }
    }
    
    private void HandleInput()
    {
        foreach(GameObject slot in m_slots)
        {
            Slot currentSlot = slot.GetComponent<Slot>();
            Button currentButton = currentSlot.GetComponent<Button>();
            Item currentItem = currentSlot.GetItem();

            // Listen to the request and do the callback
            currentButton.onClick.AddListener(delegate { TaskOnClick(currentItem); });
        }
    }

    // When the button in the shop is clicked,
    // add the item to the Player's inventory
    private void TaskOnClick(Item item)
    {
        if(item)
        {
            inventory.AddItem(item);
        }
    }

    // This is to check if an item can be stacked at the same slot.
    public bool AddItem(Item item)
    {
        // TODO: will need to modify later if items can be stacked.
        // Currently it's hard coded.
        PlaceEmpty(item);
        return true;
    }

    // Find an empty slot and add the new item in.
    private bool PlaceEmpty(Item item)
    {
        if (m_emptySlots > 0)
        {
            foreach (GameObject slot in m_slots)
            {
                Slot currentSlot = slot.GetComponent<Slot>();

                if (currentSlot.IsEmpty)
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
