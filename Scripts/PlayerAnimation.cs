using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimation : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("Horizontal") < 0) {
            _animator.SetBool("TurnLeft", true);
        } else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            _animator.SetBool("TurnLeft", false);
        } else {
            _animator.SetBool("TurnLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || CrossPlatformInputManager.GetAxis("Horizontal") > 0) {
            _animator.SetBool("TurnRight", true);
        } else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
            _animator.SetBool("TurnRight", false);
        } else {
            _animator.SetBool("TurnRight", false);
        }

        /*if (Input.GetAxis("Horizontal") < 0) {
            _animator.SetBool("TurnLeft", true);
        } else if (Input.GetAxis("Horizontal") > 0) {
            _animator.SetBool("TurnRight", true);
        } else {
            _animator.SetBool("TurnLeft", false);
            _animator.SetBool("TurnRight", false);
        }*/
    }
}
