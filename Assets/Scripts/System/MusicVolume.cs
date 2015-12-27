using UnityEngine;
using System.Collections;

public class MusicVolume : MonoBehaviour {

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerPrefs.HasKey("MusicVolume") && audioSource.volume != PlayerPrefs.GetFloat("MusicVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
	}
}
