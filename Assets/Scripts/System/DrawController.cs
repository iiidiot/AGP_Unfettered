using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;

public class DrawController : MonoBehaviour {

    public float time_scale = 0;
    public float turningTime = 1f;
    public bool isDrawing = false;
    public GameObject FuDrawPanel;

    private Transform playerTransform;

    // Use this for initialization
    void Start () {
        InitPostProcessingColor();

        playerTransform = PlayerTestController.instance.transform;

        Camera.main.GetComponent<Painting>().enabled = false;
        isDrawing = false;

        
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            t = 0f;
            isTurning = true;
        }
        
        if (isTurning && !isDrawing)
        {
            AddEffectParticles();
            TurnBlackAndWhite();
        }

        if (isTurning && isDrawing)
        {
            ReleaseSkill();
            DisableDraw();
            
            TurnColored();
        }
    }




    void EnableDraw()
    {
        Camera.main.GetComponent<Painting>().enabled = true;
        //Camera.main.GetComponent<Painting>().Clear();

        FuDrawPanel.SetActive(true);

        Time.timeScale = time_scale;

        isDrawing = true;
    }

    private GameObject characterParticleEffectBlack;
    //private GameObject characterParticleEffectWhite;

    private void AddEffectParticles()
    {
        if(!characterParticleEffectBlack)
            characterParticleEffectBlack = Instantiate(Resources.Load("Prefabs/Effects/CharacterParticleEffects"), playerTransform.position, playerTransform.rotation) as GameObject;
        ///if (!characterParticleEffectWhite)
            //characterParticleEffectWhite = Instantiate(Resources.Load("Prefabs/Effects/CharacterParticleEffectWhite"), playerTransform.position, playerTransform.rotation) as GameObject;
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
            isDrawing = false;
            DestoryEffectParticles();

            ReleaseSkill();
            skillName = "";
       // }

    }

    private void DestoryEffectParticles()
    {
        if (characterParticleEffectBlack)
            Destroy(characterParticleEffectBlack);

        //if (characterParticleEffectWhite)
        //    Destroy(characterParticleEffectWhite);

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
            playerTransform.GetComponent<SkillReleaser>().releaseSkill(skillName);
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
