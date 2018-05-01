using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public GameObject shotAnimationPrefab;

	void OnTriggerEnter2D (Collider2D trigger) {
		// Shot animation
		GameObject shotAnimation = Instantiate (shotAnimationPrefab);
		shotAnimation.transform.position = transform.position;
		Destroy (shotAnimation.gameObject, shotAnimation.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
	}
}
