using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;


public class DrawController : MonoBehaviour {

    public float time_scale = 0;
    public float turningTime = 1f;
    public GameObject FuDrawPanel;

    public GameObject[] ToHide;
    public GameObject[] ToChangeMaterial;

    //private bool[] initialIsActive; // to hide objects

    private Transform playerTransform;

    private GameObject middleGround;
    public GameObject[] platforms;

    Material alphaDissolve;
    Material spriteDiffuse;

    private GameObject characterParticleEffect;
    private GameObject inkParticleEffect;
    private GameObject fog;
    private GameObject distortion;


    // Use this for initialization
    void Start () {

        middleGround = GameObject.Find("Level/middleGround");

        InitPostProcessingColor();

        playerTransform = CharactersConfigManager.GetPlayerGameObject().transform;

        Camera.main.GetComponent<Painting>().enabled = false;
        PlayerStatus.IsDrawing = false;

        ///initialIsActive = new bool[ToHide.Length];

        alphaDissolve = Resources.Load("Materials/AlphaDissolve") as Material;
        spriteDiffuse = Resources.Load("Materials/SpriteDiffuse") as Material;

        
    }

    // make sure it is colored when the game starts
    private void InitPostProcessingColor()
    {
        PostProcessingProfile postProcessingProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        ColorGradingModel.Settings s = postProcessingProfile.colorGrading.settings;
        s.basic.saturation = 1;
        postProcessingProfile.colorGrading.settings = s;
    }
	
    private bool isTurning = false;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!PlayerStatus.IsDrawing)
            {
                PlayerTestController.instance.m_animator.SetTrigger("isThrowing");
            }
            t = 0f;
            isTurning = true;
        }

        if (isTurning && !PlayerStatus.IsDrawing)
        {
            DrawModeVisualEffects();
          
            TurnBlackAndWhite();
        }

        if (isTurning && PlayerStatus.IsDrawing)
        {
            ReleaseSkill();
            DisableDraw();
            PlayerTestController.instance.m_animator.SetTrigger("finishThrowing");
            RemoveDrawModeVisualEffects();
            TurnColored();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            DrawModeVisualEffects();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RemoveDrawModeVisualEffects();
        }
    }


    private void DrawModeVisualEffects()
    {
        // hide objects
        foreach (GameObject go in ToHide)
        {
            if (go)
            {
                go.SetActive(false);
            }
        }

        // change bg shader
        middleGround.GetComponent<SpriteRenderer>().material = alphaDissolve;
        foreach(GameObject g in platforms)
        {
            g.GetComponent<SpriteRenderer>().material = alphaDissolve;
        }

        // change player/monster shader
        foreach (GameObject go in ToChangeMaterial)
        {
            if (go)
            {
                go.GetComponent<MaterialManager>().DrawingMode();
            }
        }

        // particle
        AddEffectParticles();

        // postprocessing
        Camera.main.GetComponent<PostProcessingProfileManager>().ChangeToDrawMode();
    }

    private void RemoveDrawModeVisualEffects()
    {
        foreach (GameObject go in ToHide)
        {
            if (go)
            {
                go.SetActive(true);
            }
        }
        middleGround.GetComponent<SpriteRenderer>().material = spriteDiffuse;
        foreach(GameObject g in platforms)
        {
            g.GetComponent<SpriteRenderer>().material = spriteDiffuse;
        }

        // change player/monster shader
        foreach (GameObject go in ToChangeMaterial)
        {
            if (go)
            {
                go.GetComponent<MaterialManager>().OrdinaryMode();
            }
        }


        DestoryEffectParticles();

        Camera.main.GetComponent<PostProcessingProfileManager>().ChangeToOrdinaryMode();
    }

    void EnableDraw()
    {
        Camera.main.GetComponent<Painting>().enabled = true;
        //Camera.main.GetComponent<Painting>().Clear();

        FuDrawPanel.SetActive(true);

        Time.timeScale = time_scale;

        ParticleSystem[] fogs = inkParticleEffect.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in fogs)
        {
           
            var main = ps.main;
            //main.simulationSpeed = 1/time_scale;
        }
        ParticleSystem ps2 = fog.GetComponent<ParticleSystem>();
        var main2 = ps2.main;
        //main2.simulationSpeed = 1/time_scale;

        PlayerStatus.IsDrawing = true;
    }

   

    private void AddEffectParticles()
    {
        if(!characterParticleEffect)
            characterParticleEffect = Instantiate(Resources.Load("Prefabs/Effects/CharacterParticleEffects"), playerTransform.position, playerTransform.rotation) as GameObject;
        if (!inkParticleEffect)
            inkParticleEffect = Instantiate(Resources.Load("Prefabs/Effects/InkParticle"), playerTransform.position, playerTransform.rotation) as GameObject;

        if (!fog)
            fog = Instantiate(Resources.Load("Prefabs/Effects/Fog"), playerTransform.position, playerTransform.rotation) as GameObject;
        if (!distortion)
            distortion = Instantiate(Resources.Load("Prefabs/Effects/DistortionFuDraw"), playerTransform.position, playerTransform.rotation) as GameObject;
    }

    static float t = 0f;

    private void TurnBlackAndWhite()
    {
       
        PostProcessingProfile postProcessingProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        ColorGradingModel.Settings blackAndWhite = postProcessingProfile.colorGrading.settings;
        blackAndWhite.basic.saturation =  Mathf.Lerp(1, 0, t);
        t += 3f * Time.deltaTime;
        postProcessingProfile.colorGrading.settings = blackAndWhite;

        if (t > 1.0f)
        {
            isTurning = false;
            EnableDraw();
            //AddEffectParticles();
        }
        
    }



    private void TurnColored()
    {
       

       
        PostProcessingProfile postProcessingProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        ColorGradingModel.Settings blackAndWhite = postProcessingProfile.colorGrading.settings;
        blackAndWhite.basic.saturation = 1;//Mathf.Lerp(0, 1, t);
        //t += 10f  * Time.deltaTime;
        postProcessingProfile.colorGrading.settings = blackAndWhite;

        //if (t > 1.0f)
        //{
            
            isTurning = false;
            PlayerStatus.IsDrawing = false;
            DestoryEffectParticles();

            ReleaseSkill();
            skillName = "";
       // }

    }

    private void DestoryEffectParticles()
    {
        if (characterParticleEffect)
            Destroy(characterParticleEffect);

        if (inkParticleEffect)
            Destroy(inkParticleEffect);
            
        if (fog)
            Destroy(fog);

        if (distortion)
            Destroy(distortion);

        System.GC.Collect();
    }

    string skillName = "";
    void RecognizeSkill()
    {
        double[] arr = Camera.main.GetComponent<Painting>().GetPicArr();
        skillName = GetComponent<recognizer>().Recognize(arr); 
    }

    void ReleaseSkill()
    {
        if (skillName.Length > 0)
        {
            playerTransform.GetComponent<SkillReleaser>().ReleaseSkill(skillName);
        }
    }

    void DisableDraw()
    {

        RecognizeSkill();

        Camera.main.GetComponent<Painting>().enabled = false;
        Camera.main.GetComponent<Painting>().Clear();

        FuDrawPanel.SetActive(false);

        Time.timeScale = 1;

       
    }
}
