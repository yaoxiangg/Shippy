using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (pos.y > 10) {
			return;
		}
		pos.y = pos.y + 0.04f;
		transform.position = pos;
	}
}
