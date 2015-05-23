using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	public static bool shipInitialised = false;

	private GameObject playerShip;
	private double leftX = -1.5;
	private double rightX = 1.5;
	private GameObject bullet;

	// Use this for initialization
	void Start () {
		bullet = GameObject.FindGameObjectWithTag("Bullet");
		playerShip = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("spawnBullet", 2f, 0.1f);
		InvokeRepeating("spawnEnemyShip", 2f, 3f);
	}

	// Do Graphic & Input updates here
	void Update() {
	}

	void updateShipLocation() {
		if(playerShip == null) {
			Debug.LogError("Could not find an object with tag 'Player'.");
		}
		Vector3 shipPos = playerShip.transform.position;
		if (playerShip.transform.position.x < leftX) {
			shipPos.x = (float)leftX;
			playerShip.transform.position = shipPos;
		}
		if (playerShip.transform.position.x > rightX) {
			shipPos.x = (float)rightX;
			playerShip.transform.position = shipPos;
		}
	}
	// Do physics engine updates here
	void FixedUpdate () {
		if(playerShip.GetComponent<Ship>().isDead())
			return;
		if (!shipInitialised) {
			moveShipToStartPoint(playerShip);
		}
		updateShipLocation();
	}

	void moveShipToStartPoint(GameObject playerShip) {
		Vector3 shipPos = playerShip.transform.position;
		shipPos.y += 0.01f;
		if (shipPos.y > -1f) {
			shipInitialised = true;
		}
		playerShip.transform.position = shipPos;
	}

	void spawnBullet() {
		Vector3 pos = playerShip.transform.position;
		pos.y = pos.y + 0.3f;
		GameObject bulletClone = (GameObject) Instantiate(bullet, pos, playerShip.transform.rotation);
		Destroy (bulletClone, 2.5f);
	}

	void spawnEnemyShip() {
		GameObject enemyShip = GameObject.FindGameObjectWithTag("Enemy");
		Vector3 enemyShipPos = EnemyShip.generateRandomEnemyPos();
		GameObject enemyShipClone = (GameObject) Instantiate(enemyShip, enemyShipPos, enemyShip.transform.rotation);
		//Initialise Enemy Ship Details - Bullet Speed, Bullet Damage
		enemyShipClone.GetComponent<EnemyShip> ().setBulletSpeed (Random.Range (0.01f, 0.02f));
		enemyShipClone.GetComponent<EnemyShip> ().setBulletDamage (Random.Range (1, 6));
	}
}