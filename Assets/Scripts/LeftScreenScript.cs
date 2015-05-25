using UnityEngine;
using System.Collections;

public class LeftScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//GENERATE INITIAL CUBES OF RANDOM COLORS
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 6; j++) {
				spawnNewCube(i, j);
			}
		}
	}	

	//Create a cube at the top
	private void spawnNewCube(int row, int col) {
		int val = Random.Range (0, 8);
		GameObject cube = (GameObject) Instantiate(Resources.Load("Rune")); 
		SpriteRenderer sr = cube.GetComponent<SpriteRenderer>(); 
		sr.sprite = StartScreenScript.runeSprite[val];
		float x = (float)((col * 2.5) + 1 - 17.1);
		float y = (float)((row * 2.5) + 0.3 + 0.922);
		float z = (float)(15 + 6.2);
		Vector3 position = new Vector3(x, y, z);
		cube.transform.position = position;
	}
}
