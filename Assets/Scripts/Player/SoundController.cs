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
		if(index == 0 && audioSource.isPlaying == false ){
			Debug.Log("is walking");
			audioSource.volume = Random.Range(0.8f, 1);
			audioSource.pitch = Random.Range(0.8f, 1);
			audioSource.PlayOneShot(audioClipsOutput[index]);
			audioSource.clip = audioClipsOutput[index];
		}else if(index != 0){
			audioSource.Stop();
			audioSource.PlayOneShot(audioClipsOutput[index]);
			audioSource.clip = audioClipsOutput[index];
		}
	}

	public static void StopPlayingSound () {
		
	
		if(audioSource.clip == audioClipsOutput[0]){
			Debug.Log("stop");
			audioSource.Stop();
		}
	}




}
