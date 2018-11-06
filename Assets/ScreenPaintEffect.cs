using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenPaintEffect : MonoBehaviour
{
    
    //着色器和材质实例
    public Shader CurShader; // oil shader
    private Material CurMaterial;


    public Shader CurShader2; // repaint shader
    private Material CurMaterial2;

    public Camera additionalCamera;


    //两个参数值
    [Range(0, 5), Tooltip("分辨率比例值")]
    public float ResolutionValue = 0.9f;
    [Range(1, 30), Tooltip("半径的值，决定了迭代的次数")]
    public int RadiusValue = 5;

    //两个用于调节参数的中间变量
    public static float ChangeValue;
    public static int ChangeValue2;



    private RenderTexture tempRT;

    private RenderTexture tempRT2;

    Material material
    {
        get
        {
            if (CurMaterial == null)
            {
                CurMaterial = new Material(CurShader);
                CurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return CurMaterial;
        }
    }


    Material material2
    {
        get
        {
            if (CurMaterial2 == null)
            {
                CurMaterial2 = new Material(CurShader2);
                CurMaterial2.hideFlags = HideFlags.HideAndDontSave;
            }
            return CurMaterial2;
        }
    }

    private void Awake()
    {
        SetupAddtionalCamera();
    }

    private void SetupAddtionalCamera()
    {
        additionalCamera.CopyFrom(Camera.main);
        additionalCamera.clearFlags = CameraClearFlags.Color;
        additionalCamera.backgroundColor = Color.black;
        additionalCamera.cullingMask = 1 << LayerMask.NameToLayer("Player");
    }



    void Start()
    {
        //依次赋值
        ChangeValue = ResolutionValue;
        ChangeValue2 = RadiusValue;

        //找到当前的Shader文件
        CurShader = Shader.Find("Unfettered/OilPaintBlurPostEffect");
        CurShader2 = Shader.Find("Unfettered/OilPaintCombine");

        //判断当前设备是否支持屏幕特效
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }


    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        //着色器实例不为空，就进行参数设置
        if (CurShader != null && CurShader2 != null)
        {


           // tempRT = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0);
            //tempRT2 = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0);
            //additionalCamera.targetTexture = tempRT;

           


            //给Shader中的外部变量赋值
            material.SetFloat("_ResolutionValue", ResolutionValue);
            material.SetInt("_Radius", RadiusValue);
            material.SetVector("_ScreenResolution", new Vector4(sourceTexture.width, sourceTexture.height, 0.0f, 0.0f));
            
            //additionalCamera.Render();

           

            //Graphics.Blit(tempRT, tempRT2, material);

            material2.SetTexture("_SceneTex", sourceTexture);
            
            Graphics.Blit(sourceTexture, destTexture, material);

            //tempRT.Release();
        }

        //着色器实例为空，直接拷贝屏幕上的效果。此情况下是没有实现屏幕特效的
        else
        {
            //直接拷贝源纹理到目标渲染纹理
            Graphics.Blit(sourceTexture, destTexture);
        }


    }


    void OnValidate()
    {
        //将编辑器中的值赋值回来，确保在编辑器中值的改变立刻让结果生效
        ChangeValue = ResolutionValue;
        ChangeValue2 = RadiusValue;
    }
    // Update is called once per frame
    void Update()
    {
        //若程序在运行，进行赋值
        if (Application.isPlaying)
        {
            //赋值
            ResolutionValue = ChangeValue;
            RadiusValue = ChangeValue2;
        }
        //若程序没有在运行，去寻找对应的Shader文件
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            CurShader = Shader.Find("Unfettered/OilPaintBlurPostEffect");
            CurShader2 = Shader.Find("Unfettered/OilPaintCombine");
        }
#endif

    }

    void OnDisable()
    {
        if (CurMaterial)
        {
            //立即销毁材质实例
            DestroyImmediate(CurMaterial);
        }

    }
}


