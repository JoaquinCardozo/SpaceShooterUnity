using UnityEngine;
using System.Collections;

public class ShooterEnemy : MonoBehaviour {
	public bool onlyShootWhenPlayerisBelow = true;
	public int interval = 1;
	private Weapon weapon;
	private GameObject player;
	private bool shooting = false;
	private bool gamePaused = false;

	void Start () {
		// Pause event
		GameController gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		gameController.pauseEvent.AddListener (OnPause);

		weapon = GetComponent<Weapon> ();
		if (weapon == null) {
			Debug.LogError ("Weapon component is missing.");
		}

		if (onlyShootWhenPlayerisBelow) {
			player = GameObject.FindWithTag ("Player");
		} else {
			InvokeRepeating ("ShootWeapon", Random.Range(1, interval), interval);
		}
	}

	void Update () {
		if (onlyShootWhenPlayerisBelow && player != null) {
			if (Mathf.RoundToInt (transform.position.x) == Mathf.RoundToInt (player.transform.position.x)) {
				if (!shooting) {
					shooting = true;
					InvokeRepeating ("ShootWeapon", 0, interval);
				}
			} else if (shooting) {
				shooting = false;
				CancelInvoke ("ShootWeapon");
			}
		}
	}

	void ShootWeapon () {
		if (!gamePaused && weapon != null) {
			weapon.Shoot (Vector2.down, transform.position);
		}
	}

	public void OnPause () {
		gamePaused = !gamePaused;
	}
}
