using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private int _health = 3;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

    [SerializeField]
    private bool _tripleShot = false;
    [SerializeField]
    private bool _speedBoost = false;
    [SerializeField]
    private bool _shield = false;

    //private int _shieldStrength = 3;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _powerupClip;

    [SerializeField]
    private GameObject[] _engines;

    // Use this for initialization
    void Start() {
        Debug.Log("Player was started");
        _speed = 5.0f;
        _health = 3;
        _fireRate = 0.2f;
        _nextFire = 0.0f;
        _tripleShot = false;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null) {
            _uiManager.UpdateLives(_health);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        transform.position = new Vector3(0, -3, 0);
    }

    // Update is called once per frame
    void Update() {
        Move();
        Shot();
        BorderCollision();
    }

    void Shot() {
        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || CrossPlatformInputManager.GetButtonDown("Fire")) {
        if (CrossPlatformInputManager.GetButtonDown("Fire")) {
            if (Time.time > _nextFire) {
                _audioSource.Play();
                _nextFire = Time.time + _fireRate;
                if (_tripleShot == true) {
                     //Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    Instantiate(_laserPrefab, transform.position + new Vector3(0.53f, 0.18f, 0), Quaternion.identity);
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                    Instantiate(_laserPrefab, transform.position + new Vector3(-0.53f, 0.18f, 0), Quaternion.identity);
                } else {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
                }
            }
        }
    }

    void Move() {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        if (_speedBoost == true) {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * 2.0f * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * 2.0f * verticalInput);
        } else {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
    }

    void BorderCollision() {
        if(transform.position.y > 5.6f) {
            transform.position = new Vector3(transform.position.x, -1 * transform.position.y, 0);
        } else if (transform.position.y < -5.6f) {
            transform.position = new Vector3(transform.position.x, -1 * transform.position.y, 0);
        }

        if (transform.position.x > 9.4f) {
            transform.position = new Vector3(-1 * transform.position.x, transform.position.y, 0);
        } else if (transform.position.x < -9.4f) {
            transform.position = new Vector3(-1 * transform.position.x, transform.position.y, 0);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            if (this._health > 0) {
                _health -= 1.0f;
                Destroy(other.gameObject);
            } else {
                Destroy(this.gameObject);
            }
        }
    }*/

    public void Damage(int damage) {
        if (_shield == false) {
            this._health -= damage;
            for (int i = _engines.Length - this._health; i>=0; --i) {
                int whereDamage = (int)Random.Range(0.0f, _engines.Length);
                if (_engines[whereDamage].activeSelf) {
                    _engines[Mathf.Abs(whereDamage - 1)].SetActive(true);
                } else {
                    _engines[whereDamage].SetActive(true);
                }
            }
            /*int whereDamage = (int)Random.Range(0.0f, _engines.Length);
            if (_engines[whereDamage].activeSelf) {
                _engines[Mathf.Abs(whereDamage-1)].SetActive(true);
            } else {
                _engines[whereDamage].SetActive(true);
            }*/
        } //else if (_shieldStrength > 0) {
            //_shieldStrength -= 1;
        //} else {
            //_shield = true;
        //}

        if (this._health < 1) {
            this._health = 0;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }

        _uiManager.UpdateLives(this._health);
    }

    public void TripleShotPowerUpOn(float effectsTime) {
        Debug.Log("PewerUp: Trilple shot");
        AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position, 1f);
        _tripleShot = true;
        StartCoroutine(TripleShotPowerUpRoutine(effectsTime));
    }

    //public void SpeedPowerUpOn(float modificator, float effectsTime) {
    public void SpeedPowerUpOn(float effectsTime) {
        Debug.Log("PewerUp: Speed");
        AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position, 1f);
        //_speed *= modificator;
        _speedBoost = true;
        //StartCoroutine(SpeedPowerUpRoutine(modificator, effectsTime));
        StartCoroutine(SpeedPowerUpRoutine(effectsTime));
    }

    public void ShieldPowerUpOn(float effectsTime) {
        Debug.Log("PewerUp: Shield");
        AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position, 1f);
        _shield = true;
        //_shieldStrength = 3;
        //Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        _shieldGameObject.SetActive(true);
        StartCoroutine(ShieldPowerUpRoutine(effectsTime));
    }

    private IEnumerator TripleShotPowerUpRoutine(float effectsTime) {
        yield return new WaitForSeconds(effectsTime);
        _tripleShot = false;
    }

    //IEnumerator SpeedPowerUpRoutine(float modificator, float effectsTime) {
    private IEnumerator SpeedPowerUpRoutine(float effectsTime) {
        yield return new WaitForSeconds(effectsTime);
        //_speed /= modificator;
        _speedBoost = false;
    }

    private IEnumerator ShieldPowerUpRoutine(float effectsTime) {
        yield return new WaitForSeconds(effectsTime);
        _shield = false;
        _shieldGameObject.SetActive(false);
    }
}
