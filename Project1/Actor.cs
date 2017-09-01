using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {

	/*
	All characters inherit from this class.
	*/

	public bool shutdown; // stops movement on hit
	public float shutTimer; // shutdown timer

	// Attack variables
	public float attackDelay; // Delay before actor strikes

	public int hp; // health points
	public int moveSpeed; // actor move speed

	public GameObject target; // target that is in range for attack
	public Transform sRange; // (short range) Melee range child object

	public abstract void attack();
	public abstract void move();
	public abstract void shutDown(); // Override for stopping all movement 
	public abstract void die();

	public float lastPos; // last frame, position that actor was at
	public float currentDirection; // detecting current direction that actor is facing 
	public float getDir(float lastPos, GameObject i) // getting current direction of face
	{
		float curDir = i.GetComponent<Transform>().position.x - lastPos;
		return curDir;
	}	
}
