

using UnityEngine;
using System.Collections;

public class EnemyGreen_Script : MonoBehaviour 
{

	//Public Var
	public float speed;						//Enemy Ship Speed -->customizing the difficulty level
	public int health;                      //Enemy Ship Health  -->customizing the difficulty level
	public GameObject LaserGreenHit;		//LaserGreenHit Prefab -->player shot has been inflicted on the enemy
	public GameObject Explosion;			//Explosion Prefab
	public int ScoreValue;                  //How much the Enemy Ship give score after explosion  -->customizing the difficulty level
	public GameObject shot; 				//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	public float fireRate = 1F;				//Fire Rate between Shots

	//Private Var
	private float nextFire = 0.0F; 			//First fire & Next fire Time


	// Use this for initialization
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed;	//Enemy Ship Movement //transform.up-->yaxis(transform.position.y) transform.position.x-->transform.right //
	}

	// Update is called once per frame
	void Update () 
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 									//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 		//Instantiate fire shot 
			GetComponent<AudioSource>().Play (); 														//Play Fire sound
		}
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerLaser")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 		//Instantiate LaserGreenHit 
			Destroy(other.gameObject); 													//Destroy the Other (PlayerLaser)
			
			//Check the Health if greater than 0
			if(health > 0)
				health--; 																//Decrement Health by 1

			//Check the Health if less or equal 0
			if(health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 		//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue; 								//Increment score by ScoreValue
				Destroy(gameObject); 													//Destroy The Object (Enemy Ship)
			}
		}
		
	}
}
