using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Random = UnityEngine.Random;

public class Painting : MonoBehaviour
{
    public int brush_size = 32;
    public Material mat;     //给定的shader新建材质,不需要设置，空mat就行
    public Texture2D brushTypeTexture;   //画笔纹理，半透明

    public Color brushColor = Color.black;
    public RawImage raw;                   //使用UGUI的RawImage显示，方便进行添加UI
    public RectTransform rangePanel;
    public int num = 50;

    private RenderTexture texRender;   //画布
   
    
    private Camera mainCamera;
    private float brushScale = 0.5f;
    
    private float lastDistance;
    private Vector3[] PositionArray = new Vector3[3];
    private int a = 0;
    private Vector3[] PositionArray1 = new Vector3[4];
    private int b = 0;
    private float[] speedArray = new float[4];
    private int s = 0;


    private List<Vector2> pointsArray;

    private int[][] points_path;

    void Start()
    {
        raw.enabled = true;
        //points_path = new int[Screen.width, Screen.height];
        texRender = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        Clear(texRender);

        TextureScale.Bilinear(brushTypeTexture, brush_size, brush_size);
    }

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;
    void Update()
    {

        if (Input.GetMouseButton(0))
        {

            Vector3 cur_p = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            print(cur_p);
            //pointsArray.Add(new Vector2(cur_p.x,cur_p.y));
            OnMouseMove(cur_p);
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }
        DrawImage();

        //保存图片
        if (Input.GetKeyDown(KeyCode.F))
        {
            SavePic();
        }

        //图像匹配
        if (Input.GetKeyDown(KeyCode.V))
        {
            //string standard_path = Application.streamingAssetsPath + "/Fu/fireball.jpg";

            //Texture2D standard_tx = new Texture2D(texRender.width, texRender.height);
            //standard_tx.LoadImage(ImageProcess.getImageByte(standard_path));

            //RenderTexture.active = texRender;

            //Texture2D t = new Texture2D(texRender.width, texRender.height, TextureFormat.ARGB32, false);
            //t.ReadPixels(new Rect(0, 0, texRender.width, texRender.height), 0, 0);
            //t.Apply();
            ////0.02上 0.015上上 0.03下 0.04失败
            //print("误差值：");
            //print(ImageProcess.MAD(standard_tx, t, standard_tx.width, standard_tx.height));
        }
    }

    void SavePic()
    {
        RenderTexture prev = RenderTexture.active;

        RenderTexture.active = texRender;

        Texture2D t = new Texture2D(texRender.width, texRender.height, TextureFormat.ARGB32, false);
        t.ReadPixels(new Rect(0, 0, texRender.width, texRender.height), 0, 0);
        t.Apply();
        TextureScale.Bilinear(t,64,64);

        string path = Application.streamingAssetsPath + "/draw.jpg";
        print(path);
        FileStream file = File.Create(path);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(t.EncodeToJPG());
        file.Close();
        Texture2D.DestroyImmediate(t);
        t = null;
        RenderTexture.active = prev;
    }

    void OnMouseUp()
    {
        startPosition = Vector3.zero;
        //brushScale = 0.5f;
        a = 0;
        b = 0;
        s = 0;
    }
    //设置画笔宽度
    float SetScale(float distance)
    {
        float Scale = 0;
        if (distance < 100)
        {
            Scale = 0.8f - 0.005f * distance;
        }
        else
        {
            Scale = 0.425f - 0.00125f * distance;
        }
        if (Scale <= 0.05f)
        {
            Scale = 0.05f;
        }
        return Scale;
    }

    void OnMouseMove(Vector3 pos)
    {
        if (startPosition == Vector3.zero)
        {
            startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }

        endPosition = pos;
        float distance = Vector3.Distance(startPosition, endPosition);
        brushScale = SetScale(distance);
        ThreeOrderBézierCurse(pos, distance, 4.5f);

        startPosition = endPosition;
        lastDistance = distance;
    }

    void Clear(RenderTexture destTexture)
    {
        Graphics.SetRenderTarget(destTexture);
        GL.PushMatrix();
        GL.Clear(true, true, new Color(1,1,1,0));
        GL.PopMatrix();
    }

    void DrawBrush(RenderTexture destTexture, int x, int y, Texture sourceTexture, Color color, float scale)
    {
        DrawBrush(destTexture, new Rect(x, y, sourceTexture.width, sourceTexture.height), sourceTexture, color, scale);
    }
    void DrawBrush(RenderTexture destTexture, Rect destRect, Texture sourceTexture, Color color, float scale)
    {
        float left = destRect.xMin - destRect.width * scale / 2.0f;
        float right = destRect.xMin + destRect.width * scale / 2.0f;
        float top = destRect.yMin - destRect.height * scale / 2.0f;
        float bottom = destRect.yMin + destRect.height * scale / 2.0f;

        Graphics.SetRenderTarget(destTexture);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetTexture("_MainTex", brushTypeTexture);
        mat.SetColor("_Color", color);
        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(left / Screen.width, top / Screen.height, 0);
        GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(right / Screen.width, top / Screen.height, 0);
        GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(right / Screen.width, bottom / Screen.height, 0);
        GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(left / Screen.width, bottom / Screen.height, 0);

        GL.End();
        GL.PopMatrix();
    }
    bool bshow = true;
    void DrawImage()
    {
        raw.texture = texRender;
    }
    public void OnClickClear()
    {
        Clear(texRender);
    }

    //二阶贝塞尔曲线
    public void TwoOrderBézierCurse(Vector3 pos, float distance)
    {
        PositionArray[a] = pos;
        a++;
        if (a == 3)
        {
            for (int index = 0; index < num; index++)
            {
                Vector3 middle = (PositionArray[0] + PositionArray[2]) / 2;
                PositionArray[1] = (PositionArray[1] - middle) / 2 + middle;

                float t = (1.0f / num) * index / 2;
                Vector3 target = Mathf.Pow(1 - t, 2) * PositionArray[0] + 2 * (1 - t) * t * PositionArray[1] +
                                 Mathf.Pow(t, 2) * PositionArray[2];
                float deltaSpeed = (float)(distance - lastDistance) / num;
                DrawBrush(texRender, (int)target.x, (int)target.y, brushTypeTexture, brushColor, SetScale(lastDistance + (deltaSpeed * index)));
            }
            PositionArray[0] = PositionArray[1];
            PositionArray[1] = PositionArray[2];
            a = 2;
        }
        else
        {
            DrawBrush(texRender, (int)endPosition.x, (int)endPosition.y, brushTypeTexture,
                brushColor, brushScale);
        }
    }
    //三阶贝塞尔曲线，获取连续4个点坐标，通过调整中间2点坐标，画出部分（我使用了num/1.5实现画出部分曲线）来使曲线平滑;通过速度控制曲线宽度。
    private void ThreeOrderBézierCurse(Vector3 pos, float distance, float targetPosOffset)
    {
        //记录坐标
        PositionArray1[b] = pos;
        b++;
        //记录速度
        speedArray[s] = distance;
        s++;
        if (b == 4)
        {
            Vector3 temp1 = PositionArray1[1];
            Vector3 temp2 = PositionArray1[2];

            //修改中间两点坐标
            Vector3 middle = (PositionArray1[0] + PositionArray1[2]) / 2;
            PositionArray1[1] = (PositionArray1[1] - middle) * 1.5f + middle;
            middle = (temp1 + PositionArray1[3]) / 2;
            PositionArray1[2] = (PositionArray1[2] - middle) * 2.1f + middle;

            for (int index1 = 0; index1 < num / 1.5f; index1++)
            {
                float t1 = (1.0f / num) * index1;
                Vector3 target = Mathf.Pow(1 - t1, 3) * PositionArray1[0] +
                                 3 * PositionArray1[1] * t1 * Mathf.Pow(1 - t1, 2) +
                                 3 * PositionArray1[2] * t1 * t1 * (1 - t1) + PositionArray1[3] * Mathf.Pow(t1, 3);
                //float deltaspeed = (float)(distance - lastDistance) / num;
                //获取速度差值（存在问题，参考）
                float deltaspeed = (float)(speedArray[3] - speedArray[0]) / num;
                //float randomOffset = Random.Range(-1/(speedArray[0] + (deltaspeed * index1)), 1 / (speedArray[0] + (deltaspeed * index1)));
                //模拟毛刺效果
                float randomOffset = Random.Range(-targetPosOffset, targetPosOffset);
                DrawBrush(texRender, (int)(target.x + randomOffset), (int)(target.y + randomOffset), brushTypeTexture, brushColor, SetScale(speedArray[0] + (deltaspeed * index1)));
            }

            PositionArray1[0] = temp1;
            PositionArray1[1] = temp2;
            PositionArray1[2] = PositionArray1[3];

            speedArray[0] = speedArray[1];
            speedArray[1] = speedArray[2];
            speedArray[2] = speedArray[3];
            b = 3;
            s = 3;
        }
        else
        {
            DrawBrush(texRender, (int)endPosition.x, (int)endPosition.y, brushTypeTexture,
                brushColor, brushScale);
        }

    }

}
