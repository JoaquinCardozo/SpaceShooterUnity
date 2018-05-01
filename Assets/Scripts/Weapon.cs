using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public GameObject bulletPrefab;
	public AudioClip shootSound;
	private float speed = Constants.BULLETS_SPEED;

	public void Shoot (Vector2 direction, Vector2 origin) {
		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = origin;
		bullet.GetComponent<Rigidbody2D> ().velocity = direction * speed;

		// Animation
		GetComponent<Animator> ().SetTrigger("Shotting");
		AudioSource.PlayClipAtPoint(shootSound,  Camera.main.transform.position);
	}
}
