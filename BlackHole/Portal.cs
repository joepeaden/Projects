using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public MovementDirection directionToTeleport = MovementDirection.Right;

	// Use this for initialization
	void Start () {
		int choice = Random.Range (0, 4);
		switch (choice) 
		{
		case 0:
			directionToTeleport = MovementDirection.Right;
			break;
		case 1:
			directionToTeleport = MovementDirection.Down;
			break;
		case 2:
			directionToTeleport = MovementDirection.Up;
			break;
		case 3:
			directionToTeleport = MovementDirection.Left;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void LoadScene()
    {
        SceneManager.LoadScene(directionToTeleport.ToString());
		Score.diffinc += 0.5f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            LoadScene();
        }

    }

}
