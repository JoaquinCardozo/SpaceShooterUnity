using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesController : MonoBehaviour {
	public GameObject enemyAPrefab;
	public GameObject enemyBPrefab;
	public GameObject enemyCPrefab;
	public GameObject enemyDPrefab;
	public GameObject enemyEPrefab;
	public AudioClip newLevelSound;
	private int enemiesRows = Constants.ENEMIES_ROWS_NUMBER;
	private int enemiesColumns = Constants.ENEMIES_COLUMNS_NUMBER;
	private int enemiesRowsDistance = Constants.ENEMIES_ROWS_DISTANCE;
	private int enemiesColumnsDistance = Constants.ENEMIES_COLUMNS_DISTANCE;
	public float enemiesJumpsInterval = Constants.ENEMIES_JUMPS_INTERVAL;
	private Queue<GameObject> jumperEnemies = new Queue<GameObject> ();

	public int CreateEnemies (int level) {
		// Enemies positions
		float lastColumnX = ((enemiesColumns - 1) * enemiesColumnsDistance) / 2.0f;
		float lastRowY = -((enemiesRows - 1) * enemiesRowsDistance) / 2.0f;
		for (float x = -lastColumnX; x <= lastColumnX; x += enemiesColumnsDistance) {
			for (float y = -lastRowY; y >= lastRowY; y -= enemiesRowsDistance) {
				GameObject enemyPrefab = GetRandomEnemyType (level);
				GameObject enemy = Instantiate (enemyPrefab);
				enemy.GetComponent<Enemy> ().Init (level);
				enemy.transform.SetParent (this.transform);
				enemy.transform.localPosition = new Vector2 (x, y);
				if (enemyPrefab == enemyDPrefab || enemyPrefab == enemyEPrefab) {
					jumperEnemies.Enqueue(enemy);
				}
			}
		}
		AudioSource.PlayClipAtPoint (newLevelSound,  Camera.main.transform.position);
		InvokeRepeating ("ReleaseEnemy", enemiesJumpsInterval, enemiesJumpsInterval);
		return enemiesRows * enemiesColumns;
	}

	GameObject GetRandomEnemyType (int level) {
		float enemyAProbability = Constants.ENEMY_A_INITIAL_PROBABILITY + level * (Constants.ENEMY_A_PROBABILITY_LEVEL_FACTOR);
		float enemyBProbability = Constants.ENEMY_B_INITIAL_PROBABILITY + level * (Constants.ENEMY_B_PROBABILITY_LEVEL_FACTOR);
		float enemyCProbability = Constants.ENEMY_C_INITIAL_PROBABILITY + level * (Constants.ENEMY_C_PROBABILITY_LEVEL_FACTOR);
		float enemyDProbability = Constants.ENEMY_D_INITIAL_PROBABILITY + level * (Constants.ENEMY_D_PROBABILITY_LEVEL_FACTOR);
		float enemyEProbability = Constants.ENEMY_E_INITIAL_PROBABILITY + level * (Constants.ENEMY_E_PROBABILITY_LEVEL_FACTOR);

		List<GameObject> enemiesPrefabsList = new List<GameObject> ();
		enemiesPrefabsList.Add (enemyAPrefab);
		enemiesPrefabsList.Add (enemyBPrefab);
		enemiesPrefabsList.Add (enemyCPrefab);
		enemiesPrefabsList.Add (enemyDPrefab);
		enemiesPrefabsList.Add (enemyEPrefab);
		List<float> enemiesProbabilitiesList = new List<float> ();
		enemiesProbabilitiesList.Add (enemyAProbability);
		enemiesProbabilitiesList.Add (enemyBProbability);
		enemiesProbabilitiesList.Add (enemyCProbability);
		enemiesProbabilitiesList.Add (enemyDProbability);
		enemiesProbabilitiesList.Add (enemyEProbability);

		float randomValue = Random.value;
		GameObject enemyToReturn = null;
		for (int i = 0; i < enemiesProbabilitiesList.Count; i++) {
			if (randomValue <= enemiesProbabilitiesList [i]) {
				enemyToReturn = enemiesPrefabsList [i];
				break;
			} else {
				randomValue -= enemiesProbabilitiesList [i];
			}
		}

		return enemyToReturn;
	}

	void ReleaseEnemy () {
		if (jumperEnemies.Count > 0) {
			GameObject enemy = jumperEnemies.Dequeue ();
			if (enemy != null) {
				enemy.GetComponent<JumperEnemy> ().Jump ();
			}
		}
	}

	public void OnPause () {
		Animator animator = GetComponent<Animator> ();
		animator.enabled = !animator.enabled;
	}
}
