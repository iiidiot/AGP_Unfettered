using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This class is to manage same items stacking together,
    and display the number onto the UI.
*/
public class Slot : MonoBehaviour
{
    private Stack<Item> m_items;

    public Text stackText;

    public Sprite slotEmpty;
    public Sprite slotHighLight;

    public bool IsEmpty()
    {
        return m_items.Count == 0;
    }

    public Item GetItem()
    {
        if(!IsEmpty())
        {
            return m_items.Peek();
        }

        return null;
    }

    void Awake()
    {
        m_items = new Stack<Item>();
    }

    void Start ()
    {
        RectTransform slotRect = GetComponent<RectTransform>();

        RectTransform textRect = GetComponent<RectTransform>();

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
    }
}
