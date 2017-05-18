using UnityEngine;
using System.Collections;

public class shipScript : MonoBehaviour {
	
	//This class is attached to the ship sprite that the player controls.
	
	//Global variable for this class.
	public static shipScript myShip;
	
	//The ball object is referenced via this variable.	
	public GameObject myBall;
	
	
	//Ship-specific logic variables.
	public float shipSpeed;
	public float playerVelocity;

	public bool isDead;
	public bool canMove;
	
	//Position variables used when starting the game.
	Vector3 shipSpawnPoint;
	Vector3 ballSpawnPoint;
	
	//One more thing to keep track of...
	public int playerLives;
	
	void Start () {		
		
		myShip=this;
		
		shipSpeed= 0.6f;
		playerVelocity=7.5f;
						
		//Setting the spawnPoints.
		shipSpawnPoint= transform.position;
		ballSpawnPoint= myBall.transform.position;	
		
		//setting other variables.
		isDead=false;
		canMove=true;
		playerLives=Constants.numberOfLives;
		
	}
	
	//Function called when the ship and ball position is reset.
	void resetShipAndBall()
	{
		
		isDead=false;
		canMove=true;
		
		//1.Reset the Ball.
			//Originally I was disconnecting and reconnecting a joint between the ship and ball.
			//I'm doing something different here.

		//Before doing anything, the ball's velocity is set to zero.
			myBall.rigidbody.velocity= Vector3.zero;
			//Destroy the existing joint between ball and ship.	
			Destroy(myBall.GetComponent<FixedJoint>());
			
		//Putting the ball rigidbody to sleep before moving it to a specific position.
		//Note: this is how rigidbodies should be moved.
			myBall.rigidbody.Sleep();	
			myBall.rigidbody.MovePosition(ballSpawnPoint);
			myBall.rigidbody.WakeUp();

		//2.Reset the ship's position.
			this.rigidbody.velocity= Vector3.zero;
			this.rigidbody.Sleep();
			this.rigidbody.transform.position= shipSpawnPoint;
		
			//The player's rigidbody is reset to detect collisions.
			this.rigidbody.detectCollisions=true;
		
			//Reset the ball's state variable.
			myBall.GetComponent<ballScript>().ballState= Constants.BallOnFront;		
	}
	

	//Running a fixedupdate, that runs concurrently with the physics engine's cycle.
	void FixedUpdate () {

		//If the player's ship is alive (hasn't been hit):
		if(canMove)
		{	
			
			//Hotkey to press to reset the ball, if it gets stuck in a corner.
			if (Input.GetKeyDown("r")) {
				
				resetShipAndBall();
			
			}
			

			//Pressing the directional keys will move the ship.
				//To do this, different forces are applied to the ship's rigidbody. 
			
		else{
			
				if (Input.GetKey ("left")) 
				{
      								
					rigidbody.AddRelativeForce (Vector3.right*-1*shipSpeed  , ForceMode.VelocityChange);
					rigidbody.velocity = new Vector3(-playerVelocity, rigidbody.velocity.y, 0);
												
				}
			
				if (Input.GetKey ("right")) 
				{
				
					rigidbody.AddRelativeForce (Vector3.right*shipSpeed, ForceMode.VelocityChange);				
					rigidbody.velocity = new Vector3(playerVelocity, rigidbody.velocity.y, 0);
   		
				}
			
			  			 
				if (Input.GetKey ("up"))
				{
									
					rigidbody.AddRelativeForce (Vector3.up*shipSpeed, ForceMode.VelocityChange);	
					rigidbody.velocity = new Vector3(rigidbody.velocity.x, playerVelocity, 0);
	
				}

				if (Input.GetKey ("down"))
				{
					
					rigidbody.AddRelativeForce (Vector3.up*-1*shipSpeed, ForceMode.VelocityChange);	
					rigidbody.velocity = new Vector3(rigidbody.velocity.x, -playerVelocity, 0);

				}			
		
			} //end of the else statement.
			
			

			
		}	//end of the if(canMove) statement.

	
	
	}  //end of the FixedUpdate();
	
	
	//If an enemy object touches the ship rigidbody, the player loses a life.
	void OnTriggerEnter(Collider otherCollider)
		{	
		
			//Checking the invading rigidbody's tag. 				
			if((otherCollider.tag=="monster") || (otherCollider.tag=="monster_f"))
				if(!isDead)
				{
			
					//if the ship hits the enemy, 
						//the ship is killed.
						isDead=true;
						canMove=false;
						this.rigidbody.detectCollisions=false;
				
						StartCoroutine(deathSequence());
	
				}  
		
		}
	
	
	//Coroutine sequence that is played when the ship blows up.
	IEnumerator deathSequence()	
	{
		
		//Change the player ship's texture, and play the ship's explosion sound.		
		renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.shipDeath);		
		audio.Play ();
	
		//Checking for Game Over. If the player has lives remaining.
			//If there is no game over, then repeat the enemy wave.
			if(checkForGameOverOrRetry())
				yield return new WaitForSeconds(0.2f);
				
			//wait a little bit.
				yield return new WaitForSeconds(1f);
			
		resetShipAndBall();
			
		//Resetting the ship object's texture to default.
		renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.shipImageName);
	}
	
	
	//This function checks for a game over 
	//and calls the main script for a state change, if necessary.
	bool checkForGameOverOrRetry()
	{
		//The player has lost a life.
		playerLives--;
		
		//If the player has run out of lives, it's a game over.
		if(playerLives<=0)
		{
		
			//resetting lives counter.
			playerLives=Constants.numberOfLives;		
			MainScript.myMainScript.toGameOver();
			
			return true;
		}
		
		//the player has not yet reached a game over state.
		else{
			
			MainScript.myMainScript.retryWave();	
			
			return false;
			
			}
			
	}
	
}	//end of shipScript.
