using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _health = 3;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _uiManager;

    //[SerializeField]
    //private AudioClip _clipDestroy;

    //private Animator _enemyAnimations;

	void Start () {
        _speed = 3.0f;
        _health = 3;
        //_enemyAnimations = GetComponent<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        transform.position = new Vector3(Random.Range(8.12f, -8.12f), 6.0f, 0);
    }
	
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -6.0f) {
            //float randomX = Random.Range(8.12f, -8.12f);
            transform.position = new Vector3(Random.Range(8.12f, -8.12f), 6.0f, 0);
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            this._health -= 1;
            if (_health < 1) {
                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
                //_enemyAnimations.SetInteger("State", 1);
                _uiManager.UpdateScore(3);
                //AudioSource.PlayClipAtPoint(_clipDestroy, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        } else if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage(_health);
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            //_enemyAnimations.SetInteger("State", 1);
            _uiManager.UpdateScore(3);
            //AudioSource.PlayClipAtPoint(_clipDestroy, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
