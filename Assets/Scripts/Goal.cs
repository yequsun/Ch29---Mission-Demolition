using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	static public bool goalMet = false;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Projectile") {
			Goal.goalMet = true;
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
