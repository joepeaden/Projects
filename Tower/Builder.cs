using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
	public static int score;

	// valid values: "TowerSection" 
	public void build(string b)
	{
		if(b == "TowerSection")
		{
			Debug.Log("Building section!");
		}
	}
}
