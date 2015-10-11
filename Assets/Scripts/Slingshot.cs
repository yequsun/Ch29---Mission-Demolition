using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _____________________;
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;
	// Use this for initialization
	static public Slingshot S;
	void Awake(){
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
		S = this;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!aimingMode) {
			return;
		}
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);
		Vector3 mouseDelt = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelt.magnitude > maxMagnitude) {
			mouseDelt.Normalize ();
			mouseDelt *= maxMagnitude;
		}

		Vector3 projPos = launchPos + mouseDelt;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) {
			aimingMode = false;
			projectile.rigidbody.isKinematic = false;
			projectile.rigidbody.velocity = -mouseDelt * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
			MissionDemolition.ShotFired();
		}
	
	}

	void OnMouseEnter(){
		//print ("Slingshot:OnMouseEnter()");
		launchPoint.SetActive (true);
	}

	void OnMouseExit(){
		//print ("Slingshot:OnMouseExit()");
		launchPoint.SetActive (false);
	}

	void OnMouseDown(){
		aimingMode = true;
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
		projectile.rigidbody.isKinematic = true;
	}
}
