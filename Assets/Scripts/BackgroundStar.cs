using UnityEngine;
using System.Collections;

public class BackgroundStar : MonoBehaviour {
	Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
		Invoke ("InitAtRandomPosition", Random.Range(0f, 5f));
	}

	void InitAtRandomPosition () {
		float randomX = Random.Range(0, Screen.width);
		float randomY = Random.Range(0, Screen.height);
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, Camera.main.farClipPlane));
		animator.SetTrigger ("StartAnimation");
		Invoke ("InitAtRandomPosition", animator.GetCurrentAnimatorStateInfo (0).length + Random.Range(3f, 5f));
	}
}
