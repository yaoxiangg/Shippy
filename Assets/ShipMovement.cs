using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private Transform shipTrans;

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
		shipTrans.position = pos;
		if(dead) {
			deathCooldown -= Time.deltaTime;

			if(deathCooldown <= 0) {
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
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