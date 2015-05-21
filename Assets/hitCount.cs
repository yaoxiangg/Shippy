using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hitCount : MonoBehaviour {

	static int hit = 0;
	Text txtBox;

	static public void AddHit() {
		hit++;
	}

	void Start() {
		txtBox = GetComponent<Text>();
		hit = 0;
	}

	void OnDestroy() {
	}

	void Update () {
		txtBox.text = "Hit Count: " + hit;
	}
}
