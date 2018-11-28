using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {
    public float unit = 1f;
    public float minVertexDistance = 0.5f;
    public float lefttime = 5f;
    public Material material;
    int layer;
    Vector3 upDir = Vector3.up;
    List<TrailPoint> points = new List<TrailPoint>();
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    List<Color> colors = new List<Color>();
    Mesh mesh;
	// Use this for initialization
	void Start () {
        layer = LayerMask.NameToLayer("Units");
        mesh = new Mesh();
	}
	
	// Update is called once per frame
	void Update () {
		while(points.Count > 0)
        {
            if(Time.time - points[0].creationTime > lefttime)
            {
                points.RemoveAt(0);
            }
            else
            {
                break;
            }
        }

        Vector3 pos = transform.position;
        bool addedPoint = false;
        if(points.Count == 0 || Vector3.Distance(points[points.Count - 1].pos, pos) > minVertexDistance)
        {
            points.Add(new TrailPoint(pos, Time.time));
            addedPoint = true;
        }

        List<TrailPoint> renderPoints = new List<TrailPoint>(points);
        if(!addedPoint)
        {
            renderPoints.Add(new TrailPoint(pos, Time.time));
        }

        if(renderPoints.Count < 2)
        {
            return;
        }

        mesh.Clear();
        if(renderPoints.Count < 2)
        {
            return;
        }

        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        float uvFactor = 1f / (renderPoints.Count - 1);
        for(int i = 0; i < renderPoints.Count; i++)
        {
            TrailPoint point = renderPoints[i];
            if(i == 0)
            {
                AddPoint(point, renderPoints[i + 1].pos - point.pos, 0);
                continue;
            }

            TrailPoint lastPoint = renderPoints[i - 1];
            if(i == renderPoints.Count - 1)
            {
                AddPoint(point, point.pos - lastPoint.pos, 1f);
                break;
            }

            TrailPoint nextPoint = renderPoints[i + 1];
            AddPoint(point, nextPoint.pos - lastPoint.pos, i * uvFactor);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        Graphics.DrawMesh(mesh, Matrix4x4.identity, material, layer);
    }

    void AddPoint(TrailPoint point, Vector3 direction, float uv)
    {
        float lifePercent = (Time.time - point.creationTime) / lefttime;
        float halfWidth = unit;// (1 - lifePercent) * unit;
        float normalStrength = 1 - lifePercent;
        Vector2 dir = new Vector2();
        dir.x = direction.x * 0.5f + 0.5f;
        dir.y = direction.z * 0.5f + 0.5f;
        Color normalStrengthColor = new Color(dir.x, normalStrength * 0.5f, dir.y, 1f);
        Vector3 pos = point.pos;
        Vector3 right = Vector3.Cross(upDir, direction);
        vertices.Add(pos - right * halfWidth);
        vertices.Add(pos + right * halfWidth);
        colors.Add(normalStrengthColor);
        colors.Add(normalStrengthColor);
        int lastVert = vertices.Count - 1;
        if(lastVert >= 3)
        {
            triangles.Add(lastVert - 1);
            triangles.Add(lastVert);
            triangles.Add(lastVert - 2);
            triangles.Add(lastVert - 2);
            triangles.Add(lastVert - 3);
            triangles.Add(lastVert - 1);
        }
    }
}

[Serializable]
public struct TrailPoint
{
    public Vector3 pos;
    public float creationTime;

    public TrailPoint(Vector3 pos, float creationTime)
    {
        this.pos = pos;
        this.creationTime = creationTime;
    }
}
