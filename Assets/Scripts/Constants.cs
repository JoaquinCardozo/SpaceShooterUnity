using UnityEngine;
using System.Collections;

public static class Constants {
	public const float PLAYER_SPEED = 10f;
	public const int PLAYER_INITIAL_LIVES = 3;
	public const int NEW_LIFE_SCORE = 100;

	public const float BULLETS_SPEED = 10f;

	public const float JUMPER_ENEMIES_SPEED = 2f;
	public const int ENEMIES_ROWS_NUMBER = 4;
	public const int ENEMIES_COLUMNS_NUMBER = 6;
	public const int ENEMIES_ROWS_DISTANCE = 1;
	public const int ENEMIES_COLUMNS_DISTANCE = 2;
	public const float ENEMIES_JUMPS_INTERVAL = 5f;
	public const float ENEMIES_LIVES_LEVEL_FACTOR = 0.1f;

	// Must add up to 1
	public const float ENEMY_A_INITIAL_PROBABILITY = 0.90f;
	public const float ENEMY_B_INITIAL_PROBABILITY = 0.05f;
	public const float ENEMY_C_INITIAL_PROBABILITY = 0.05f;
	public const float ENEMY_D_INITIAL_PROBABILITY = 0f;
	public const float ENEMY_E_INITIAL_PROBABILITY = 0f;
	// Must add up to 0
	public const float ENEMY_A_PROBABILITY_LEVEL_FACTOR = -0.04f;
	public const float ENEMY_B_PROBABILITY_LEVEL_FACTOR = 0.015f;
	public const float ENEMY_C_PROBABILITY_LEVEL_FACTOR = 0.015f;
	public const float ENEMY_D_PROBABILITY_LEVEL_FACTOR = 0.005f;
	public const float ENEMY_E_PROBABILITY_LEVEL_FACTOR = 0.005f;
}
