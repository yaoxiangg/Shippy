using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private Transform shipTrans;
	private double leftX = -1.632;
	private double rightX = 1.632;

	Animator animator;
	public bool godMode = false;
	public bool dead = false;
	float deathCooldown;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();
		shipTrans = this.transform;
		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	// Do Graphic & Input updates here
	void Update() {
		//base.Update();
		Vector3 pos = shipTrans.position;
		pos.y = pos.y + 0.01f;
		if (pos.x < leftX) {
			pos.x = (float)leftX;
		}
		if (pos.x > rightX) {
			pos.x = (float)rightX;
		}
		shipTrans.position = pos;

		correctShipXAxisLimit();
		
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
	}
	
	void correctShipXAxisLimit() {
		GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
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
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(godMode)
			return;

		animator.SetTrigger("Death");
		dead = true;
		deathCooldown = 0.5f;
	}
}