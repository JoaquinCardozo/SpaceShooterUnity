using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RemoveOutsideScreen : MonoBehaviour {
	void Update () {
		if (!GetComponent<Renderer> ().isVisible) {
			Destroy (this.gameObject);
		}
	}
}
