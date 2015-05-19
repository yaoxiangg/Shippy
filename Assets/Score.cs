using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	static public void AddPoint() {
		if(instance.ship.dead)
			return;

		score++;

		if(score > highScore) {
			highScore = score;
		}
	}

	ShipMovement ship;

	void Start() {
		instance = this;
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");
		if(player_go == null) {
			Debug.LogError("Could not find an object with tag 'Player'.");
		}

		ship = player_go.GetComponent<ShipMovement>();
		score = 0;
		highScore = PlayerPrefs.GetInt("highScore", 0);
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt("highScore", highScore);
	}

	void Update () {
		guiText.fontSize = 20;
		guiText.text = "X: " + ship.transform.position.x + " Y: " + ship.transform.position.y;
	}
}
