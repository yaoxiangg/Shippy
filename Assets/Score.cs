using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	static public void AddPoint() {
		score++;

		if(score > highScore) {
			highScore = score;
		}
	}

	GameObject ship;

	void Start() {
		GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
		if(playerShip == null) {
			Debug.LogError("Could not find an object with tag 'Player'.");
		}

		ship = playerShip;
		score = 0;
		highScore = PlayerPrefs.GetInt("highScore", 0);
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt("highScore", highScore);
	}

	void Update () {
		GetComponent<GUIText>().fontSize = 20;
		GetComponent<GUIText>().text = "X: " + ship.transform.position.x + " Y: " + ship.transform.position.y;
	}
}
