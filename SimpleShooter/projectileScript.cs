using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileScript : MonoBehaviour {

	// Bullet speed
	public int speed;

	// Projectile direction from player direction
	private playerController player;
	// Projectile rigidbody for launch
	private Rigidbody2D rb;
	private AudioSource audio;

	void Start(){
		audio = GetComponent<AudioSource>();
		audio.Play();
		// Retrieving player direction variable
		player = GameObject.Find ("Player").GetComponent<playerController>();
		// Launching the projectile. Intending to make public variable
		// to adjust starting direction based on player direction, +/-
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector3 (player.prodirection*speed,0,0);
	}

	void OnTriggerEnter2D(Collider2D other){

		if (player.weapon == "Lazer")
			GetComponent<Renderer>().material.color = Color.magenta;
		if (other.gameObject.tag == "Enemy") {
			// Destroy on contact w/ enemy
			Destroy (gameObject);
			Destroy (other.gameObject);
			GameObject.Find ("GameController").GetComponent<gameController>().score += 1;
		}
	}

}
