using UnityEngine;
using System.Collections;

public class ShipMovement : TouchLogicV2 {

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
	public override void Update() {
		base.Update();
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

	void UpdateShipPosition()
	{
		float zPos = shipTrans.position.z;
		Vector3 fingerPos = new Vector3(Input.GetTouch(touch2Watch).position.x, Input.GetTouch(touch2Watch).position.y, zPos);
		//z of ScreenToWorldPoint is distance from camera into the world, so we need to find this object's distance from the camera to make it stay on the same plane
		Vector3 realWorldPos = Camera.main.ScreenToWorldPoint(fingerPos);
		//Convert screen to world space
		realWorldPos.z = zPos;
		shipTrans.position = realWorldPos;
	}

	public override void OnTouchMovedAnywhere()
	{
		UpdateShipPosition();
	}
	public override void OnTouchStayedAnywhere()
	{
		UpdateShipPosition();
	}
	public override void OnTouchBeganAnywhere()
	{
		touch2Watch = TouchLogicV2.currTouch;
	}
}