using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerupsPrefab;

    private GameManager _gameManager;

    /*
    private bool _enemySpawn = true;
    private bool _powerupSpawn = true;

	// Use this for initialization
	void Start () {
        _enemySpawn = true;
        _powerupSpawn = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (_enemySpawn == true) {
            _enemySpawn = false;
            Instantiate(_enemyPrefab);
            StartCoroutine(EnemySpawnRoutine(Random.Range(0.0f, 7.0f)));
        }
        if (_powerupSpawn == true) {
            _powerupSpawn = false;
            Instantiate(_powerupsPrefab[(int)Random.Range(0.0f, _powerupsPrefab.Length - 1)]);
            StartCoroutine(PowerupSpawnRoutine(Random.Range(0.0f, 13.0f)));
        }
    }

    private IEnumerator EnemySpawnRoutine(float nextEnemyTime) {
        Debug.Log("Enemy time: " + nextEnemyTime);
        yield return new WaitForSeconds(nextEnemyTime);
        _enemySpawn = true;
    }

    private IEnumerator PowerupSpawnRoutine(float nextPowerupTime) {
        Debug.Log("Powerup time: " + nextPowerupTime);
        yield return new WaitForSeconds(nextPowerupTime);
        _powerupSpawn = true;
    }
    */
    void Start() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /*void Update() {
        if (_gameManager.gameOver == true) {
            Destroy(this.gameObject);
        }
    }*/

    public void StartSpawn() {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine() {
        while (_gameManager.gameOver == false) {
            Instantiate(_enemyPrefab);
            yield return new WaitForSeconds(Random.Range(0.0f, 7.0f));
        }
        //Destroy(GameObject.FindWithTag("Enemy"));
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
    }

    private IEnumerator PowerupSpawnRoutine() {
        while (_gameManager.gameOver == false) {
            Instantiate(_powerupsPrefab[(int)Random.Range(0.0f, _powerupsPrefab.Length)]);
            yield return new WaitForSeconds(Random.Range(0.0f, 13.0f));
        }
    }
}