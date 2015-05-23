using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {

	private GameObject enemyShip;
	private GameObject bullet;
	private Vector3 enemyShipPos;
	private int healthPoints;
	private float shipBulletSpeed;
	private int shipBulletDamage;
	private Animator animator;


	// Use this for initialization
	void Start () {
		bullet = GameObject.FindGameObjectWithTag("EnemyBullet");
		enemyShip = this.gameObject;
		animator = transform.GetComponentInChildren<Animator>();
		if(animator == null) {
			//Debug.LogError("Didn't find animator for Enemy!");
		}
		if (enemyShipPos.x < 1.27f)
			InvokeRepeating("spawnEnemyBullet", 0f, Random.Range (2.5f, 3f));
	}
	
	// Update is called once per frame
	void Update () {
		//MoveShip to location
		Vector3 pos = enemyShip.transform.position;
		if (pos.y < 0.5) {
			return;
		}
		pos.y -= 0.005f;
		enemyShip.transform.position = pos;
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
		enemyBulletClone.GetComponent<EnemyBulletScript>().setSpeed (shipBulletSpeed);
		enemyBulletClone.GetComponent<EnemyBulletScript>().setBulletDamage (shipBulletDamage);
		Destroy (enemyBulletClone, 5.5f);
	}

	public void setBulletSpeed(float val) {
		this.shipBulletSpeed = val;	
	}

	public void setBulletDamage(float val) {
		this.shipBulletDamage = (int) val;	
	}

	//Bullet hit
	void OnTriggerEnter2D(Collider2D otherObject) {
		if (otherObject.tag.Equals ("Bullet")) {
			if (healthPoints < 0) {
				animator.SetTrigger ("Death"); 
				//Destroy object
			} else {
				//Deduct health
				if (otherObject.tag.Equals ("EnemyBullet")) {
					healthPoints -= otherObject.GetComponent<EnemyBulletScript>().getBulletDamage();
					//Debug.Log ("HIT MY SHIP! - HP: " + healthPoints);
				}
			}
		}
	}
}
