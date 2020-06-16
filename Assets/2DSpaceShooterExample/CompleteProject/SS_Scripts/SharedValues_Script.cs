using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SharedValues_Script : MonoBehaviour 
{
	//Public Var
	public Text scoreText; 				//GUI Score
	public Text GameOverText; 			//GUI GameOver
	public Text FinalScoreText; 			//GUI Final Score
	public Text ReplayText; 				//GUI Replay

	//Public Shared Var
	public static int score = 0; 			//Total in-game Score
	public static bool gameover = false;    //GameOver Trigger


	//temp variables
	public float normalScore;
	public float uniqueScore;
	public float ultimateScore;
	public float finalScore;

	// Use this for initialization
	void Start () 
	{

		finalScore = PlayerPrefs.GetFloat("ultimateScore");

		gameover = false; 					//return the Gameover trigger to its initial state when the game restart
		score = 0; 							//return the Score to its initial state when the game restart
	}

	// Fixed Update is called one per specific time
	void FixedUpdate ()
	{
		scoreText.text= "Score: " + score; 			//Update the GUI Score

		//Excute when the GameOver Trigger is True
		if (gameover == true)
		{
			//writing to device the current score
			PlayerPrefs.SetFloat("normalScore", score);

			if (score > finalScore)
			{
				uniqueScore = score;
			}
            else
            {
				uniqueScore = finalScore;
            }

			//write the ultimate for access in the mainMenu
			PlayerPrefs.SetFloat("ultimateScore", uniqueScore);

			StartCoroutine("LoadMainMenu");             //coroutine initiated
			GameOverText.text = "GAME OVER"; 			//Show GUI GameOver
			FinalScoreText.text = "" + score; 			//Show GUI FinalScore //PlayerPrefs-->HighScores
			ReplayText.text = "";                       //Show GUI Replay
			
		}
	}

	IEnumerator LoadMainMenu()
    {
		yield return new WaitForSeconds(3f);    //wait for 5 seconds--> float datatype         
		SceneManager.LoadScene(0);              //load int scene
    }



}
