
using UnityEngine;
using System.Collections;

public class EnemyShot_Script : MonoBehaviour 
{
	//Public Var
	public float speed; //EnemyRed Shot Speed

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
	}
}
