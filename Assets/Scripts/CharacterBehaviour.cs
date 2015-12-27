using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour {

    public GameSystem gameSystem;
    public AudioSource batFlapSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bat")
        {
            gameSystem.gameOver();
            if (batFlapSound != null)
            {
                batFlapSound.Play();
            }
        }
    }
}
