#define DEBUG_ON

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager s_instance = null;
    
    public FarmItemsSO farmItemsSO;

    public GameObject tilePrefab;

    [SerializeField]
    private Color m_validColor;

    [SerializeField]
    private Color m_invalidColor;

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
    
    private GameObject m_tilePrefab;

    private Vector3 m_preCenterPos;

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
        if(!s_instance)
        {
            s_instance = this;
        }
        else if(s_instance)
        {
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        m_preCenterPos = Vector3.zero;

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

        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            m_mousePos = hitInfo.point;

            int row = 0;
            int col = 0;

            Vector3 topLeft = GetTopLeftPosition(m_mousePos, out row, out col);
            
            Vector3 centerPos = GetCenterPosition(topLeft);

            // If it's an empty cell
            if (IsValidPosition(m_mousePos, row, col, topLeft))
            {
                // if the tile doesn't exist, we create a new tile
                if(m_preCenterPos != centerPos)
                {
                    // update the previous center position for record
                    m_preCenterPos = centerPos;

                    // reset the tile 
                    Destroy(m_tilePrefab);
                    
                }

                // If it doesn't exist, create one. 
                if(!m_tilePrefab)
                {
                    InitTile(m_validColor);
                }
                

                //DrawTile(m_validColor, topLeft);

                // If mouse button has pressed at this point, add the item.
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceObject(row, col, topLeft);
                }
            }

            // If it's not an empty cell, but within the range of the grid
            else if(IsValidPosition(m_mousePos, row, col))
            {
                // if the tile doesn't exist, we create a new tile
                if (m_preCenterPos != centerPos)
                {
                    m_preCenterPos = centerPos;

                    // reset the tile 
                    Destroy(m_tilePrefab);

                }

                // If it doesn't exist, create one. 
                if (!m_tilePrefab)
                {
                    InitTile(m_invalidColor);
                }
            }

            // mouse position is out of bound
            else
            {
                Destroy(m_tilePrefab);
            }
        }

        if(m_currentDay != TimeManager.s_days)
        {
            UpdateDays();
            UpdateField();

            m_currentDay = TimeManager.s_days;
        } 
    }

    void InitTile(Color color)
    {
        m_tilePrefab = Instantiate(tilePrefab, m_preCenterPos, Quaternion.identity);

        float currentZ = m_tilePrefab.GetComponent<Renderer>().bounds.size.z;
        float currentX = m_tilePrefab.GetComponent<Renderer>().bounds.size.x;

        Vector3 scale = m_tilePrefab.transform.localScale;

        // z = width, x = height
        scale.z = m_cellWidth * scale.z / currentZ;
        scale.x = m_cellHeight * scale.x / currentX;

        // Set the scale of the tile to fit the cell size of the grid
        m_tilePrefab.transform.localScale = scale;

        // Set the color of the tile
        m_tilePrefab.GetComponent<Renderer>().material.color = color;
    }

    //private void AddItem()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit hitInfo;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //        if (Physics.Raycast(ray, out hitInfo))
    //        {
    //            m_mousePos = hitInfo.point;

    //            int row = 0;
    //            int col = 0;

    //            Vector3 topLeft = Vector3.zero;

    //            if (IsValidPosition(m_mousePos, out row, out col, out topLeft))
    //            {
    //                PlaceObject(row, col, topLeft);
    //            }
    //        }
    //    }
    //}

    private void PlaceObject(int row, int col, Vector3 topLeft)
    {
        SetVisited(m_mousePos, row, col, topLeft);
    }

    // Add new item to the field, everything is initialized.
    private void SetVisited(Vector3 mousePos, int row, int col, Vector3 topLeft)
    {
        Vector3 centerPos = GetCenterPosition(topLeft);

        GameObject item = Instantiate(farmItemsSO.AppleItems[0], centerPos, Quaternion.identity);

        m_visited[row, col] = true;
        m_field[row, col] = item;
        m_days[row, col] = 0;

#if DEBUG_ON
        Debug.Log("Mouse Position = " + topLeft.x + " , " + topLeft.z);
        Debug.Log(row + " , " + col);
#endif
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

    //private Vector3 GetTopLeftPosition(Vector3 mousePos)
    //{
    //    float x = m_startPos.x;
    //    float z = m_startPos.z;

    //    while (x + m_cellWidth < mousePos.x)
    //    {
    //        x += m_cellHeight;
    //    }

    //    while (z + m_cellHeight < mousePos.z)
    //    {
    //        z += m_cellHeight;
    //    }

    //    return new Vector3(x, m_startPos.y, z);
    //}

    private Vector3 GetTopLeftPosition(Vector3 mousePos, out int row, out int col)
    {
        float x = m_startPos.x;
        float z = m_startPos.z;

        int r = 0;
        int c = 0;

        // z = width
        while (z + m_cellWidth < mousePos.z)
        {
            z += m_cellWidth;
            r++;
        }

        // x = height
        while (x + m_cellHeight < mousePos.x)
        {
            x += m_cellHeight;
            c++;
        }

        row = r;
        col = c;

        return new Vector3(x, m_startPos.y, z);
    }

    private bool IsValidPosition(Vector3 mousePos,
                                int row, int col,
                                Vector3 topLeft)
    {
        return (IsValidPosition(mousePos, row, col) && !m_visited[row, col]);
    }

    private bool IsValidPosition(Vector3 mousePos, int row, int col)
    {
        return (row < m_rows && row >= 0 && col < m_cols && col >= 0 &&
                mousePos.x >= m_startPos.x && mousePos.x < (m_startPos.x + m_height) &&
                mousePos.z >= m_startPos.z && mousePos.z < (m_startPos.z + m_width));
    }
    
    public Vector3 GetCenterPosition(Vector3 topLeft)
    {
        //Vector3 topLeft = GetTopLeftPosition(mousePos);

        //Vector3 topRight = new Vector3(topLeft.x, topLeft.y, topLeft.z + m_cellWidth);
        //Vector3 bottomLeft = new Vector3(topLeft.x + m_cellHeight, topLeft.y, topLeft.z);
        //Vector3 bottomRight = new Vector3(topLeft.x + m_cellHeight, topLeft.y, topLeft.z);

        //return new Vector3( topLeft.x + (m_cellWidth / 2), 
        //                    topLeft.y, 
        //                    topLeft.z + (m_cellHeight / 2));

        return new Vector3( topLeft.x + (m_cellHeight / 2),
                            topLeft.y,
                            topLeft.z + (m_cellWidth / 2));
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
