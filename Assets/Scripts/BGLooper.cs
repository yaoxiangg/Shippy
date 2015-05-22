using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	int numBGPanels = 3;

	void Start() {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		float heightOfBGObject = ((BoxCollider2D)collider).size.x;
		Vector3 pos = collider.transform.position;
		//Debug.Log ("Triggered: " + heightOfBGObject);
		pos.y += (heightOfBGObject * numBGPanels);
		collider.transform.position = pos;
	}
}
