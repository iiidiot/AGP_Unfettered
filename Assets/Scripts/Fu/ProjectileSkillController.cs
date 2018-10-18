using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkillController : MonoBehaviour {

    public GameObject particlesParent;
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public Transform muzzleParent;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false; //Use to ensure we only call collision once!!

    void Start()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = particlesParent.transform;
        if (muzzleParticle)
        {
            muzzleParent = GameObject.Find("Player/MuzzlePoint").transform;
            muzzleParticle = Instantiate(muzzleParticle, muzzleParent.position, muzzleParent.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
        }
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!hasCollided && hit.gameObject.tag!="Player")
        {
            hasCollided = true;
            //transform.DetachChildren();
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

            if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
            {
                Destroy(hit.gameObject);
            }


            //yield WaitForSeconds (0.05);
            foreach (GameObject trail in trailParticles)
            {
                GameObject curTrail = particlesParent.transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }
            Destroy(projectileParticle, 3f);
            Destroy(impactParticle, 3f);
            Destroy(gameObject);
            //projectileParticle.Stop();

            ParticleSystem[] trails = particlesParent.transform.GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                ParticleSystem trail = trails[i];
                if (!trail.gameObject.name.Contains("Trail"))
                    continue;

                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2);
            }
        }
    }
}
