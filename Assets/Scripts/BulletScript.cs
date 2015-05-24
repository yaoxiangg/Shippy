using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private float bulletMoveSpeed = 0.01f;
	private int bulletDamage = 1;

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (pos.y > 10) {
			return;
		}
		pos.y = pos.y + 0.05f;
		transform.position = pos;
	}

	public void setSpeed(float val) {
		this.bulletMoveSpeed = val;	
	}
	
	public void setBulletDamage(int val) {
		this.bulletDamage = val;	
	}
	
	public int getBulletDamage() {
		return this.bulletDamage;	
	}

	void OnTriggerEnter2D(Collider2D otherObject) {
		//Destroy bullet on contact
		if (otherObject.tag.Equals ("Enemy")) {
			Destroy (gameObject);
		}
	}
}
