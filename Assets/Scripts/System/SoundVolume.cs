using UnityEngine;
using System.Collections;

public class SoundVolume : MonoBehaviour {

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.HasKey("SoundVolume") && audioSource.volume != PlayerPrefs.GetFloat("SoundVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
        }
	}
}
