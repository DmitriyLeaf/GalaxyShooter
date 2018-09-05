using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
	
    // Use this for initialization
	void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver == true) {
            if (Input.GetKeyDown(KeyCode.P) || Input.touchCount > 1) {
                Instantiate(player);
                gameOver = false;
                _uiManager.HideTitleScreen();
                _spawnManager.StartSpawn();
            }
        }
	}
}
