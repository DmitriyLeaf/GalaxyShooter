using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    private AudioSource _audio;

	// Use this for initialization
	void Start () {
        if (GetComponent<AudioSource>() != null) {
            _audio = GetComponent<AudioSource>();
            _audio.Play();
        }
        Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
