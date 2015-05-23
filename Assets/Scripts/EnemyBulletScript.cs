using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {

	private float bulletMoveSpeed = 0.01f;
	private int bulletDamage = 1;
	
	// Update is called once per frame
	void Update () {
		//Update enemy bullet position (Should it be tracer bullet?)
		Vector3 pos = transform.position;
		if (pos.y < -5) {
			return;
		}
		pos.y = pos.y - bulletMoveSpeed;
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
}
