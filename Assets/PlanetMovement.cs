using UnityEngine;
using System.Collections;

public class PlanetMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = pos.y + 0.007f;
		transform.position = pos;
	}
}
