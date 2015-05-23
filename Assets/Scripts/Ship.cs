using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	private int healthPoints = 100;
	private Animator animator;
	
	private bool godMode = false;
	private bool dead = false;
	private float deathCooldown;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(dead) {
			deathCooldown -= Time.deltaTime;
			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otherObject) {
		if(godMode)
			return;
		if (healthPoints < 0) {
			animator.SetTrigger ("Death");
			dead = true;
			deathCooldown = 0.5f;
		} else {
			//Deduct health
			if (otherObject.tag.Equals ("EnemyBullet")) {
				healthPoints -= otherObject.GetComponent<EnemyBulletScript>().getBulletDamage();
				//Debug.Log ("HIT MY SHIP! - HP: " + healthPoints);
			}
		}
	}

	public bool isDead() {
		return dead;
	}
}
