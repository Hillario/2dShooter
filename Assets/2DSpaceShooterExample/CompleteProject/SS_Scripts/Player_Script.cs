using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]

//container class
public class Boundary 
{
	public float xMin, xMax, yMin, yMax; //Screen Boundary dimentions
}

public class Player_Script : MonoBehaviour 
{
	//Public Var
	public float speed; 			//Player Ship Speed --> Control parameter for your levels
	public Boundary boundary; 		//make an Object from Class Boundary
	public GameObject shot;         //Fire Prefab //--> Weapon1
	public GameObject shot2;         //Fire Prefab //--> Weapon2
	public GameObject shot3;         //Fire Prefab //--> Weapon3
	public Transform shotSpawn;
	public Transform shotSpawn2; //Where the Fire Spawn
	public Transform shotSpawn3; //Where the Fire Spawn

	public float fireRate = 0.5F;	//Fire Rate between Shots //amount of shots per second
	public GameObject Explosion;    //Explosion Prefab




	//Private Var
	private float nextFire = 0.0F;  //First fire & Next fire Time  //settings for the fire rate
									//score from score system
	private float myScore;

   


    // Update is called once per frame
    void Update () 
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;                                //Increment nextFire time with the current system time + fireRate
			myScore = SharedValues_Script.score; //local variable accessing the score system script

			if (myScore >= 0 && myScore <= 50)
			{

				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				GetComponent<AudioSource>().Play();                                                     //Play Fire sound
			}

			else if (myScore >= 51 && myScore <= 100) {

				Instantiate(shot2, shotSpawn.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				Instantiate(shot2, shotSpawn2.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				GetComponent<AudioSource>().Play();                                                     //Play Fire sound

			}
			else if (myScore >= 101)
            {
				Instantiate(shot3, shotSpawn.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				Instantiate(shot3, shotSpawn2.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				Instantiate(shot3, shotSpawn3.position, shotSpawn.rotation);  //Instantiate fire shot 3D-->Quaternion.Identity(Gimble Lock)-->(x,y,z,w)
				GetComponent<AudioSource>().Play();                                                     //Play Fire sound
			}
		}
			
	}

	// FixedUpdate is called one per specific time
	void FixedUpdate ()
	{
        
		if (SystemInfo.deviceType == DeviceType.Desktop) //pc--> Win,Linux,Mac
		{
			float moveHorizontal = Input.GetAxis("Horizontal");                 //Get if Any Horizontal Keys pressed
			float moveVertical = Input.GetAxis("Vertical");                 //Get if Any Vertical Keys pressed
			//Add Velocity to the player ship rigidbody
			Vector2 movement = new Vector2(moveHorizontal, moveVertical);       //Put them in a Vector2 Variable (x,y)
			GetComponent<Rigidbody2D>().velocity = movement * speed;
		}
        else
        {
			//Add Velocity to the player ship rigidbody
			Vector2 movement = new Vector2(Input.acceleration.x, Input.acceleration.y);       //Put them in a Vector2 Variable (x,y)
			GetComponent<Rigidbody2D>().velocity = movement * speed;
		}

		//Lock the position in the screen by putting a boundaries
		GetComponent<Rigidbody2D>().position = new Vector2 
			(
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),  //X
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)	 //Y
			);
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other) //other is an object of the Collider2d Class
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "Enemy" || other.tag == "Asteroid" || other.tag == "EnemyShot") 
		{
			//player health
			//decrement --10

			Instantiate (Explosion, transform.position , transform.rotation); 				//Instantiate Explosion
			SharedValues_Script.gameover = true;                                            //Trigger That its a GameOver
			Destroy(gameObject);                                                            //Destroy Player Ship Object

			                                                                         //sytem to prompt that player is eliminated
			
			
		}
	}

	
}
