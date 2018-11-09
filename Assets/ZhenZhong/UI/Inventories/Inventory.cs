using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    This is the Inventory class for Player to manage his Items.
    It is an UI feature for displaying items, add items, remove items, etc.

    Right now the player can buy items from the shop by clicking the items,
    or trigger the object on the map.
*/
public class Inventory : MonoBehaviour
{
    private static GameObject m_hoverObject;

    private static Slot m_fromSlot;
    //private static Slot m_toSlot;

    protected float m_hoverYOffset;

    protected RectTransform m_inventoryRect;

    protected float m_inventoryWidth;

    protected float m_inventoryHeight;

    // This is the container to store all the slots in the inventory.
    protected List<GameObject> m_slots;

    protected static int m_emptySlots;

    // Getter and setter
    public static int EmptySlots
    {
        get
        {
            return m_emptySlots;
        }

        set
        {
            m_emptySlots = value;
        }
    }

    // Set them at the Inspector
    public int totalSlots;

    public int rows;

    public float slotPaddingLeft;
    public float slotPaddingTop;

    public float slotSize;

    public int totalMoney;
    public Text MoneyText;

    public GameObject slotPrefab;

    //public GameObject iconPrefab;

    public Canvas canvas;

    public EventSystem eventSystem;

    protected virtual void Awake()
    {
        // Initialize the inventory
        CreateLayOut();
       // HandleInput();
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    // Update is called once per frame
    protected virtual void Update()
    {
        //DetectedShop();

        FollowMousePosition();
    }

    //protected virtual void DetectedShop()
    //{
    //    // Check if the left mouse button was clicked (up)
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        // Check if the mouse was clicked over a shop UI element
    //        SellItem();

    //        // Check if the mouse is not over a game object. If not, drop it.
    //        if (!eventSystem.IsPointerOverGameObject(-1) && m_fromSlot)
    //        {
    //            ResetSlotData();
    //        }
    //    }
    //}

    // Helper function to reset all the slot data for dragging and dropping.
    protected virtual void ResetSlotData()
    {
        // Reset everything
        m_fromSlot.GetComponent<Image>().color = Color.white;
        m_fromSlot.ClearSlot();
        Destroy(GameObject.Find("Hover Object"));
        //m_toSlot = null;
        m_fromSlot = null;
        m_hoverObject = null;
    }

    protected virtual void SellItem()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                // It's within the shop, sell the item, and receive my money.
                if (go.gameObject.name == "Shop" && m_fromSlot)
                {
                    int price = m_fromSlot.GetItem().price;
                    int size = m_fromSlot.Items.Count;

                    totalMoney += (price * size);

                    MoneyText.text = totalMoney.ToString();

                    // Reset everything
                    ResetSlotData();

                    break;
                }
            }
        }
    }

    // Make sure the hovering object is following the mouse position.
    protected virtual void FollowMousePosition()
    {
        if (m_hoverObject)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                                                                    Input.mousePosition, canvas.worldCamera, out position);

            // The Y offset is to move the icon a little bit out of the mouse,
            // so that it's easier to drop to the new location.
            position.Set(position.x, position.y - m_hoverYOffset);

            m_hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    protected virtual void CreateLayOut()
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
        for(int y = 0; y < rows; y++)
        {
            // Columns
            for(int x = 0; x < columns; x++)
            {
                // Create a new slot using the slot Prefab.
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";
                newSlot.tag = "Slot";

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

    //protected virtual void HandleInput()
    //{
    //    foreach (GameObject slot in m_slots)
    //    {
    //        Slot currentSlot = slot.GetComponent<Slot>();
    //        Button currentButton = currentSlot.GetComponent<Button>();
    //       // Item currentItem = currentSlot.GetItem();

    //        // Listen to the request and do the callback
    //        currentButton.onClick.AddListener(delegate { Drag(slot); });        
    //    }
    //}

    //protected virtual void Drag(GameObject item)
    //{
    //    if (item)
    //    {
    //        MoveItem(item);
    //    }
    //}

    // This is to check if an item can be stacked at the same slot.
    public virtual bool AddItem(Item item)
    {
        if(item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }

        // Stack the same items together at the same slot
        else
        {
            foreach(GameObject slot in m_slots)
            {
                Slot currentSlot = slot.GetComponent<Slot>();

                if(!currentSlot.IsEmpty)
                {
                    if(currentSlot.GetItem().itemType == item.itemType &&
                       currentSlot.IsAvailable)
                    {
                        currentSlot.AddItem(item);
                        return true;
                    }
                }
            }

            if(m_emptySlots > 0)
            {
                PlaceEmpty(item);
            }
        }

        return false;
    }

    // Find an empty slot and add the new item in.
    protected virtual bool PlaceEmpty(Item item)
    {
        if(m_emptySlots > 0)
        {
            foreach(GameObject slot in m_slots)
            {
                Slot currentSlot = slot.GetComponent<Slot>();

                if(currentSlot.IsEmpty)
                {
                    currentSlot.AddItem(item);
                    m_emptySlots--;
                    return true;
                }
            }
        }

        return false;
    }

    //protected virtual void MoveItem(GameObject clicked)
    //{
    //    if(!m_fromSlot)
    //    {
    //        // Make sure the item we are clicking on is valid.
    //        if(!clicked.GetComponent<Slot>().IsEmpty)
    //        {
    //            // Set this slot as the "From" slot.
    //            m_fromSlot = clicked.GetComponent<Slot>();

    //            // Highlight the current clicked slot
    //            m_fromSlot.GetComponent<Image>().color = Color.gray;

    //            // Create the hovering object, and set it as a child of the Canvas.
    //            // It's used for hovering the current item and moving it around.
    //            m_hoverObject = (GameObject)Instantiate(iconPrefab);
    //            m_hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
    //            m_hoverObject.name = "Hover Object";

    //            RectTransform hoverTransform = m_hoverObject.GetComponent<RectTransform>();
    //            RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

    //            // Set the size of the rectTransform
    //            hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
    //            hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

    //            m_hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

    //            m_hoverObject.transform.localScale = m_fromSlot.gameObject.transform.localScale;
    //        }
    //    }

    //    else if(!m_toSlot)
    //    {
    //        m_toSlot = clicked.GetComponent<Slot>();

    //        // Remove the hovering icon after placing the items to the target slot.
    //        Destroy(GameObject.Find("Hover Object"));
    //    }

    //    // if Both slot are valid, cancel the action.
    //    if (m_toSlot && m_fromSlot)
    //    {
    //        //Stack<Item> tempTo = new Stack<Item>(m_toSlot.Items);

    //        //// move the items to the target slot.
    //        //m_toSlot.AddItems(m_fromSlot.Items);

    //        //// If the target slot is empty, that means
    //        //// we successfully move all the items.
    //        //// So we need to clear the source slot.
    //        //if (tempTo.Count == 0)
    //        //{
    //        //    m_fromSlot.ClearSlot();
    //        //}

    //        //// Otherwise, we just swap items between the source
    //        //// and the target slots.
    //        //else
    //        //{
    //        //    m_fromSlot.AddItems(tempTo);
    //        //}

    //        m_fromSlot.GetComponent<Image>().color = Color.white;

    //        // reset them for the next moving process.
    //        m_toSlot = null;
    //        m_fromSlot = null;
    //        m_hoverObject = null;
    //    }
    //}
}
