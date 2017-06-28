using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

	public float speed;
	public float jump;

	private Rigidbody2D rb;

	private GameObject player;

	void Start () 
	{
		speed = 2;
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.Find ("Player");
	}

	void Update ()
	{
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
	}

	void OnTriggerExit2D (Collider2D platformbound)
	{
		if (platformbound.tag == "Platformbound") 
		{
			Debug.Log ("Platform");
			if (player.transform.position.y >= transform.position.y) 
			{
				rb.velocity = new Vector3 (0, jump, 0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "Player") 
			player.GetComponent<playerController> ().isDead = true;
		}
}

