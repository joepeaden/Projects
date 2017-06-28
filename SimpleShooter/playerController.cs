using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

	public float speed; // For speed of player movement
	public float jump; // For size of player jump
	public GameObject projectile; 
	[HideInInspector]
	public int prodirection; // Projectile direction
	[HideInInspector]
	public float fireRate; // Fire rate, the lower the faster
	[HideInInspector]
	public bool isDead;
	[HideInInspector]
	public string weapon;
	[HideInInspector]
	public int ammo;

	private Text ammoText;
	private AudioSource audio;
	private float lastShot = 0; // Used for fire pauses
	private Rigidbody2D rb; // Player rigidbody for jumping
	private Transform pTransform;
	private GameObject controller;
	//--------------------------------------------- EVENT FUNCTIONS ---------------------------------------------

	void Start () {
		
		audio = GetComponent<AudioSource>();
		audio.Play();

		ammo = 30;

		prodirection = 1; // Initial projectile direction to the right
		isDead = false;
		rb = GetComponent<Rigidbody2D>(); // Assigning rb to the player Rigidbody for jump
		pTransform = GetComponent<Transform>();
		controller = GameObject.Find ("GameController");
		ammoText = GameObject.Find ("ammoText").GetComponent<Text>();
		SetAmmo();
		weapon = "Pistol";
	}


	void Update ()
	{
		
		Move ();

		if (Input.GetKey (KeyCode.Space) && Time.time > fireRate + lastShot) {
			Fire ();	
		}
		if (isDead == true){
			Die ();
		}
		SetAmmo();	
	}

	void FixedUpdate ()
	{

		// Jump Control
		if (Input.GetKeyDown (KeyCode.W))
			rb.velocity = new Vector3 (0, jump, 0);
	}

	//--------------------------------------------- FUNCTIONS ---------------------------------------------

	void Fire () 	// Fire Controls
	{
		if (weapon == "Pistol"){
			fireRate = .5f;
			Instantiate (projectile, transform.position, transform.rotation);
			lastShot = Time.time;		
		}else{
			if (weapon == "MachineGun")
				fireRate = .1f;
			else if(weapon == "Lazer")
				fireRate = .05f;
			if(ammo >= 1){
				Instantiate (projectile, transform.position, transform.rotation);
				lastShot = Time.time;
				ammo -= 1;
			}else{
				Debug.Log("Out of ammo!");
				weapon = "Pistol";
			}
		}
	}

	void Move () // Movement Controls
	{
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			prodirection = -1;
		}
		if (Input.GetKey (KeyCode.D)){
			transform.Translate (Vector3.right * speed * Time.deltaTime);	
			prodirection = 1;
		}
	}

	void Die (){
		Debug.Log ("You died");
		isDead = false;
		controller.GetComponent<gameController> ().lives -= 1;
		pTransform.position = new Vector3 (0, 0, 0);
		print (controller.GetComponent<gameController>().lives);
	}

	void SetAmmo (){// Updates text for player lives and kills
		if (weapon == "Pistol")
			ammoText.text = "Ammo: infinite / Pistol";
		else
			ammoText.text = "Ammo: " + ammo.ToString () + " / " + weapon;
	}

}	
