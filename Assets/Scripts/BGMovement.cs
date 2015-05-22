using UnityEngine;
using System.Collections;

public class BGMovement : MonoBehaviour {
	
	public float Speed = 0.2f;

	// Update is called once per frame
	void Update () {
		if (Time.time > 1)
		this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0f, (Time.time - 1 )* Speed);
	}
}
