using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float lives = 1;
	public int points = 1;
	private GameController gameController;
	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();

	}

	public void Init (int level) {
		lives += lives *level * Constants.ENEMIES_LIVES_LEVEL_FACTOR;
	}

	void OnRenderObject () {
		GetComponent<RemoveOutsideScreen> ().enabled = true;
	}

	void OnTriggerEnter2D (Collider2D trigger) {
		if (trigger.tag == "PlayerBullet") {
			Destroy (trigger.gameObject);
			lives--;
			if (lives <= 0.5f) {
				animator.SetTrigger("Dying");
				Destroy (gameObject, animator.GetCurrentAnimatorStateInfo (0).length);
			} else {
				animator.SetTrigger("Damage");
			}
		}
	}

	void OnDestroy () {
		gameController.EnemyDied (points);
	}
}
