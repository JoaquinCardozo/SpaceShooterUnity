using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	public Player player;
	public EnemiesController enemiesController;
	public GameObject UILivesCounter;
	public Text UILevelCounter;
	public Text UIScoreCounter;
	public Text UIHighScoreCounter;
	public GameObject pauseMenu;
	public GameObject resultsMenu;
	public GameObject resultsMenuNewHighScoreTitle;
	public Text resultsMenuScore;
	public AudioClip pauseSound;
	public AudioClip newLifeSound;
	private int level = 0;
	private int score = 0;
	private int highScore = 0;
	private int enemiesNumber;
	private bool playerIsDead = false;
	private bool quitting = false;
	public UnityEvent pauseEvent;

	void Start () {
		if (PlayerPrefs.HasKey ("highscore")) {
			highScore = PlayerPrefs.GetInt ("highscore");
			UIHighScoreCounter.text = highScore.ToString ();
		}

		StartNewLevel (0);
	}

	void StartNewLevel (int newLevel) {
		level = newLevel;
		UILevelCounter.text = level.ToString ();
		enemiesNumber = enemiesController.CreateEnemies (level);
	}

	void Update () {
		if (Input.GetButtonDown ("Cancel") && !resultsMenu.activeSelf) {
			pauseMenu.SetActive (!pauseMenu.activeSelf);
			pauseEvent.Invoke ();
			AudioSource.PlayClipAtPoint (pauseSound,  Camera.main.transform.position);
		}
	}

	void AddLifeOnLivesCounter () {
		if (UILivesCounter.transform.childCount > 0) {
			Transform child = UILivesCounter.transform.GetChild (0);
			if (child != null) {
				Instantiate (child, UILivesCounter.transform);
			}
		}
	}

	public void RemoveLifeOnLivesCounter () {
		if (UILivesCounter.transform.childCount > 0) {
			Transform child = UILivesCounter.transform.GetChild (UILivesCounter.transform.childCount - 1);
			if (child != null) {
				Destroy (child.gameObject);
			}
		}
	}

	public void AddScore (int earnedPoints) {
		score += earnedPoints;
		UIScoreCounter.text = score.ToString ();
		// New Life
		if (score % Constants.NEW_LIFE_SCORE == 0) {
			player.AddLife ();
			AudioSource.PlayClipAtPoint (newLifeSound, Camera.main.transform.position);
			AddLifeOnLivesCounter ();
		}
	}

	public void EnemyDied (int earnedPoints) {
		if (!playerIsDead && !quitting) {
			AddScore (earnedPoints);
			enemiesNumber--;
			if (enemiesNumber <= 0) {
				StartNewLevel (level + 1);
			}
		}
	}

	public void PlayerDied () {
		playerIsDead = true;
		ShowResultsMenu ();
	}

	public void ShowResultsMenu () {
		resultsMenuScore.text = score.ToString ();
		bool newHighScore = UpdateHighScore ();
		if (newHighScore) {
			resultsMenuNewHighScoreTitle.SetActive (true);
		}

		if (pauseMenu.activeSelf) {
			pauseMenu.SetActive (false);
		} else {
			pauseEvent.Invoke ();
		}
		resultsMenu.SetActive (true);
	}

	public void ReturnToMainMenu () {
		quitting = true;
		SceneManager.LoadScene ("MainMenu");
	}

	bool UpdateHighScore () {
		if (score > highScore) {
			highScore = score;
			UIHighScoreCounter.text = score.ToString ();
			PlayerPrefs.SetInt ("highscore", score);
			return true;
		}
		return false;
	}

	void OnApplicationQuit () {
		quitting = true;
	}
}
