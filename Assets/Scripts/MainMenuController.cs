using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
	public Text UIHighScoreCounter;

	void Start () {
		if (PlayerPrefs.HasKey ("highscore")) {
			UIHighScoreCounter.text = PlayerPrefs.GetInt ("highscore").ToString ();
		} else {
			UIHighScoreCounter.text = "0";
		}
	}

	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			Quit ();
		}
	}

	public void StartGame () {
		SceneManager.LoadScene ("Game");
	}

	public void Quit () {
		Application.Quit ();
	}
}
