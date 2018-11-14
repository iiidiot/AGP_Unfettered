//////////////////////////////////////////////////////
// MK Glow	    			                        //
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de | www.michaelkremmel.store //
// Copyright © 2017 All rights reserved.            //
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Glow
{
    [ExecuteInEditMode]
    [ImageEffectAllowedInSceneView]
    [RequireComponent(typeof(Camera))]
    public class MKGlow : MonoBehaviour
    {
        #region C
        private static float[] gaussFilter = new float[11]
        {
            0.50f, 0.5398f, 0.5793f, 0.6179f, 0.6554f, 0.6915f, 0.7257f, 0.7580f, 0.7881f, 0.8159f, 0.8413f
        };
        private const float GLOW_INTENSITY_INNER_MULT = 25.0f;
        private const float GLOW_INTENSITY_OUTER_MULT = 50.0f;
        private const float BLUR_SPREAD_INNTER_MULT = 10.0f;
        private const float BLUR_SPREAD_OUTER_MULT = 50.0f;
        private const string LENS_KEYWORD = "_MK_LENS";
        #endregion

        #region P
#if UNITY_EDITOR
#pragma warning disable 414
        [SerializeField]
        private bool showMainBehavior = true;
        [SerializeField]
        private bool showInnerGlowBehavior = false;
        [SerializeField]
        private bool showOuterGlowBehavior = false;
        [SerializeField]
        private bool showLensBehavior = false;
#pragma warning restore 414
#endif
        private RenderTextureFormat rtFormat = RenderTextureFormat.Default;

        [SerializeField]
        private Shader blurShader;
        [SerializeField]
        private Shader compositeShader;
        [SerializeField]
        private Shader selectiveRenderShader;
        [SerializeField]
        private Shader extractLuminanceShader;

        private Material compositeMaterial;
        private Material blurMaterial;
        private Material extractLuminanceMaterial;

        [SerializeField]
        private Camera Cam
        {
            get { return GetComponent<Camera>(); }
        }

        [SerializeField]
        [Tooltip("Lens Texture")]
        private Texture lensTex;
        [SerializeField]
        [Tooltip("Lens Intensity")]
        private float lensIntensity;
        [SerializeField]
        [Tooltip("recommend: -1")]
        private LayerMask glowLayer = -1;
        [SerializeField]
        [Tooltip("Selective = to specifically bring objects to glow, Fullscreen = complete screen glows")]
        private GlowType glowType = GlowType.Luminance;
        [SerializeField]
        [Tooltip("The glows coloration")]
        private Color glowTint = new Color(1, 1, 1, 0);
        [SerializeField]
        [Tooltip("Inner width of the glow effect")]
        private float blurSpreadInner = 0.60f;
        [SerializeField]
        [Tooltip("Outer width of the glow effect")]
        private float blurSpreadOuter = 0.70f;
        [SerializeField]
        [Tooltip("Number of used blurs. Lower iterations = better performance")]
        private int blurIterations = 5;
        [SerializeField]
        [Tooltip("The global inner luminous intensity")]
        private float glowIntensityInner = 0.40f;
        [SerializeField]
        [Tooltip("The global outer luminous intensity")]
        private float glowIntensityOuter = 0.00f;
        [SerializeField]
        [Tooltip("Downsampling steps of the blur. Higher samples = better performance, but gains more flickering")]
        private int samples = 2;
        [SerializeField]
        [Tooltip("Threshold for glow")]
        private float threshold = 1.0f;
        [SerializeField]
        [Tooltip("Enable and disable the lens")]
        private bool useLens = false;
        #endregion

        #region GET_SET
        public bool UseLens
        {
            get { return useLens; }
            set { useLens = value;}
        }
        public Texture LensTex
        {
            get { return lensTex; }
            set { lensTex = value; }
        }
        public float LensIntensity
        {
            get { return lensIntensity; }
            set { lensIntensity = value; }
        }
        public LayerMask GlowLayer
        {
            get { return glowLayer; }
            set { glowLayer = value; }
        }
        public GlowType GlowType
        {
            get { return glowType; }
            set { glowType = value;}
        }
        public Color GlowTint
        {
            get { return glowTint; }
            set { glowTint = value; }
        }
        public int Samples
        {
            get { return samples; }
            set { samples = value; }
        }
        public int BlurIterations
        {
            get { return blurIterations; }
            set
            {
                blurIterations = Mathf.Clamp(value, 0, 10);
            }
        }
        public float GlowIntensityInner
        {
            get { return glowIntensityInner; }
            set { glowIntensityInner = value; }
        }
        public float GlowIntensityOuter
        {
            get { return glowIntensityOuter; }
            set { glowIntensityOuter = value; }
        }
        public float BlurSpreadInner
        {
            get { return blurSpreadInner; }
            set { blurSpreadInner = value; }
        }
        public float BlurSpreadOuter
        {
            get { return blurSpreadOuter; }
            set { blurSpreadOuter = value; }
        }
        public float Threshold
        {
            get { return threshold; }
            set { threshold = value; }
        }
        #endregion

        private void Awake()
        {
            GlowInitialize();
        }

        private void Reset()
        {
            GlowInitialize();
        }

        private void GlowInitialize()
        {
            Cleanup();
            SetupShaders();
            SetupMaterials();
        }

        private void SetupShaders()
        {
            if (!blurShader)
                blurShader = Shader.Find("Hidden/MK/Glow/Blur");

            if (!compositeShader)
                compositeShader = Shader.Find("Hidden/MK/Glow/Composite");

            if (!selectiveRenderShader)
                selectiveRenderShader = Shader.Find("Hidden/MK/Glow/SelectiveRender");

            if (!extractLuminanceShader)
                extractLuminanceShader = Shader.Find("Hidden/MK/Glow/ExtractLuminance");
        }

        private void Cleanup()
        {
            DestroyImmediate(selectiveGlowCamera);
            DestroyImmediate(SelectiveGlowCameraObject);
        }

        private void OnEnable()
        {
            GlowInitialize();
        }

        private void OnDisable()
        {
            Cleanup();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

#if UNITY_2017_2_OR_NEWER
        RenderTexture GetTemporaryRT(int width, int height, VRTextureUsage vrUsage)
        {
            return RenderTexture.GetTemporary(width, height, 0, rtFormat, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.None, vrUsage);
        }
#else
        RenderTexture GetTemporaryRT(int width, int height)
        {
            return RenderTexture.GetTemporary(width, height, 0, rtFormat, RenderTextureReadWrite.Default, 1);
        }
#endif

        private void Blur(RenderTexture main, RenderTexture tmpMain, RenderTexture sec, RenderTexture tmpSec)
        {
            for (int i = 1; i <= blurIterations; i++)
            {
                float offsetInner = i * (blurSpreadInner * BLUR_SPREAD_INNTER_MULT) / blurIterations / samples;
                offsetInner *= gaussFilter[i];

                blurMaterial.SetFloat("_Offset", offsetInner);
                Graphics.Blit(main, tmpMain, blurMaterial);
                blurMaterial.SetFloat("_Offset", offsetInner);
                Graphics.Blit(tmpMain, main, blurMaterial);


                if (blurSpreadOuter > 0.0f || glowIntensityOuter > 0.0f)
                {
                    float offsetOuter = i * (blurSpreadOuter * BLUR_SPREAD_OUTER_MULT) / blurIterations / samples;
                    offsetOuter *= gaussFilter[i];

                    blurMaterial.SetFloat("_Offset", offsetOuter);
                    Graphics.Blit(sec, tmpSec, blurMaterial);

                    blurMaterial.SetFloat("_Offset", offsetOuter);
                    Graphics.Blit(tmpSec, sec, blurMaterial);
                }
            }
        }

        private void Blur(RenderTexture main, RenderTexture tmpMain)
        {
            for (int i = 1; i <= blurIterations; i++)
            {
                float offsetInner = i * (blurSpreadInner * BLUR_SPREAD_INNTER_MULT) / blurIterations / samples;
                offsetInner *= gaussFilter[i];

                blurMaterial.SetFloat("_Offset", offsetInner);
                Graphics.Blit(main, tmpMain, blurMaterial);

                blurMaterial.SetFloat("_Offset", offsetInner);
                Graphics.Blit(tmpMain, main, blurMaterial);
            }
        }

        private void SetupMaterials()
        {
            if (blurMaterial == null)
            {
                blurMaterial = new Material(blurShader);
                blurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            if (extractLuminanceMaterial == null)
            {
                extractLuminanceMaterial = new Material(extractLuminanceShader);
                extractLuminanceMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            if (compositeMaterial == null)
            {
                compositeMaterial = new Material(compositeShader);
                compositeMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        private Camera selectiveGlowCamera;
        private GameObject selectiveGlowCameraObject;

        private GameObject SelectiveGlowCameraObject
        {
            get
            {
                if (!selectiveGlowCameraObject)
                {
                    selectiveGlowCameraObject = new GameObject("selectiveGlowCameraObject");
                    selectiveGlowCameraObject.AddComponent<Camera>();
                    selectiveGlowCameraObject.hideFlags = HideFlags.HideAndDontSave;
                    SelectiveGlowCamera.orthographic = false;
                    SelectiveGlowCamera.enabled = false;
                    SelectiveGlowCamera.renderingPath = RenderingPath.VertexLit;
                    SelectiveGlowCamera.hideFlags = HideFlags.HideAndDontSave;
                }
                return selectiveGlowCameraObject;
            }
        }
        private Camera SelectiveGlowCamera
        {
            get
            {
                if (selectiveGlowCamera == null)
                {
                    selectiveGlowCamera = SelectiveGlowCameraObject.GetComponent<Camera>();
                }
                return selectiveGlowCamera;
            }
        }

        private void SetupGlowCamera()
        {
            SelectiveGlowCamera.CopyFrom(Cam);
            SelectiveGlowCamera.depthTextureMode = DepthTextureMode.None;
            SelectiveGlowCamera.targetTexture = glowTexRaw;

            SelectiveGlowCamera.clearFlags = CameraClearFlags.SolidColor;
            SelectiveGlowCamera.rect = new Rect(0, 0, 1, 1);
            SelectiveGlowCamera.backgroundColor = new Color(0, 0, 0, 0);
            SelectiveGlowCamera.cullingMask = glowLayer;
            SelectiveGlowCamera.renderingPath = RenderingPath.VertexLit;
        }

        private void LuminanceGlow(RenderTexture src, RenderTexture dest, RenderTexture glowTexInner, RenderTexture tmpGlowTexInner, RenderTexture glowTexOuter, RenderTexture tmpGlowTexOuter)
        {
            Graphics.Blit(src, glowTexInner, extractLuminanceMaterial);
            Graphics.Blit(glowTexInner, glowTexOuter);

            Blur(glowTexInner, tmpGlowTexInner, glowTexOuter, tmpGlowTexOuter);

            compositeMaterial.SetTexture("_MKGlowTexInner", glowTexInner);
            compositeMaterial.SetTexture("_MKGlowTexOuter", glowTexOuter);
            Graphics.Blit(src, dest, compositeMaterial, 0);
        }

        private void FullScreenGlow(RenderTexture src, RenderTexture dest, RenderTexture glowTexInner, RenderTexture tmpGlowTexInner, RenderTexture glowTexOuter, RenderTexture tmpGlowTexOuter)
        {
            Graphics.Blit(src, glowTexInner);

            Blur(glowTexInner, tmpGlowTexInner);
            compositeMaterial.SetTexture("_MKGlowTexInner", glowTexInner);

            Graphics.Blit(src, dest, compositeMaterial, 1);
        }

        private RenderTexture glowTexRaw;
        private int srcWidth, srcHeight;

        private void SelectiveGlow(RenderTexture src, RenderTexture dest, RenderTexture glowTexInner, RenderTexture tmpGlowTexInner, RenderTexture glowTexOuter, RenderTexture tmpGlowTexOuter)
        {
            Graphics.Blit(glowTexRaw, glowTexInner);
            Graphics.Blit(glowTexRaw, glowTexOuter);

            Blur(glowTexInner, tmpGlowTexInner, glowTexOuter, tmpGlowTexOuter);

            compositeMaterial.SetTexture("_MKGlowTexInner", glowTexInner);
            compositeMaterial.SetTexture("_MKGlowTexOuter", glowTexOuter);
            Graphics.Blit(src, dest, compositeMaterial, 0);
        }

#if UNITY_2017_2_OR_NEWER
        private VRTextureUsage srcVRUsage = VRTextureUsage.TwoEyes;
#endif
        private void OnPostRender()
        {
            switch (glowType)
            {
                case GlowType.Luminance:
                    extractLuminanceMaterial.SetFloat("_Threshold", threshold);
                    break;
                case GlowType.Selective:
                    RenderTexture.ReleaseTemporary(glowTexRaw);
#if UNITY_2017_2_OR_NEWER
                    glowTexRaw = RenderTexture.GetTemporary((int)((Cam.pixelWidth) / samples), (int)((Cam.pixelHeight) / samples), 16, rtFormat, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.None, srcVRUsage);
#else
                    glowTexRaw = RenderTexture.GetTemporary((int)((Cam.pixelWidth) / samples), (int)((Cam.pixelHeight) / samples), 16, rtFormat, RenderTextureReadWrite.Default, 1);
#endif
                    SetupGlowCamera();
                    SelectiveGlowCamera.RenderWithShader(selectiveRenderShader, "RenderType");
                    break;
                case GlowType.Fullscreen:
                    break;
            }
            blurMaterial.SetFloat("_VRMult", Cam.stereoEnabled ? 0.5f : 1.0f);
            compositeMaterial.SetFloat("_GlowIntensityInner", glowIntensityInner * ((glowType != GlowType.Fullscreen) ? GLOW_INTENSITY_INNER_MULT * blurSpreadInner : 10.0f));
            compositeMaterial.SetFloat("_GlowIntensityOuter", glowIntensityOuter * BlurSpreadOuter * GLOW_INTENSITY_OUTER_MULT);
            compositeMaterial.SetColor("_GlowTint", glowTint);
            UpdateLensUsage();
            if(useLens)
            {
                compositeMaterial.SetTexture("_LensTex", lensTex);
                compositeMaterial.SetFloat("_LensIntensity", lensIntensity);
            }
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            rtFormat = src.format;
            srcWidth = src.width / samples;
            srcHeight = src.height / samples;

#if UNITY_2017_2_OR_NEWER
            srcVRUsage = src.vrUsage;

            RenderTexture glowTexInner = GetTemporaryRT(srcWidth, srcHeight, src.vrUsage);
            RenderTexture tmpGlowTexInner = GetTemporaryRT(srcWidth, srcHeight, src.vrUsage);

            RenderTexture glowTexOuter = GetTemporaryRT(srcWidth / 2, srcHeight / 2, src.vrUsage);
            RenderTexture tmpGlowTexOuter = GetTemporaryRT(srcWidth / 2, srcHeight / 2, src.vrUsage);
#else
            RenderTexture glowTexInner = GetTemporaryRT(srcWidth, srcHeight);
            RenderTexture tmpGlowTexInner = GetTemporaryRT(srcWidth, srcHeight);

            RenderTexture glowTexOuter = GetTemporaryRT(srcWidth / 2, srcHeight / 2);
            RenderTexture tmpGlowTexOuter = GetTemporaryRT(srcWidth / 2, srcHeight / 2);
#endif

            switch (glowType)
            {
                case GlowType.Luminance:
                    LuminanceGlow(src, dest, glowTexInner, tmpGlowTexInner, glowTexOuter, tmpGlowTexOuter);
                    break;
                case GlowType.Selective:
                    SelectiveGlow(src, dest, glowTexInner, tmpGlowTexInner, glowTexOuter, tmpGlowTexOuter);
                    break;
                case GlowType.Fullscreen:
                    FullScreenGlow(src, dest, glowTexInner, tmpGlowTexInner, glowTexOuter, tmpGlowTexOuter);
                    break;
            }

            RenderTexture.ReleaseTemporary(glowTexOuter);
            RenderTexture.ReleaseTemporary(tmpGlowTexOuter);

            RenderTexture.ReleaseTemporary(glowTexInner);
            RenderTexture.ReleaseTemporary(tmpGlowTexInner);
        }

        private void UpdateLensUsage()
        {
            if (useLens && glowType != GlowType.Fullscreen)
            {
                 EnableLens(true);
            }
            else
            {
                EnableLens(false);
            }
        }

        private void EnableLens(bool use)
        {
            if(use)
            {
                SetKeyword(true, LENS_KEYWORD, compositeMaterial);
            }
            else
            {
                SetKeyword(false, LENS_KEYWORD, compositeMaterial);
            }
        }

        private static void SetKeyword(bool enable, string keyword, Material mat)
        {
            if (enable)
            {
                if(!mat.IsKeywordEnabled(keyword))
                    mat.EnableKeyword(keyword);
            }
            else
            {
                if (mat.IsKeywordEnabled(keyword))
                    mat.DisableKeyword(keyword);
            }
        }
    }

    public enum GlowType
    {
        Selective = 0,
        Fullscreen = 1,
        Luminance = 2
    }
}