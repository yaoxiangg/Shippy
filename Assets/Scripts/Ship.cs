using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	private int healthPoints = 100;
	private Animator animator;
	private GameObject playerShip;
	private GameObject bullet;

	private bool godMode = false;
	private bool dead = false;
	private float deathCooldown;

	// Use this for initialization
	void Start () {
		bullet = GameObject.FindGameObjectWithTag("Bullet");
		playerShip = GameObject.FindGameObjectWithTag("Player");
		animator = playerShip.GetComponent<Animator>();
		if(animator == null) {
			Debug.LogError("Didn't find player animator!");
		}
		
		InvokeRepeating("spawnBullet", 2f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(dead) {
			deathCooldown -= Time.deltaTime;
			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel(1);
					//Reset globals
					ShipMovement.shipInitialised = false;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D otherObject) {
		if(godMode)
			return;
		if (healthPoints <= 0) {
			dead = true;
			//animator.SetTrigger ("ShipDeath"); 
			Destroy (gameObject.GetComponent<CircleCollider2D>(), 0f);
			Destroy (gameObject, 1f);
			deathCooldown = 2f;
		} else {
			if (otherObject.tag.Equals ("EnemyBullet")) {
				//Debug.Log ("HIT MY SHIP! - HP: " + healthPoints);
				healthPoints -= otherObject.GetComponent<EnemyBulletScript>().getBulletDamage();
			}
		}
	}

	public bool isDead() {
		return dead;
	}
	
	void spawnBullet() {
		Vector3 pos = playerShip.transform.position;
		pos.y = pos.y + 0.3f;
		GameObject bulletClone = (GameObject) Instantiate(bullet, pos, playerShip.transform.rotation);
		bulletClone.GetComponent<BulletScript> ().setBulletDamage(1);
		Destroy (bulletClone, 2.5f);
	}

	public int getHealthPoints(){
		return healthPoints;
	}
}
