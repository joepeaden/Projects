using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunker : MonoBehaviour 
{

	public GameObject blackHoleyCrap;
	public GameObject[] chunkList;
	public GameObject[] dangerList;
	public int chunkLength;

	private List<GameObject> instList; // Instantiated List of chunks

	public enum mode
	{
		up, down, left, right
	}

	public mode currentMode = mode.right;

	// WANT TO GET RANDOM PORTAL GENERATION INCREASING IN FREQUENCY

	void Start()
	{
		instList = new List<GameObject> ();
		instList.Add(GameObject.Find("StartSection"));
		switch (currentMode) 
		{
		case mode.right:
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.5f, 5f);
			GetComponent<BoxCollider2D> ().offset = new Vector2 (-5, 0);
			break;
		case mode.down:
			GetComponent<BoxCollider2D> ().size = new Vector2 (5f, 0.5f);
			GetComponent<BoxCollider2D> ().offset = new Vector2 (0, -5);
			break;
		case mode.left:
			GetComponent<BoxCollider2D> ().size = new Vector2 (0.5f, 5f);
			GetComponent<BoxCollider2D> ().offset = new Vector2 (5, 0);
			break;
		case mode.up:
			GetComponent<BoxCollider2D> ().size = new Vector2 (5f, 0.5f);
			GetComponent<BoxCollider2D> ().offset = new Vector2 (0, 5);
			break;
		default:
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && other is BoxCollider2D)
		{
			int choice = Random.Range (0, chunkList.Length);
			if (instList.Count > 1) {
				Destroy (instList [0]);
				instList.RemoveAt (0);
			}
			Vector2 pos;
			GameObject newObj;
			int chance = Random.Range (0, 3);
			int danger = Random.Range (3, 7); // being range of # objects to spawn
			switch (currentMode) {
			case mode.right:
				pos = new Vector2 (instList [0].GetComponent<Transform> ().position.x + chunkLength, transform.position.y);
				newObj = Instantiate (chunkList [choice], pos, Quaternion.identity);
				instList.Add (newObj);	
				GetComponent<BoxCollider2D> ().offset = 
					new Vector2 (GetComponent<BoxCollider2D> ().offset.x + chunkLength, 0);
				// Portal generation chance
				if (chance == 0) {
					Instantiate (blackHoleyCrap, pos, Quaternion.Euler (0, 0, 90)); 
				}
				// Object generation
				for (int i = 0; i < danger; i++)
					Instantiate (dangerList [Random.Range (0, dangerList.Length - 1)], new Vector2 ((pos.x + Random.Range(-15, 15)), (pos.y + Random.Range(-3.5f, 3.5f))), Quaternion.identity);
				break;
			case mode.down:
				pos = new Vector2 (transform.position.x, instList [0].GetComponent<Transform> ().position.y - chunkLength);
				newObj = Instantiate (chunkList [choice], pos, Quaternion.Euler (new Vector3 (0, 0, 90)));
				instList.Add (newObj);	
				GetComponent<BoxCollider2D> ().offset = 
					new Vector2 (0, GetComponent<BoxCollider2D> ().offset.y - chunkLength);
				// Portal generation chance
				if (chance == 0) {
					Instantiate (blackHoleyCrap, pos, Quaternion.identity); 
				}
				// Object generation
				for (int i = 0; i < danger; i++)
					Instantiate (dangerList [Random.Range (0, dangerList.Length - 1)], new Vector2 ((pos.x + Random.Range(-3.5f, 3.5f)), (pos.y + Random.Range(-15, 15))), Quaternion.identity);
				break;
			case mode.left:
				pos = new Vector2 (instList [0].GetComponent<Transform> ().position.x - chunkLength, transform.position.y);
				newObj = Instantiate (chunkList [choice], pos, Quaternion.Euler(0, 0, 0));
				instList.Add (newObj);	
				GetComponent<BoxCollider2D> ().offset = 
					new Vector2 (GetComponent<BoxCollider2D> ().offset.x - chunkLength, 0);
				// Portal generation chance
				if (chance == 0) {
					Instantiate (blackHoleyCrap, pos, Quaternion.Euler(0, 0, -90));
				}
				// Object generation
				for (int i = 0; i < danger; i++)
					Instantiate (dangerList [Random.Range (0, dangerList.Length - 1)], new Vector2 ((pos.x + Random.Range(-15, 15)), (pos.y + Random.Range(-3.5f, 3.5f))), Quaternion.identity);
				break;
			case mode.up:
				pos = new Vector2 (transform.position.x, instList [0].GetComponent<Transform> ().position.y + chunkLength);
				newObj = Instantiate (chunkList [choice], pos, Quaternion.Euler (new Vector3 (0, 0, 90)));
				instList.Add (newObj);	
				GetComponent<BoxCollider2D> ().offset = 
					new Vector2 (0, GetComponent<BoxCollider2D> ().offset.y + chunkLength);
				// Portal generation chance
				if (chance == 0) {
					Instantiate (blackHoleyCrap, pos, Quaternion.Euler(0, 0, 180)); 
				}
				// Object generation
				for (int i = 0; i < danger; i++)
					Instantiate (dangerList [Random.Range (0, dangerList.Length - 1)], new Vector2 ((pos.x + Random.Range(-3.5f, 3.5f)), (pos.y + Random.Range(-15, 15))), Quaternion.identity);
				break;
			default:
				break;
			}
		}
	}

}
