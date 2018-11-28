using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateGrassTool : EditorWindow
{
    [MenuItem("Tools/CreateGrassTool")]
    static void Init()
    {
        CreateGrassTool sc = (CreateGrassTool)EditorWindow.GetWindow(typeof(CreateGrassTool));
    }


    int width = 1;
    int len = 1;
    string padding = "3";
    GameObject obj;
    private List<Vector3> vertices1;
    private List<int> triangles1;
    private List<Vector2> uv1;
    List<Color> colors1;
    string radius = "3";
    private void OnDestroy()
    {
        if (obj != null)
        {
            GameObject.DestroyImmediate(obj);
        }
    }
    string times = "50";
    string grassUnitWidth = "4";
    string grassUnitHeight = "40";
    string segment = "5";
    void OnGUI()
    {
        GUILayout.Label("Grass Setting", EditorStyles.boldLabel);
        times = EditorGUILayout.TextField("Num", times);

        padding = EditorGUILayout.TextField("Range", padding);
        grassUnitWidth = EditorGUILayout.TextField("grassUnitWidth", grassUnitWidth);
        segment = EditorGUILayout.TextField("segment", segment);
        if (GUILayout.Button("Create"))
        {
            if (obj != null )
            {
                if(obj.GetComponent<MeshFilter>() != null)
                {
                    GameObject.DestroyImmediate(obj.GetComponent<MeshFilter>().sharedMesh);
                }
                
                GameObject.DestroyImmediate(obj);
            }

            obj = new GameObject();
            obj.transform.position = Vector3.zero;
            obj.name = "grass";
            Mesh mesh = new Mesh();
            mesh.Clear();
            float paddingF = float.Parse(padding);
            int seg = int.Parse(segment);
            int wNum = width * seg;
            int lNum = len;
            int timesNum = int.Parse(times);
            int vertNum = (wNum + 1) * (lNum + 1) * timesNum * timesNum;
            if (vertNum > 65535)
            {
                Debug.LogError("Vert num can not be more than 65536");
                return;
            }
            vertices1 = new List<Vector3>();
            uv1 = new List<Vector2>();
            colors1 = new List<Color>();
            int num = 0;
            triangles1 = new List<int>();
            int startIndex = 0;
            for(int t = 0; t < timesNum; t++)
            {
                for(int k = 0; k < timesNum; k++)
                {
                    Vector3[] vertices = new Vector3[(wNum + 1) * (lNum + 1)];
                    Color[] colors = new Color[(wNum + 1) * (lNum + 1)];
                    Vector2[] uv = new Vector2[(wNum + 1) * (lNum + 1)];
                    int[] triangles = new int[wNum * lNum * 6];
                    float padding1 = paddingF / 2;
                    Vector3 off = new Vector3(-padding1, 0, -padding1);
                    off += new Vector3(paddingF / timesNum * t, 0f, paddingF / timesNum * k);
                    Vector3 basePos = off;
                    float xr = Random.Range(-paddingF / timesNum, paddingF / timesNum);
                    float zr = Random.Range(-paddingF / timesNum, paddingF / timesNum);
                    basePos += new Vector3(xr, 0f, zr);
                    int index = 0;
                    for (int i = 0; i < wNum; i++)
                    {
                        for (int j = 0; j < lNum; j++)
                        {
                            int line = lNum + 1;
                            int self = j + (i * line) + startIndex;

                            triangles[index] = self;
                            triangles[index + 1] = self + line + 1;
                            triangles[index + 2] = self + line;
                            triangles[index + 3] = self;
                            triangles[index + 4] = self + 1;
                            triangles[index + 5] = self + 1 + line;
                            index += 6;
                        }
                    }

                    float anglez = Random.Range(-10f, 10f);
                    Vector3 normalDir = Quaternion.Euler(0f, 0f, anglez) * Vector3.up;
                    Vector3 rightDir = Quaternion.Euler(0f, 0f, anglez) * Vector3.right;
                    float weightR = Random.Range(-float.Parse(grassUnitWidth) / 2f, 0f);
                    float lr = Random.Range(0f, 1f);
                    Color color = Color.Lerp(new Color(0.6f, 0.9f, 0f, 1f), new Color(1f, 0.6f, 0.4f, 1f), lr);
                    float heightR = Random.Range(0.7f, 1.1f);
                    for (int j = 0; j < wNum + 1; j++)
                    {
                        for (int i = 0; i < lNum + 1; i++)
                        {
                            int line = lNum + 1;
                            float w = (weightR + float.Parse(grassUnitWidth)) * i / timesNum;
                            if(i == 0)
                            {
                                w += j * w * 0.9f / seg;
                            }
                            else
                            {
                                w -= j * w * 0.9f / seg;
                            }
                            int index1 = j * line + i;
                            vertices[index1] = basePos + (w) * rightDir +  (heightR * float.Parse(grassUnitHeight) * j / seg / 70f) * normalDir;
                            //vertices[i * line + j] = basePos + Vector3.right * float.Parse(grassUnitWidth) *i / timesNum + float.Parse(grassUnitHeight) * j / timesNum * Vector3.up;
                            uv[index1] = new Vector2(1f * i / lNum, 1f * j / wNum);
                            colors[index1] = color;
                        }
                    }

                    vertices1.AddRange(vertices);
                    colors1.AddRange(colors);
                    uv1.AddRange(uv);
                    triangles1.AddRange(triangles);
                    startIndex += vertices.Length;
                }
                
            }
           
            
            mesh.vertices = vertices1.ToArray();
            mesh.triangles = triangles1.ToArray();
            mesh.uv = uv1.ToArray();
            mesh.colors = colors1.ToArray();
            Vector3[] normals = new Vector3[mesh.vertices.Length];
            mesh.normals = normals;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            obj.AddComponent<MeshFilter>().sharedMesh = mesh;
            MeshRenderer render = obj.AddComponent<MeshRenderer>();
            render.sharedMaterial = AssetDatabase.LoadAssetAtPath("Assets/MobileGrass/Material/grass.mat", typeof(Material)) as Material;
            render.sharedMaterial.SetFloat("_GrassSeg", seg);
            render.sharedMaterial.SetFloat("_GrassNum", float.Parse(times));
            render.sharedMaterial.SetFloat("_GrassRange", float.Parse(padding));
            GameObject newObj = GameObject.Instantiate(obj);
            newObj.transform.parent = obj.transform;
            newObj.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localScale = Vector3.one;
        }

        if(GUILayout.Button("Save"))
        {
            if(obj != null)
            {
                AssetDatabase.CreateAsset(obj.GetComponent<MeshFilter>().sharedMesh, "Assets/MobileGrass/Model/grass.asset");
                PrefabUtility.CreatePrefab("Assets/MobileGrass/Prefab/MobileGrass.prefab", obj);
            }
           
        }
    }
}
