using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Sprite[] lives;
    public Image imageSprite;

    public int score = 0;
    public Text textScore;

    public GameObject titleScreen;
    public GameObject scoreObj;
    public GameObject playerLives;
    public GameObject pressToStart;
    public GameObject mobCortrol;

    public void UpdateLives(int livesNumber) {
        imageSprite.sprite = lives[livesNumber];
    }

    public void UpdateScore(int points) {
        score += points;
        textScore.text = "Score: " + score;
    }

    public void ShowTitleScreen() {
        titleScreen.SetActive(true);
        pressToStart.SetActive(true);

        mobCortrol.SetActive(false);
        playerLives.SetActive(false);
    }

    public void HideTitleScreen() {
        titleScreen.SetActive(false);
        pressToStart.SetActive(false);

        score = 0;
        UpdateScore(score);

        mobCortrol.SetActive(true);
        scoreObj.SetActive(true);
        playerLives.SetActive(true);
    }

	// Use this for initialization
	void Start () {
        mobCortrol.SetActive(false);
        scoreObj.SetActive(false);
        playerLives.SetActive(false);
    }
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
}
