using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	int numBGPanels = 3;

	void Start() {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log ("Triggered: " + collider.name);
		float heightOfBGObject = ((BoxCollider2D)collider).size.y;
		Vector3 pos = collider.transform.position;
		pos.y += heightOfBGObject * numBGPanels;
		collider.transform.position = pos;
	}
}
