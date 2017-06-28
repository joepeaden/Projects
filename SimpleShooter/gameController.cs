using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

	public int lives;
	public int score; // For enemies killed, duh
	public Text livesText; // Text for displaying lives remaining
	public Text scoreText; // Text displays enemies killed

	private int ammo;

	void Start(){

		lives = 3;
		score = 0;

		SetStatsText();

	}

	void Update(){

		SetStatsText ();

		SpawnWave ();
	}
		
	void SetStatsText (){// Updates text for player lives and kills

		livesText.text = "Lives Remaining: " + lives.ToString ();
		scoreText.text = "Enemies Killed: " + score.ToString ();
		
	}

	IEnumerator SpawnWave (){
		yield return new WaitForSeconds(5f);
	}

}
