using UnityEngine;
using System.Collections;

public class BGMovement : MonoBehaviour {
	
	public float Speed = 0.2f;
	private float initialisedTime = 0;
	// Update is called once per frame
	void Update () {
		if (ShipMovement.shipInitialised) {
			this.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0f, (Time.time - initialisedTime) * Speed);
		} else {
			initialisedTime = Time.time;
		}
	}
}
