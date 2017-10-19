using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// NOTE: Need 3d collider for raycast to detect.

	public int power;
	public GameObject shotPrefab;

	void Start()
	{
		power = 100;
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray))
			{
				GameObject shotInstance;
				// Fix for position being recorded behind the scene
				Vector3 pos = new Vector3(ray.GetPoint(10).x, 
					ray.GetPoint(10).y, ray.GetPoint(10).z - .3f);
				shotInstance = Instantiate(shotPrefab, transform.position, Quaternion.identity);
				shotInstance.GetComponent<Rigidbody>().AddForce(pos * power);
			}
		}
	}

}
