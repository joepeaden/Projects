using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour {

	public GameObject enemy;

	private float spawnDelay;
	private Transform location;
	private float diffInc; // difficulty incrementer
	private float diffDel; // time between difficulty increment

	void Start (){
		spawnDelay = 1f;
		location = GetComponent<Transform>();
		diffInc = 0f;
		diffDel = 5f;
	}

	void Update (){

		diffDel -= Time.deltaTime;
		if (diffDel < 0 && spawnDelay > 0){
			diffInc += .1f;
			diffDel = 5f;
			Debug.Log ("Incrementing difficulty: " + diffInc.ToString());
		}

		spawnDelay -= Time.deltaTime;
		if (spawnDelay < 0){
			Instantiate (enemy, location.position, Quaternion.identity);
			spawnDelay = 1f - diffInc;
		}
	}

}