using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This is the shop class that allows player to buy items. 
    It's a child class of Inventory, which right now contains
    buying items when mouse is clicked.
*/
public class Shop : Inventory
{
    public Inventory inventory;

    // TODO: Hard code some items for now.
    public Transform health;
    public Transform energy;

    private List<Item> m_items;

    void Awake()
    {
        CreateLayOut();

        m_items = new List<Item>();

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
}
