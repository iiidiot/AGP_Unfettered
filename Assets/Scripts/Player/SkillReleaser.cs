using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReleaser : MonoBehaviour {

    //public GameObject obj;
    //public GameObject obj2;

    private GameObject fireBall;
    private GameObject waterBall;

    public Transform skillSpellingPoint;

    // Use this for initialization
    void Start () {
        InitFireBall();
        fireBall.SetActive(false);
    }

    private void Tm_CollisionEnter(object sender, RFX4_TransformMotion.RFX4_CollisionInfo e)
    {
        Debug.Log(e.Hit.transform.name); //will print collided object name to the console.
    }

    // Update is called once per frame
    void Update () {


        if (Input.GetKeyDown(KeyCode.F9))
        {
            ReleaseFireBallEffect();
        }

        Transform player = PlayerTestController.instance.transform;
        float h_direction = player.rotation.eulerAngles.y > 179 ? 1 : -1;

    }


    private void ReleaseFireBallEffect()
    {
        bool isPlayerFaceLeft = !PlayerTestController.instance.facingRight;
        if (isPlayerFaceLeft)
        {
            fireBall.transform.SetPositionAndRotation(skillSpellingPoint.position, Quaternion.Euler(0, -90, 0));
        }
        else
        {
            fireBall.transform.SetPositionAndRotation(skillSpellingPoint.position, Quaternion.Euler(0, 90, 0));
        }
        fireBall.SetActive(true);
    }

    private void InitFireBall()
    {
        fireBall = Instantiate(Resources.Load("Prefabs/Effects/FireBall"), skillSpellingPoint.position, Quaternion.Euler(0, -90, 0)) as GameObject;
        var tm = fireBall.GetComponentInChildren<RFX4_TransformMotion>(true);
        if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;

        // define color
        float colorHUE = 6f; // red fire color
        RFX4_ColorHelper.ChangeObjectColorByHUE(fireBall, colorHUE / 360f);
        var transformMotion = fireBall.GetComponentInChildren<RFX4_TransformMotion>(true);
        if (transformMotion != null)
        {
            transformMotion.HUE = colorHUE / 360f;
            foreach (var collidedInstance in transformMotion.CollidedInstances)
            {
                if (collidedInstance != null) RFX4_ColorHelper.ChangeObjectColorByHUE(collidedInstance, colorHUE / 360f);
            }
        }

        var rayCastCollision = fireBall.GetComponentInChildren<RFX4_RaycastCollision>(true);
        if (rayCastCollision != null)
        {
            rayCastCollision.HUE = colorHUE / 360f;
            foreach (var collidedInstance in rayCastCollision.CollidedInstances)
            {
                if (collidedInstance != null) RFX4_ColorHelper.ChangeObjectColorByHUE(collidedInstance, colorHUE / 360f);
            }
        }
    }

    public void ReleaseSkill(string FuName)
    {
        if (FuName == "FireBall")
        {
            //Debug.Log("火火火火火火火火火火火");
            ReleaseFireBallEffect();
        }
        if(FuName == "FrostBall")
        {
            //Debug.Log("水水水水水水水水水水水水");
            ReleaseWaterBallEffect();
        }
    }


    private void ReleaseWaterBallEffect()
    { 
        Transform player = PlayerTestController.instance.transform;
        bool isPlayerFaceLeft = player.rotation.eulerAngles.y > 90;
        waterBall = Instantiate(Resources.Load("Prefabs/Effects/FrostMissile/OBJ"), player.position, player.rotation) as GameObject;
        Rigidbody r = waterBall.GetComponent<Rigidbody>();

        if (isPlayerFaceLeft)
        {
            r.velocity = new Vector2(-15f, r.velocity.y);
        }
        else
        {
            r.velocity = new Vector2(15f, r.velocity.y);
        }
    }
}
