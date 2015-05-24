using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	//EnemyShip Attributes
	private Vector3 enemyShipPos; 	//Position
	private int healthPoints;		//Health Points
	private float shipBulletSpeed;	//Bullet Speed
	private int shipBulletDamage;	//Bullet Damage
	private bool alive;				//Ship Alive?

	private GameObject enemyShip;
	private GameObject bullet;
	private Animator animator;


	// Use this for initialization
	void Start () {
		bullet = GameObject.FindGameObjectWithTag("EnemyBullet");
		enemyShip = this.gameObject;
		animator = enemyShip.GetComponent<Animator>();
		alive = true;
		if(animator == null) {
			Debug.LogError("Didn't find animator for Enemy!");
		}
		if (enemyShipPos.x < 1.27f && enemyShip.tag.Equals ("Untagged"))
			InvokeRepeating("spawnEnemyBullet", 0f, Random.Range (2.5f, 3f));
	}
	
	// Update is called once per frame
	void Update () {
		//MoveShip to location
		if (StartScreenScript.started) {
			if (!alive)
				return;
			Vector3 pos = enemyShip.transform.position;
			if (pos.y < 0.5) {
				return;
			}
			pos.y -= 0.005f;
			enemyShip.transform.position = pos;
		}
	}

	public static Vector3 generateRandomEnemyPos() {
		//Need to check for collision?
		return new Vector3(Random.Range(-1.27f, 1.27f), 2, 0);
	}

	void spawnEnemyBullet() {
		Vector3 pos = enemyShip.transform.position;
		pos.y = pos.y - 0.2f;
		GameObject enemyBulletClone = (GameObject) Instantiate(bullet, pos, enemyShip.transform.rotation);
		//Set Bullet Parameters
		enemyBulletClone.tag = "EnemyBulletClone";
		enemyBulletClone.GetComponent<EnemyBulletScript>().setSpeed (shipBulletSpeed);
		enemyBulletClone.GetComponent<EnemyBulletScript>().setBulletDamage (shipBulletDamage);
		Destroy (enemyBulletClone, 5.5f);
	}

	//Bullet hit
	void OnTriggerEnter2D(Collider2D otherObject) {
		if (otherObject.tag.Equals ("PlayerBulletClone")) {
			healthPoints -= otherObject.GetComponent<BulletScript>().getBulletDamage();
			if (healthPoints <= 0) {
				alive = false;
				animator.SetTrigger ("EnemyShipDeath"); 
				Destroy (gameObject.GetComponent<CircleCollider2D>(), 0f);
				Destroy (gameObject, 1f);
				//Destroy object
			}
		}
	}

	public int getHealthPoints {
		get {
			return healthPoints;
		}
	}

	public void setHealthPoints(int hp) {
		healthPoints = hp;
	}

	public void setBulletSpeed(float val) {
		this.shipBulletSpeed = val;	
	}
	
	public void setBulletDamage(float val) {
		this.shipBulletDamage = (int) val;	
	}

}
