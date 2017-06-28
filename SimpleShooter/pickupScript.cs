using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			// Destroy on contact w/ player
			// Accessing player game object for weapon alteration
			col.gameObject.GetComponent<playerController>().weapon = "MachineGun";
			col.gameObject.GetComponent<playerController>().ammo = 30;
			//GameObject.Find ("GameController").GetComponent<gameController>().lives += 1;
			Destroy (gameObject);
		}

	}
}

