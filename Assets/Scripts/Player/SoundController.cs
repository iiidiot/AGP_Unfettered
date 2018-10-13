using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {
	static AudioSource audioSource;
	private static AudioClip[] audioClipsOutput; 

	[SerializeField]
	private string[] audioName = {"walk", "run", "jump", "attack", "underAttack", "shoot", "death"}; 

	[SerializeField]
	private  AudioClip[] audioClipsInput; 
	


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		if(audioClipsInput != null){
			audioClipsOutput = new AudioClip[audioClipsInput.Length];
			audioClipsOutput = (AudioClip[])audioClipsInput.Clone();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound (int index) {
		audioSource.PlayOneShot(audioClipsOutput[index]);
	}




}
