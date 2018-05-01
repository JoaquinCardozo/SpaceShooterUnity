using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public AudioClip playerLostSound;
	public AudioClip damageSound;
	private float speed = Constants.PLAYER_SPEED;
	private int lives = Constants.PLAYER_INITIAL_LIVES;
	private Weapon weapon;
	private GameController gameController;
	private Animator animator;
	private bool gamePaused = false;

	void Awake () {
		weapon = GetComponent<Weapon> ();
		animator = GetComponent<Animator> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}

	void Update () {
		// Shotting
		if (!gamePaused && Input.GetButtonDown ("Fire1")) {
			weapon.Shoot (Vector2.up, transform.position);
		}
	}

	void FixedUpdate () {
		// Movement
		if (!gamePaused) {
			float direction = Input.GetAxis ("Horizontal");
			transform.position += new Vector3 (direction * speed * Time.fixedDeltaTime, 0, 0);
		}
	}

	void OnTriggerEnter2D (Collider2D trigger) {
		if (trigger.tag == "EnemyBullet" || trigger.tag == "Enemy") {
			Destroy (trigger.gameObject);
			lives--;
			AudioSource.PlayClipAtPoint (damageSound,  Camera.main.transform.position);
			gameController.RemoveLifeOnLivesCounter ();
			if (lives <= 0) {
				animator.SetTrigger("Dying");
				AudioSource.PlayClipAtPoint (playerLostSound, Camera.main.transform.position);
				Destroy (gameObject, animator.GetCurrentAnimatorStateInfo (0).length);
				gameController.PlayerDied ();
			} else {
				animator.SetTrigger("Damage");
			}
		}
	}

	public void AddLife () {
		lives++;
	}

	public void OnPause () {
		gamePaused = !gamePaused;
	}
}
