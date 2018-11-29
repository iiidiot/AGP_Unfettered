using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFadeSamplePlayer : MonoBehaviour {

    enum State {
        None,
        FadeOut,
        FadeIn,
    }
    public GameObject fadeObject;
    public MeshRenderer[] fadeMeshes;
    public SkinnedMeshRenderer[] fadeSkinnedMeshes;
    public ParticleSystem fadeOutParticle;
    public ParticleSystem fadeInParticle;

    List<Material> fadeMaterials = new List<Material>();
    float fadeTime;
    State state;
    float fadeSpeed = 1.0f;
    float risePower = 0.2f;
    float twistPower = 3.0f;
    float spreadPower = 0.6f;

	void Start () {
        foreach (var mesh in fadeMeshes) {
            foreach (var material in mesh.materials) {
                fadeMaterials.Add(material);
            }
        }
        foreach (var mesh in fadeSkinnedMeshes) {
            foreach (var material in mesh.materials) {
                fadeMaterials.Add(material);
            }
        }
    }

    void Update () {
        fadeTime += Time.deltaTime;
        float fadeDuration = 2.0f / fadeSpeed;
        float fadeStartDelay = 0.9f / fadeSpeed;
        float fadeRate = 0.0f;
        switch (state) {
        case State.FadeOut:
            fadeRate = Mathf.Clamp((fadeTime - fadeStartDelay) / fadeDuration, 0.0f, 1.0f);
            break;
        case State.FadeIn:
            fadeRate = 1.0f - Mathf.Clamp((fadeTime - fadeStartDelay) / fadeDuration, 0.0f, 1.0f);
            break;
        }
        var basePos = new Vector4();
        basePos.x = fadeObject.transform.position.x;
        basePos.y = fadeObject.transform.position.y;
        basePos.z = fadeObject.transform.position.z;
        foreach (var material in fadeMaterials) {
            material.SetVector("_ObjectBasePos", basePos);
            material.SetFloat("_FadeRate", fadeRate);
            material.SetFloat("_RisePower", risePower);
            material.SetFloat("_TwistPower", twistPower);
            material.SetFloat("_SpreadPower", spreadPower);
        }
    }

    public void StartFadeOut() {
        fadeTime = 0.0f;
        state = State.FadeOut;
        if (fadeOutParticle != null) {
            var main = fadeOutParticle.main;
            main.simulationSpeed = fadeSpeed;
            foreach (var childParticle in fadeOutParticle.GetComponentsInChildren<ParticleSystem>()) {
                var mainChild = childParticle.main;
                mainChild.simulationSpeed = fadeSpeed;
            }
            fadeOutParticle.Play(true);
        }
    }

    public void StartFadeIn() {
        fadeTime = 0.0f;
        state = State.FadeIn;
        if (fadeInParticle != null) {
            var main = fadeInParticle.main;
            main.simulationSpeed = fadeSpeed;
            foreach (var childParticle in fadeInParticle.GetComponentsInChildren<ParticleSystem>()) {
                var mainChild = childParticle.main;
                mainChild.simulationSpeed = fadeSpeed;
            }
            fadeInParticle.Play(true);
        }
    }

    public void FadeSpeedChange(float value) {
        fadeSpeed = value;
    }

    public void RisePowerChange(float value) {
        risePower = value;
    }

    public void TwistPowerChange(float value) {
        twistPower = value;
    }

    public void SpreadPowerChange(float value) {
        spreadPower = value;
    }
}
