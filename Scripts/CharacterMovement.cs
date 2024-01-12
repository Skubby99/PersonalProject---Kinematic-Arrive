using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [SerializeField] private Transform formationLeader;
    [SerializeField] private float formationDistanceOffset;
    [SerializeField] private float formationRotationOffset;
	[SerializeField] private Transform trans;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Vector3 target;
	[SerializeField] private Quaternion targetRotation;


	private float maxSpeed;
	private float radiusOfSat;

	public float smooth = 5.0f;
	public bool active;

	void getTarget(){
		//checks if left mouse button was clicked and the object is not in motion
		if (Input.GetMouseButtonDown (0) && active == false) {
			//creates a Ray from the camera to the mouse's position
			Ray dir = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			//checks if Ray hits an object in the scene
			if (Physics.Raycast (dir, out hit)) {
				target = hit.point;
				active = true;
			}
		}
		//after getting target orientates object to face it 
		setOrientation ();
	}

	void setOrientation(){
		//normalizes the distance between target and player
		Vector3 towardsNormal = (target - trans.position).normalized;
		//normalizes players position
		Vector3 playerPosNormal = trans.position.normalized;
		//calculates quaternion angles
		targetRotation = Quaternion.LookRotation (towardsNormal);
		//checks to see if the player is facing the same direction as target =1, 0 if perpendicular, -1 if parallel but facing the opposite direction
		if (Vector3.Dot (playerPosNormal, towardsNormal) < 0) {
			//rotates the player over Time form current player roation to target rotation
			trans.rotation = Quaternion.Lerp (trans.rotation, targetRotation, smooth * Time.deltaTime);
			//locks the x and z axis from rotating only allow the y axis to be manipulated
			trans.localEulerAngles = new Vector3 (0, trans.localEulerAngles.y, 0);
		}
		//moving player to target
		getSteering ();
	}

	void getSteering(){
		// Calculate vector from character to target
		Vector3 towards = target - trans.position;

		// If we haven't reached the target yet
		if (towards.magnitude > radiusOfSat) {

			// Normalize vector to get just the direction
			towards.Normalize ();
			towards *= maxSpeed;
		} 
		// Moves character
		rb.velocity = towards;
		//checks the velocity of player to see if the player is still in motion
		if (rb.velocity.magnitude < 1)
			active = false;
	}
	

	void Start () {
		//initalize all the variables declared
		active = false;
		maxSpeed = 5f;
		radiusOfSat = .1f;
		trans = this.transform;
		rb = this.GetComponent<Rigidbody> ();
		targetRotation = new Quaternion(0f, 0f, 0f, 1f);
		target = new Vector3 (0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//click detection
		getTarget ();

	}
}
