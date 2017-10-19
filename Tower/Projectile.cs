using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "enemy")
		{	
			Debug.Log("hit!");
			Destroy(other.gameObject);
		}
	}
}
