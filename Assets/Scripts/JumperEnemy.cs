using UnityEngine;
using System.Collections;

public class JumperEnemy : MonoBehaviour {
	public float speed = Constants.JUMPER_ENEMIES_SPEED;
	public bool jumpsTowardsPlayer = false;
	private GameObject player;
	private bool isJumping = false;
	private bool gamePaused = false;

	void Start () {
		// Pause event
		GameController gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		gameController.pauseEvent.AddListener (OnPause);
			
		if (jumpsTowardsPlayer) {
			player = GameObject.FindWithTag ("Player");
		}
	}

	void FixedUpdate () {
		// Jumping
		if (!gamePaused && isJumping) {
			// Follow player
			if (jumpsTowardsPlayer && player != null && transform.position.y - player.transform.position.y > 0.5) {
				transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed * Time.fixedDeltaTime);
				// Look at the player
				Vector2 difference = transform.position - player.transform.position;
				float z = Mathf.Atan2 (difference.normalized.y, difference.normalized.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0f, 0f, z - 90);
			} else {
				// Move down
				transform.position -= new Vector3 (0, speed * Time.fixedDeltaTime, 0);
				transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			}
		}
	}

	public void Jump () {
		transform.SetParent(null);
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
		isJumping = true;
	}

	public void OnPause () {
		gamePaused = !gamePaused;
	}
}
