using UnityEngine;
using System.Collections;

public class EnemyAndStatusManagerScript : MonoBehaviour {
	
	//The biggest class in this game,
		//It displays specific messages and 
		//calls enemy wave formations.

	public bool isGameInTransition;
	
	//The Enemy and Game Status is displayed with this textMesh. 
	public TextMesh GameStatusTxt;
	
	
	//Variables for the Enemy Wave Management.
	int currentWave;
    	public int remainingFlockEnemiesInWave;
		public int remainingEnemiesInWave;	
		bool isWaveCompleted;
	
	
	//Constant values for speed and such.
		int _highWave=33;
		int _lowWave=34;
		public const float avg_speed=0.04f;
	
	
	//Game Status Messages.
		string[] statustxt= new string[] {"Not bad.", 
										"That's good.. \nbut can you handle this?",
										"Next wave starting!",
										"Defense wins!",
										"Keep the ball moving!",
										"BOOM \nSha-ka-la-ka!",
										"\"The game is my wife.\"\n-Michael Jordan"
											};
	
		string[] failtxt= new string[] {"\"Criticize on defense\nand encourage on offense.\"", 
									  "\"There are no\nfree throws in life!\"",
									  "Just play.",
									  "\"If you can't pass,\nyou can't play...\"" };
	
	
	//This class also keeps track of all the enemies in the game,
		//as well as which ones are active onscreen.

	//Basically, the game pre-instantiates a list of enemies that are on call,
		//ready to be deployed as a flock or wave formation.	
		public GameObject [] enemyList;
		public int currentEnemyInUse;
		
		public GameObject [] flockEnemyList;
		public int currentFlockEnemyInUse;
	

	//Running awake(), before any other object starts up.
	void Awake () {
		
				
		//1.Initializing and populating our enemy arrays.		
		enemyList= new GameObject[Constants.numberOfEnemiesInArray];		
				
		for(int i=0; i< Constants.numberOfEnemiesInArray; i++)
			{
				enemyList[i]= Instantiate(Resources.Load ("EnemyPrefab")) as GameObject;		
			}
		
			//same for flock enemies..
	
		flockEnemyList= new GameObject[Constants.numberOfFlockEnemiesInArray];		
				
		for(int i=0; i< Constants.numberOfFlockEnemiesInArray; i++)
			{
				
			flockEnemyList[i]= Instantiate(Resources.Load ("FlockEnemyPrefab")) as GameObject;
	
			}
				
	}
	
//----->This function starts the game's enemy waves.
	public void StartGame_Waves()
	{
		
		isGameInTransition=false;		
		
		//Setting starting values.
		currentEnemyInUse=0;
		currentFlockEnemyInUse=0;
		currentWave=1;
		
		//Setting variables managing wave transititons.
		remainingEnemiesInWave=0;
		remainingFlockEnemiesInWave=0;
		isWaveCompleted=false;
		
		//Showing a starting message.
		GameStatusTxt.text=Constants.firstMsgtxt;
		GameStatusTxt.renderer.enabled=true;
		
		StartCoroutine("callWave");
		
		
	}
	

	
	
//------Preparing Waves
	
	
	//I've turned this method into a Coroutine to add pauses.
	IEnumerator callWave()
	{
				
		//0.Resetting our wave variables.
			//A brief transition text appears in between waves.
			//This is what the transition boolean is referring to.
		remainingEnemiesInWave=0;
		remainingFlockEnemiesInWave=0;
		isWaveCompleted=false;
		isGameInTransition=false;
				
		//Prepare different enemy waves depending on the value of currentWave.
		switch(currentWave)
		{
			
		case 1:
			
			//Wave 1 consists of 3 enemies, each appearing one at a time.
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						2, avg_speed);
			
			
			yield return new WaitForSeconds(3f);

			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			
			yield return new WaitForSeconds(3f);

			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			isWaveCompleted=true;
			
			break;
			
		case 2:
			
			//Calling enemies in singles and in pairs.			
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			
			yield return new WaitForSeconds(3f);
			
			
			callDuo(random_HighLow(), 
					Constants.Straight_Movement_Type ,
					Constants.fromTheRight, 
					avg_speed*0.8f);
			
			
			
			yield return new WaitForSeconds(3f);
			
			
			callDuo(random_HighLow(), 
					Constants.Straight_Movement_Type ,
					Constants.fromTheRight, 
					avg_speed*0.8f);
			
			
			yield return new WaitForSeconds(3f);
			
	
			//Two enemies in quick succession.
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			yield return new WaitForSeconds(1.5f);

			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			
			isWaveCompleted=true;

			
			break;
			
		case 3:
			
			//calling enemies in singles, plus one quick single and a trio.			
			//enemies start moving in sine wave pattern.

			callSingle(Constants.Sine_Movement_Type,
						Constants.fromTheRight,
						3, avg_speed);
			
			yield return new WaitForSeconds(2f);

			callSingle(Constants.Sine_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed);
			
			yield return new WaitForSeconds(3f);
			
			
			//Fast enemy.
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						2, avg_speed*2);
			
			yield return new WaitForSeconds(2f);
			
			callTrio(random_HighLow(), 
					Constants.Straight_Movement_Type,
					Constants.fromTheRight,
					avg_speed);
			
			
			isWaveCompleted=true;

			
			break;
			
			
		case 4:
			
			//singles and pairs of enemies are now faster.
			//calling enemies with sine movements and from random points+directions.
			
			callSingle(Constants.Straight_Movement_Type,
						random_direction(),
						random_point(), avg_speed);
			
			
			yield return new WaitForSeconds(2f);

			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheTop,
						3, avg_speed);
						
			yield return new WaitForSeconds(3f);
			
			
			callDuo(random_HighLow(), 
					Constants.Sine_Movement_Type,
					Constants.fromTheRight,
					avg_speed);
		
			yield return new WaitForSeconds(2f);
			
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheBottom,
						random_point(), avg_speed*1.5f);
			
			
			yield return new WaitForSeconds(2f);
			
			callDuo(random_HighLow(), 
					Constants.Sine_Movement_Type,
					Constants.fromTheRight,
					avg_speed);
			
			isWaveCompleted=true;
			
			break;
	
	case 5:
			
			//In this wave, a flock is called for the first time.
			
			callFlock(Constants.fromTheRight, 2, avg_speed);
			
			
			yield return new WaitForSeconds(3f);

			 
			callSingle(Constants.Sine_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed*1.3f);
			
			yield return new WaitForSeconds(2f);

			callTrio(random_HighLow(), 
					Constants.Straight_Movement_Type, 
					random_direction(), 
					avg_speed*1.3f);
			
			yield return new WaitForSeconds(2f);

			callDuo(random_HighLow(), 
					Constants.Sine_Movement_Type, 
					Constants.fromTheLeft, 
					avg_speed*1.3f);
			
			
			yield return new WaitForSeconds(3f);
			
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed*2f);
			
			yield return new WaitForSeconds(1f);
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheRight,
						random_point(), avg_speed*2f);
			
			yield return new WaitForSeconds(2f);
			
			callDuo(random_HighLow(), 
					Constants.Sine_Movement_Type,
					Constants.fromTheTop,
					avg_speed*1.3f);
			
			
			isWaveCompleted=true;		
			
			break;	
			
			
		case 6:
			
			//Calling enemies from random locations.
				//Quick enemies with sine wave movement, and several flocks.
			
			callDuo(random_HighLow(), 
					Constants.Straight_Movement_Type, 
					Constants.fromTheBottom, 
					avg_speed*2f);			
			
			yield return new WaitForSeconds(2f);
			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheTop,
						2, avg_speed*2.3f);
			
			yield return new WaitForSeconds(1f);
			
			
			callSingle(Constants.Sine_Movement_Type,
						Constants.fromTheLeft,
						2, avg_speed*1.5f);
			
			callSingle(Constants.Sine_Movement_Type,
						Constants.fromTheRight,
						2, avg_speed*1.5f);
			
			
			//
			 
			yield return new WaitForSeconds(3f);
			
			//
			
			callTrio(random_HighLow(), 
					Constants.Straight_Movement_Type, 
					random_direction(), 
					avg_speed*1.3f);
			
			yield return new WaitForSeconds(2f);

			callDuo(random_HighLow(), 
					Constants.Sine_Movement_Type, 
					Constants.fromTheLeft, 
					avg_speed*1.3f);
			
			//
			
			yield return new WaitForSeconds(3f);
			
			//
			
			callFlock(Constants.fromTheLeft, 2, avg_speed*1.3f);

			yield return new WaitForSeconds(2f);
			

			callFlock(Constants.fromTheBottom, 2, avg_speed*1.3f);
			
			
			yield return new WaitForSeconds(2f);

			
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheTop,
						3, avg_speed*3f);
			
			
			isWaveCompleted=true;		
			
			break;	
			
		
		case 7:
			
			
			//The last level.			
				//This one is does not have as much randomness as the previous one.

				//Making a 'pyramid' of enemies:
					//Calling single, pairs and trios of enemies in succession.
			
								
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheTop,
						3, avg_speed*1.4f);
			
			yield return new WaitForSeconds(1.5f);
			
			callDuo(_highWave, 
					Constants.Straight_Movement_Type, 
					Constants.fromTheTop, 
					avg_speed*1.4f);	
			
			yield return new WaitForSeconds(1.5f);
			
			
			callTrio(_highWave, 
					Constants.Straight_Movement_Type, 
					Constants.fromTheTop, 
					avg_speed*1.4f);
			
			 
			yield return new WaitForSeconds(2f);
			
			//Calling another pyramid.
								
			callSingle(Constants.Straight_Movement_Type,
						Constants.fromTheBottom,
						2, avg_speed*1.4f);
			
			yield return new WaitForSeconds(1.5f);
			
			callDuo(_lowWave, 
					Constants.Straight_Movement_Type, 
					Constants.fromTheBottom, 
					avg_speed*1.4f);	
			
			yield return new WaitForSeconds(1.5f);
			
			
			callTrio(_lowWave, 
					Constants.Straight_Movement_Type, 
					Constants.fromTheBottom, 
					avg_speed*1.4f);
			
						
			yield return new WaitForSeconds(2f);
			
			
				callDuo(_lowWave, 
					Constants.Sine_Movement_Type, 
					Constants.fromTheLeft, 
					avg_speed);
			
			
			yield return new WaitForSeconds(3f);
			
			
			callSingle(Constants.Straight_Movement_Type,
						random_direction(),
						random_point(), avg_speed);
						
			isWaveCompleted=true;		
			
			break;	
			
			
		default:
			print ("no wave to call in EnemyManager, callWave()");
			break;
		}
		
					
	}
	
	
//The following methods are used to assign movement and formation patterns to enemies.	
	//They all rely on enemy_SetTypeDirectionPointSpeed().

	void callSingle(int MovementType, int whichDirection, int whichPoint, float speed)
	{
		
		
			enemy_SetTypeDirectionPointSpeed(MovementType,
											whichDirection,
											whichPoint, speed);
			
			//Increasing our logic counter.
			remainingEnemiesInWave++;
		
	}
	

	void callDuo(int highOrLow, int SineOrStraight ,int whichDirection, float speed)
	{
		
		if(highOrLow==_highWave)
			{	enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											1, speed);
					
			
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											3, speed);
	
			}
			
		else
			{	enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											2, speed);
					
			
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											4, speed);
	
			}
		
		
		//Increasing our logic counter.
		remainingEnemiesInWave+= 2;
		
	}
	
	void callTrio(int highOrLow, int SineOrStraight ,int whichDirection, float speed)
	{
	
		if(highOrLow==_highWave)
			{	enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											1, speed);
					
			
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											2, speed);
	
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											3, speed);
			
			}
			
		else
			{	enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											2, speed);
					
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											3, speed);
			
			
				enemy_SetTypeDirectionPointSpeed(SineOrStraight,
											whichDirection,
											4, speed);
	
			}
		
		//Increasing our logic counter.
		remainingEnemiesInWave+= 3;
		
	}
	
	void callFlock(int whichDirection, int whichPoint, float speed)
	{
		//assuming the time, shorthand for distance covered, is 8f.
		
		float[] flockTimes= new float[3];
		
		flockTimes[1]= 0.88f;
		flockTimes[2]= 1f; 					//The third value doesn't matter, 
											//the flock keeps advancing.
		
		//call the flock group, with the appropriate time offset.
		for(int i=0; i< Constants.numberPerFlock; i++)
		{
		
			//my time formula for now...
			flockTimes[0]= 6*(0.44f + i* 0.08f);
			
			//call the flockEnemy
			flockEnemyList[currentFlockEnemyInUse].GetComponent<FlockEnemyScript>().SetTimeDirectionPointOffsetSpeed(
				flockTimes, whichDirection,whichPoint, Constants.flockEnemyGap*i, speed);
			
			remainingFlockEnemiesInWave++;
			increaseFlockEnemyIndex();
			
		}	
		
		
		
	}
	
	
//Functions that advance the game's enemy manager logic.
	
	//Goes to the next level, or to the end of game message.
	void goToNextWave()
	{
		
		currentWave++;
		
		if(currentWave <= Constants.numberOfWaves)
				StartCoroutine(WavePassedRoutine());		//The game continues.
		else 		
				StartCoroutine(GameWonRoutine());
		
	}	
	

	//repeats the level, if the player has been killed.	
	public void retryWave()
	{
		
		//The counter is not increased.
		StartCoroutine(RetryWaveRoutine());
			
	}
	
	public void toGameOver()
	{
		
		
		StartCoroutine(GameOverRoutine());
		
				
	}
	
	
	//The following are coroutines that transition to new Game States...
	IEnumerator GameOverRoutine()
	{
		if(!isGameInTransition)
		{
			
			isGameInTransition=true;	
			
		//Show the win text.
		GameStatusTxt.renderer.enabled=true;
		GameStatusTxt.text=Constants.gameOvertxt;	
		
		//Delay...
		yield return new WaitForSeconds(1.2f);

		
		//Remove the win txt.
		GameStatusTxt.renderer.enabled=false;
		
		//Revert Wave counter.
		currentWave=1;
		
		//Stopping the wave calls.
		StopCoroutine("callWave");
			
		//resetting enemies
		resetAllEnemies();
			
		//Go back to Main Menu.
		MainScript.myMainScript.ShowMenu();
		
		}
	}
	
	
	IEnumerator GameWonRoutine()
	{
		if(!isGameInTransition)
	   {
			
		isGameInTransition=true;
		
		yield return new WaitForSeconds(0.5f);
	
			
		//Show the win text.
		GameStatusTxt.renderer.enabled=true;
		GameStatusTxt.text=Constants.gameWintxt;	

		//play the sound.
		audio.Play();
		
		//Delay...
		yield return new WaitForSeconds(3f);

		
		//Remove the win txt.
		GameStatusTxt.renderer.enabled=false;
		
		//Revert Wave counter.
		currentWave=1;
		
		
		//Go back to Main Menu.
		MainScript.myMainScript.ShowMenu();
		
		}	
			
	}
	
	IEnumerator WavePassedRoutine()
	{
		if(!isGameInTransition)
		{
			
		isGameInTransition=true;	
			
		//Show the player v wave success text.
		GameStatusTxt.renderer.enabled=true;
		GameStatusTxt.text=statustxt[random_statustxt()];	
		
		//Delay...
		yield return new WaitForSeconds(1.5f);
	
		//Remove the status txt.
		GameStatusTxt.renderer.enabled=false;		
			
		//Go to next wave...	
		StartCoroutine("callWave");
		
		
		}
	}
	
	IEnumerator RetryWaveRoutine()
	{
		if(!isGameInTransition)
		{
			
			isGameInTransition=true;		
			
			
		//Show the win text.
		GameStatusTxt.renderer.enabled=true;
		GameStatusTxt.text=failtxt[random_failtxt()];	
		
			
		//Stopping the wave calls.
		StopCoroutine("callWave");
		
			
		//resetting enemies
		resetAllEnemies();
			
			
	
			
		//Delay...
		yield return new WaitForSeconds(1.2f);

		//Remove the win txt.
		GameStatusTxt.renderer.enabled=false;
			
			
		//Retry wave...	
		StartCoroutine("callWave");
		
		}	
	}	
	
	
//Functions that check whether the current Wave is finished.
	//These two functions are called when an enemy is destroyed, 
	//or if it touches the screen's boundary areas.
	public void enemyReset()
	{
		
		remainingEnemiesInWave--;
		
		//Checking for Wave ended, in which case we go to next wave.
		if(isWaveCompleted)
			if((remainingEnemiesInWave<=0)&&(remainingFlockEnemiesInWave<=0))
				goToNextWave();
	
	}
	
	public void flockEnemyReset()
	{
			
		remainingFlockEnemiesInWave--;
		
		//Checking for Wave ended, in which case we go to next wave.
		if(isWaveCompleted)
			if((remainingEnemiesInWave<=0)&&(remainingFlockEnemiesInWave<=0))
				goToNextWave();	
	
	}
	
	
	public void resetAllEnemies()
	{
	
		//Enemies.
		for(int i=0; i< Constants.numberOfEnemiesInArray; i++)
		{
		
			
			//resetting
			enemyList[i].GetComponent<EnemyScript>().MoveState=Constants.No_Move;
			enemyList[i].GetComponent<EnemyScript>().returnToPosition();

			
		}
		
		
		//FLock Enemies.
		for(int i=0; i< Constants.numberOfFlockEnemiesInArray; i++)
		{
		
			
			//resetting
			flockEnemyList[i].GetComponent<FlockEnemyScript>().currentMoveState=Constants.No_Move;
			flockEnemyList[i].GetComponent<FlockEnemyScript>().returnToPosition();

			
		}
		
		
	}	
//------>Helper methods used to set up enemy movement+position.	
	int random_direction()
	{
		
		return Random.Range(41,45);
		
	}
	
	int random_point()
	{
		
		return Random.Range (1,5);
		
	}
	
	int random_statustxt()
	{		

		return Random.Range (0,7);
	}	
	
	int random_failtxt()
	{
		
		return Random.Range (0,4);
		
	}	
	
	int random_HighLow()
	{		

		return Random.Range (33,35);
	}
	
	
	void enemy_SetTypeDirectionPointSpeed(int whichMoveType, 
										   int whichDirection, 
										   int whichPoint, 
										   float whichSpeed)
	{
			
		enemyList[currentEnemyInUse].GetComponent<EnemyScript>().SetTypeDirectionPointSpeed( whichMoveType, whichDirection, whichPoint, whichSpeed);
		
		//increasing the counter for currentEnemyInUse.
		increaseEnemyIndex();		
	
	}
	
	
	void increaseEnemyIndex()
	{
		currentEnemyInUse++;
		
			if(currentEnemyInUse>= Constants.numberOfEnemiesInArray)
				currentEnemyInUse=0;
		
	}
	
	void increaseFlockEnemyIndex()
	{
		currentFlockEnemyInUse++;
		
			if(currentFlockEnemyInUse>= Constants.numberOfFlockEnemiesInArray)
				currentFlockEnemyInUse=0;
		
	}
	

	
}
