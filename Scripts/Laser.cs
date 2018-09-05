using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private float _speed = 20.0f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 6) {
            Destroy(this.gameObject);
        }
	}
}
