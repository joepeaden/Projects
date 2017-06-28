using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryScript : MonoBehaviour {

	private GameObject player;

	void Start (){
		player = GameObject.Find ("Player");
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject == player)
			player.GetComponent<playerController> ().isDead = true;
		else
			Destroy (other.gameObject);
	}
}
