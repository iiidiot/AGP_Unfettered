//#define ON_DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    重要:
    Only Display the list of valid farm items in the inventory.

    The item objects should use the same Item class like any other items in the inventory,
    but farm objects can be detected by a boolean: isFarmObject (example is in FarmItemsSO.cs).

    I don't know what classes are useful right now, so I use mine for now, but idea should be similar.

    Idea:
    1. Pull the list of valid farm items from the inventory.
    2. Get the size of the list.
    3. Visible slots on the screen: 5
    4. Loop through the list and change each slots' images based on user input (left or right)
    5. Remove items by setting the target ptr's value to -1, so it won't read from the ScriptableObject list.
*/

class ListNode
{
    public ListNode(int v)
    {
        val = v;
        next = null;
        prev = null;
    }

    public int val;
    public ListNode next;
    public ListNode prev;
}

public class UIFarmItemManager : MonoBehaviour
{
    public static UIFarmItemManager s_instance = null;

    public static int s_index;
    public static GameObject s_targetFarmItem;

    public FarmItemsSO farmItemsSO;

    [SerializeField]
    private float m_UISize;

    private static Image[] m_slots;

    private static List<Item> m_farmItems;
    
    private static ListNode s_currentPtr;
    private static ListNode s_targetPtr;

    private ListNode m_indexList;
    private ListNode m_head;

    // For future use
    private ListNode m_tail;
    
    private int m_totalFarmItems;

    private const int m_slotSize =5;

    private void Awake()
    {
        if (!s_instance)
        {
            s_instance = this;
        }
        else if (s_instance)
        {
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        s_index = -1;
        s_targetFarmItem = null;

        // Debug: It might needs to be dynamic array, because we can add items into the inventory
        m_farmItems = farmItemsSO.InventoryItems;

        m_totalFarmItems = m_farmItems.Count;

        m_slots = transform.GetComponentsInChildren<Image>();

        int diff = m_slotSize - m_totalFarmItems;

        m_indexList = new ListNode(0);
        s_currentPtr = m_indexList;

        for(int i = 1; i < m_totalFarmItems; i++)
        {
            ListNode node = new ListNode(i);

            node.prev = s_currentPtr;
            s_currentPtr.next = node;

            s_currentPtr = s_currentPtr.next;
        }

        if(diff >0)
        {
            for (int i = 0; i < diff; i++)
            {
                ListNode node = new ListNode(-1);

                s_currentPtr.next = node;
                node.prev = s_currentPtr;

                s_currentPtr = s_currentPtr.next;
            }
        }

        // set the cycle
        s_currentPtr.next = m_indexList;
        m_indexList.prev = s_currentPtr;

        s_currentPtr = s_currentPtr.next;

        m_head = s_currentPtr;
        m_tail = s_currentPtr.prev;

        // Set scale
        Vector3 scale = s_instance.transform.localScale;
        scale *= m_UISize;
        s_instance.transform.localScale = scale;
    }

    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.D))
        {
            RotateLeft();
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            RotateRight();
        }
    }

    // Receive event notifications from GridManager.cs and display the UI on screen, 
    // at the mouse position, pointing to the middle slot of the list.
    public static void DisplayUI()
    {
        // Display the UI first
        s_instance.GetComponentInParent<Canvas>().enabled = true;

        for (int i = 0; i < m_slotSize; i++)
        {
            if(s_currentPtr.val == -1)
            {
                m_slots[i].sprite = null;
            }
            else
            {
                m_slots[i].sprite = m_farmItems[s_currentPtr.val].sprite;
            }

            s_currentPtr = s_currentPtr.next;
        }

        Vector3 pos = Input.mousePosition;
        pos.y += 100f;

        s_instance.transform.position = pos;
    }

    private void RotateLeft()
    {
#if ON_DEBUG
        Debug.Log(m_head.val);
#endif

        s_currentPtr = m_head;

        for(int i = 0; i < m_slotSize; i++)
        {
            if (i == 0)
            {
                m_head = s_currentPtr.next;
                m_tail = s_currentPtr.next.prev;
            }

            // set the index that can be chosen
            else if(i == 2)
            {
                s_index = s_currentPtr.next.val;

                if(s_index != -1)
                {
                    s_targetFarmItem = farmItemsSO.InventoryItems[s_index].itemPrefab;
                    s_targetPtr = s_currentPtr.next;
                }
            }

            if (s_currentPtr.next.val == -1)
            {
                m_slots[i].sprite = null;
            }
            else
            {
                m_slots[i].sprite = m_farmItems[s_currentPtr.next.val].sprite;
            }

            s_currentPtr = s_currentPtr.next;
        }   
    }

    private void RotateRight()
    {
#if ON_DEBUG
        Debug.Log(m_head.val);
#endif
        s_currentPtr = m_head;

        for (int i = 0; i < m_slotSize; i++)
        {
            if (i == 0)
            {
                m_head = s_currentPtr.prev;
                m_tail = s_currentPtr.prev.prev;
            }

            // set the index that can be chosen
            else if (i == 2)
            {
                s_index = s_currentPtr.prev.val;

                if(s_index != -1)
                {
                    s_targetFarmItem = farmItemsSO.InventoryItems[s_index].itemPrefab;
                    s_targetPtr = s_currentPtr.prev;
                }
            }

            if (s_currentPtr.prev.val == -1)
            {
                m_slots[i].sprite = null;
            }
            else
            {
                m_slots[i].sprite = m_farmItems[s_currentPtr.prev.val].sprite;
            }

            s_currentPtr = s_currentPtr.next; 
        }
    }

    public static void RemoveItem()
    {
        s_targetFarmItem = null;
        s_index = -1;
        s_targetPtr.val = -1;
    }

    
}
