using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerUpId;
    //0 - triple
    //1 - speed
    //2 - shield

	// Use this for initialization
	void Start () {
        _speed = 3.0f;

        transform.position = new Vector3(Random.Range(8.2f, -8.2f), 5.63f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5.63f) {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                if (powerUpId == 0) {
                    player.TripleShotPowerUpOn(8.0f);
                } else if (powerUpId == 1) {
                    //player.SpeedPowerUpOn(2.0f, 8.0f);
                    player.SpeedPowerUpOn(8.0f);
                } else if (powerUpId == 2) {
                    player.ShieldPowerUpOn(13.0f);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
