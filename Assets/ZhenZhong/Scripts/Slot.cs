using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    This class is to manage same items stacking together,
    and display the number onto the UI.
*/
public class Slot : MonoBehaviour, IPointerClickHandler
{
    private Stack<Item> m_items;

    public Text stackText;

    public Sprite slotEmpty;
    public Sprite slotHighLight;

    private Vector3 m_localPosition;

    public bool IsEmpty
    {
        get { return m_items.Count == 0; }
    }

    public Item GetItem()
    {
        if (!IsEmpty)
        {
            return m_items.Peek();
        }
        return null;
    }

    public bool IsAvailable
    {
        get { return GetItem().maxSize > m_items.Count; }
    }

    public Stack<Item> Items
    {
        get
        {
            return m_items;
        }

        set
        {
            m_items = value;
        }
    }

    void Awake()
    {
        m_items = new Stack<Item>();
    }

    void Start ()
    {
        RectTransform slotRect = GetComponent<RectTransform>();

        RectTransform textRect = stackText.GetComponent<RectTransform>();

        // This is a text scaling factor to make sure the text display will always have
        // the same size ratio (60% of the slot size for now).
        int textScaleFactor = (int)(slotRect.sizeDelta.x * 0.6);
        stackText.resizeTextMinSize = textScaleFactor;
        stackText.resizeTextMaxSize = textScaleFactor;


        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        textRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);

         
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Add the list of items into the current slot
    public void AddItems(Stack<Item> items)
    {
        this.m_items = new Stack<Item>(items);

        // Update the number text on the current slot.
        stackText.text = (m_items.Count > 1) ? m_items.Count.ToString() : string.Empty;

        // Update the sprite of this slot.
        ChangeSprite(GetItem().normalSprite, GetItem().hightLightedSprite);
    }


    public void AddItem(Item item)
    {
        m_items.Push(item);

        if(m_items.Count > 1)
        {
            // Display the number onto the screen.
            stackText.text = m_items.Count.ToString();
        }

        // Update the sprite of this slot.
        ChangeSprite(item.normalSprite, item.hightLightedSprite);
    }

    private void ChangeSprite(Sprite normalSprite, Sprite highLightedSprite)
    {
        GetComponent<Image>().sprite = normalSprite;

        SpriteState spriteState = new SpriteState();
        spriteState.highlightedSprite = highLightedSprite;
        spriteState.pressedSprite = normalSprite;

        // Update the Button Component's Sprite in the ItemSlot Prefab
        // whenever a new item has been added to this ItemSlot.
        GetComponent<Button>().spriteState = spriteState;

        m_localPosition = GetComponent<Button>().transform.localPosition;
    }

    // Take the current item inside the slot and use it.
    private void UseItem()
    {
        if(!IsEmpty)
        {
            // Remove the item from the stack
            m_items.Pop().Use();

            // Update the number text on the current slot.
            stackText.text = (m_items.Count > 1) ? m_items.Count.ToString() : string.Empty;

            // If it's an empty slot again,
            // reset the slot image back to empty slot images.
            if(IsEmpty)
            {
                ChangeSprite(slotEmpty, slotHighLight);
                
                // Update the total number of empty slots in the inventory.
                Inventory.EmptySlots++;
            }
        }
    }

    // Clear the current slot, reset the sprite, and text number.
    public void ClearSlot()
    {
        m_items.Clear();
        ChangeSprite(slotEmpty, slotHighLight);
        stackText.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
}
