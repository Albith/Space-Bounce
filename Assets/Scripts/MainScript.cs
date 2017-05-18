using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {
	
	//This class handles the Main Menu and game start.  
		//It also links up to other scripts in the game.

	//Since the game does not load levels, most of the complexity of the game
		//is delegated to the EnemyAndStatusManager.
		//The game 'levels' all consist of different enemy configurations.
	
	public static MainScript myMainScript;
	
	public TextMesh TitleTxt;
	
	public bool areEnemiesStarted;
	
	public EnemyAndStatusManagerScript gameLogicManager;
		
	//more variables.
	
	// Use this for initialization
	void Start () {
		
		myMainScript=this;
		
		ShowMenu();		
		
	}
	
	public void ShowMenu()
	{
		
		print("Game Starting");
		
		areEnemiesStarted= false;
		
		TitleTxt.renderer.enabled=true;
		TitleTxt.text=Constants.startGametxt;
		
		//Repeatedly checking for a player button press with an Invoke function.	
		InvokeRepeating("CheckPress",0,0.02f);
		
		
	}
	
	void CheckPress()
	{
		
		if(Input.GetKeyDown("space"))
			{
				startEnemies();
				CancelInvoke();
			
			}
		
	}
	
	
	public void startEnemies()
	{
		
		//Game Text is hidden.
		TitleTxt.renderer.enabled=false;
		
		//Play the GameStart Sound.
		audio.Play ();
		
		gameLogicManager.StartGame_Waves();
		
	}
	
	
//Callbacks for Game State Changes.
	
	
	public void retryWave(){
		
		gameLogicManager.retryWave();
		
	}
	
	
	
	
	
	public void toGameOver()	
	{
		
		gameLogicManager.toGameOver();
	}
	
	
	//Game State Changes here too..
	
	public void enemyReset()
	{
	
		gameLogicManager.enemyReset();
		
	}
	
	public void flockEnemyReset()
	{
		
		gameLogicManager.flockEnemyReset();
		
	}
	
	
}
