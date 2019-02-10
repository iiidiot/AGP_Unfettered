using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
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
    }

    // Use this for initialization
    void Start ()
    {
        m_visited = new bool[m_rows, m_cols];

        for(int r = 0; r < m_rows; r++)
        {
            for(int c = 0; c < m_cols; c++)
            {
                m_visited[r, c] = false;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // Allow designers to move around the grid and display on the scene view when the game is running
        m_startPos = transform.position;
        m_width = m_cols * m_cellWidth;
        m_height = m_rows * m_cellHeight;      
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

    public void SetVisited(Vector3 mousePos)
    {
        int row = 0;
        int col = 0;

        Vector3 topLeft = GetTopLeftPosition(mousePos, out row, out col);

        m_visited[row, col] = true;
        
        Debug.Log("Mouse Position = " + topLeft.x + " , " + topLeft.z);
        Debug.Log(row + " , " + col);
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

    public Vector3 GetNearestPointOnGrid(Vector3 pos)
    {
        pos -= transform.position;

        int xOffset = Mathf.RoundToInt(pos.x / m_cellWidth);
        int yOffset = Mathf.RoundToInt(pos.y / m_cellWidth);
        int zOffset = Mathf.RoundToInt(pos.z / m_cellHeight);

        Vector3 result = new Vector3(xOffset * m_cellWidth,
                                     yOffset * m_cellWidth,
                                     zOffset * m_cellHeight);

        result += transform.position;

        return result;

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
