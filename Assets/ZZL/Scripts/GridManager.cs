using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public FarmItemsSO farmItemsSO;

    [SerializeField]
    private int m_rows;

    [SerializeField]
    private int m_cols;

    [SerializeField]
    private float m_cellWidth;

    [SerializeField]
    private float m_cellHeight;

    // Grid's width and height.
    [SerializeField]
    private float m_width;

    [SerializeField]
    private float m_height;

    [SerializeField]
    private Vector3 m_startPos;

    private bool[,] m_visited;

    // It's used to update current farm items' prefab based on the days.
    // Stores number of days on each cell
    private int[,] m_days;

    private GameObject[,] m_field;

    private Vector3 m_mousePos;

    private int m_currentDay;

    // Getters
    public Vector3 StartPos
    {
        get
        {
            return m_startPos;
        }
    }
    public float Width
    {
        get
        {
            return m_width;
        }
    }
    public float Height
    {
        get
        {
            return m_height;
        }
    }
    
    private void Awake()
    {
        m_startPos = transform.position;
        m_width = m_cols * m_cellWidth;
        m_height = m_rows * m_cellHeight;

        m_visited = new bool[m_rows, m_cols];

        m_days = new int[m_rows, m_cols];

        m_field = new GameObject[m_rows, m_cols];

        for (int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_cols; c++)
            {
                m_visited[r, c] = false;
                m_days[r, c] = -1;
                m_field[r, c] = null;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        m_currentDay = TimeManager.s_days;
    }

    // Update is called once per frame
    void Update ()
    {
        // Allow designers to move around the grid and display on the scene view when the game is running
        m_startPos = transform.position;
        m_width = m_cols * m_cellWidth;
        m_height = m_rows * m_cellHeight;

        AddItem();

        if(m_currentDay != TimeManager.s_days)
        {
            UpdateDays();
            UpdateField();

            m_currentDay = TimeManager.s_days;
        } 
    }

    private void AddItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                m_mousePos = hitInfo.point;

                if (IsValidPosition(m_mousePos))
                {
                    PlaceObject();
                }
            }
        }
    }

    private void PlaceObject()
    {
        SetVisited(m_mousePos);
    }

    // Add new item to the field, everything is initialized.
    public void SetVisited(Vector3 mousePos)
    {
        Vector3 pos = GetCenterPosition(m_mousePos);

        GameObject item = Instantiate(farmItemsSO.AppleItems[0], pos, Quaternion.identity);

        int row = 0;
        int col = 0;

        Vector3 topLeft = GetTopLeftPosition(mousePos, out row, out col);

        m_visited[row, col] = true;
        m_field[row, col] = item;
        m_days[row, col] = 0;

       // Debug.Log("Mouse Position = " + topLeft.x + " , " + topLeft.z);
       // Debug.Log(row + " , " + col);
    }

    private void UpdateDays()
    {
        for(int r = 0; r < m_rows; r++)
        {
            for(int c =0; c < m_cols; c++)
            {
                if(m_days[r, c] >=0)
                {
                    // 假设游戏时间设定为写死，P5的时间系统
                    if (m_days[r,c] < farmItemsSO.AppleItems.Count -1)
                    {
                        m_days[r, c]++;
                    }
                    
                }
            }
        }
    }

    private void UpdateField()
    {
        for (int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_cols; c++)
            {
                // The current cell has something
                if (m_days[r, c] >= 0)
                {
                    // This should be the center position of the current cell.
                    Vector3 pos = m_field[r, c].transform.position;

                    Destroy(m_field[r, c]);

                    GameObject item = Instantiate(farmItemsSO.AppleItems[m_days[r, c]], pos, Quaternion.identity);
                    item.transform.localScale += new Vector3(10, 10, 10);

                    m_field[r, c] = item;
                }
            }
        }
    }

    private Vector3 GetTopLeftPosition(Vector3 mousePos)
    {
        float x = m_startPos.x;
        float z = m_startPos.z;

        while (x + m_cellWidth < mousePos.x)
        {
            x += m_cellHeight;
        }

        while (z + m_cellHeight < mousePos.z)
        {
            z += m_cellHeight;
        }

        return new Vector3(x, m_startPos.y, z);
    }

    private Vector3 GetTopLeftPosition(Vector3 mousePos, out int row, out int col)
    {
        float x = m_startPos.x;
        float z = m_startPos.z;

        int r = 0;
        int c = 0;

        while (x + m_cellWidth < mousePos.x)
        {
            x += m_cellHeight;
            r++;
        }

        while (z + m_cellHeight < mousePos.z)
        {
            z += m_cellHeight;
            c++;
        }

        row = r;
        col = c;

        return new Vector3(x, m_startPos.y, z);
    }

    public bool IsValidPosition(Vector3 mousePos)
    {
        int row = 0;
        int col = 0;

        Vector3 topLeft = GetTopLeftPosition(mousePos, out row, out col);

        return (mousePos.x >= m_startPos.x && mousePos.x < (m_startPos.x + m_height) &&
                mousePos.z >= m_startPos.z && mousePos.z < (m_startPos.z + m_width) &&
                !m_visited[row, col]);
    }

    public Vector3 GetCenterPosition(Vector3 mousePos)
    {
        Vector3 topLeft = GetTopLeftPosition(mousePos);

        //Vector3 topRight = new Vector3(topLeft.x, topLeft.y, topLeft.z + m_cellWidth);
        //Vector3 bottomLeft = new Vector3(topLeft.x + m_cellHeight, topLeft.y, topLeft.z);
        //Vector3 bottomRight = new Vector3(topLeft.x + m_cellHeight, topLeft.y, topLeft.z);

        return new Vector3( topLeft.x + (m_cellWidth / 2), 
                            topLeft.y, 
                            topLeft.z + (m_cellHeight / 2));
    }
    
    // This is to draw the grid on the scene view, but it got glitches, and only draw the full grid when starts the game.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_startPos, 2.0f);

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;

        // 2D Plane at X-Z coordinate, traverse from top left to right bottom.
        float row = 0.0f;

        for (int r = 0; r <= m_rows; r++)
        {
            // draw rows
            Gizmos.DrawLine(new Vector3(StartPos.x + row, StartPos.y, StartPos.z),
                            new Vector3(StartPos.x + row, StartPos.y, StartPos.z + m_width));

            float col = 0.0f;

            for (int c = 0; c <= m_cols; c++)
            {
                // draw cols
                Gizmos.DrawLine(new Vector3(StartPos.x + row, StartPos.y, StartPos.z + col),
                                new Vector3(StartPos.x + m_height, StartPos.y, StartPos.z + col));

                col += m_cellWidth;
            }

            row += m_cellHeight;
        }
    }

    

}
