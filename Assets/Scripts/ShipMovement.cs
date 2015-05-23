using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private Transform shipTrans;
	private GameObject playerShip;
	private double leftX = -1.632;
	private double rightX = 1.632;
	public GameObject bullet;
	public float bulletSpeed = 100;
	public static bool shipInitialised = false;

	Animator animator;
	public bool godMode = false;
	public bool dead = false;
	float deathCooldown;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		bullet = GameObject.FindGameObjectWithTag("Bullet");
		playerShip = GameObject.FindGameObjectWithTag("Player");
		shipTrans = this.transform;
		InvokeRepeating("spawnBullet", 2f, 0.1f);
		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	// Do Graphic & Input updates here
	void Update() {
		//base.Update();
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
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
		if(dead)
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

	void OnCollisionEnter2D(Collision2D collision) {
		if(godMode)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
	}

	void spawnBullet() {
		Vector3 pos = playerShip.transform.position;
		pos.y = pos.y + 0.3f;
		GameObject bulletClone = (GameObject) Instantiate(bullet, pos, playerShip.transform.rotation);
		Destroy (bulletClone, 2.5f);
	}
}