using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float moveSpeed;
	
	private Transform castle;

	void Start()
	{
		castle = GameObject.Find("Castle Base").GetComponent<Transform>();
		moveSpeed = .05f;
	}

	void Update()
	{
		// Move towards player
		if(Vector2.Distance(castle.position, transform.position) > 1)
		{
			// getting 1 (if enemy on left) or -1 (if enemy on right)
			float direction = -(transform.position.x/Mathf.Abs(transform.position.x));
			transform.Translate(new Vector3(direction * moveSpeed, 0, 0));
		}
	}

	void OnDestroy()
	{
		Builder.score++;
		GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + Builder.score; 
	}

}
