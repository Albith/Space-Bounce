using UnityEngine;
using System.Collections;

public class FlockEnemyScript : MonoBehaviour {
	
	//This is a movement class applied 
		//to each enemy in a flock formation.
	
	//The Flock flies in a Square pattern,
		//Going forward, downward(or upward), and then backwards.
	
	//To spice things up, different spawn directions now
		//go upward or downward differently.
		
	//Variables for setting up movement function.
	public float timeCounter;
		public float [] howMuchTimeCounts;
	
	public int currentMoveState;
		public int currentMoveDirectionIndex;
	
	public Vector3 basePosition;
	
	//variables for handling being touched with the ball or ship.
	public bool isTouched;
	
	//movement speeds and directions for the FixedUpdate()
		public float speed;
		public Vector3 [] speedVectors;
		
		public int GoingUp=52;
		public int GoingDown=53;
		public int GoingLeft=54;
		public int GoingRight=55;
		
		public int MoveState;
	
	
	//<EnemyScript positioning constants>
		//These positioning variables are used to specify 
		//different spawn points on the screen.
			public const int _offset=2; 		

			public const float _x0= -8.422f;
			public const float _y0= -1.399f;
			
			public const float _x5= 1.578f;
			public const float _y5= 6.102f;
			
			public const float _xGap=2f;
			public const float _yGap=1.5f;
			
			public const float _zValue= -11.27f;
		
			//The enemy is placed in a position offscreen before they are launched 
				//as a wave.
			public readonly Vector3 waitPosition= new Vector3(0, 20, _zValue);
	
	//</end of EnemyScript positioning constants>/


	//Setting the Flock Enemy class's initial values.		
	void Awake() 
	{
		
		speed= 0f;
		basePosition= Vector3.zero;
		speedVectors= new Vector3[3];
		
		timeCounter=0f;
		howMuchTimeCounts= new float[3];
		isTouched=false;
			
		currentMoveState=Constants.No_Move;  //This will be changed by the mainScript.
		currentMoveDirectionIndex=0;
		
		this.rigidbody.transform.position=(waitPosition);
					
	}
	
	
	
	public void SetTimeDirectionPointOffsetSpeed(float[] howMuchTime, 
										   int whichDirection, 
										   int whichPoint,
   										   float flockOffset,
										   float whichSpeed)
	{
				
		//some housekeeping...
		//When moving objects with rigidbodies, first put them to sleep.
			this.rigidbody.Sleep();
					
			isTouched=false;
			collider.enabled=true;
			timeCounter=0f;
			currentMoveDirectionIndex=0;
		
		//0.Setting an enemytype:  for now we're using a single enemyType for all flocks.
		renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.flockBomberName);
		
		//1.Speed: setting speed.
		speed= whichSpeed;
		
		//2.Setting the Flock's Direction and from this, the enemy's movement variables.
		switch(whichDirection)
		{		
			case Constants.fromTheRight:
				
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
				//setting position, each enemy is offset slightly by 
					//the whichPoint value.	
				this.rigidbody.transform.position=(
											new Vector3(_x5 + _offset + flockOffset, 
											_y0 + _yGap*whichPoint,
											_zValue));
			
				//preparing to enter the Fixed Update.			
				currentMoveState= Constants.Horiz_Move;			
				
					howMuchTimeCounts[0]=howMuchTime[0];
					howMuchTimeCounts[1]=howMuchTime[1];
					howMuchTimeCounts[2]=howMuchTime[2];				
			
				//preparing the speed vector for the FixedUpdate.
					//this vector will set how the enemy moves.				
					speedVectors[0]= new Vector3(-speed, 0, 0);
					speedVectors[1]= new Vector3(0, -speed, 0);
					speedVectors[2]= new Vector3(speed, 0, 0);

			
				break;
		

			case Constants.fromTheLeft:			
				//setting the image rotation.
				setRotation(whichDirection);
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position=new Vector3(_x0 - _offset - flockOffset, 
											_y0 + _yGap*whichPoint,
											_zValue);
			
				//preparing to enter the Fixed Update.				
				currentMoveState= Constants.Horiz_Move;	
							
					howMuchTimeCounts[0]=howMuchTime[0];
					howMuchTimeCounts[1]=howMuchTime[1];
					howMuchTimeCounts[2]=howMuchTime[2];

					
			
				//preparing the vector for the FixedUpdate.
				
					speedVectors[0]= new Vector3(speed, 0, 0);
					speedVectors[1]= new Vector3(0, speed, 0);
					speedVectors[2]= new Vector3(-speed, 0, 0);
				
		

			
				break;	
			
			case Constants.fromTheTop:
			
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position= 
									new Vector3(_x0 + _xGap*whichPoint, 
											_y5 + _offset + flockOffset,
											_zValue);
			

			
				//preparing to enter the Fixed Update.	
				
				currentMoveState= Constants.Horiz_Move;	
						
					howMuchTimeCounts[0]=howMuchTime[0];
					howMuchTimeCounts[1]=howMuchTime[1];
					howMuchTimeCounts[2]=howMuchTime[2];
		
				//preparing the vector for the FixedUpdate.
				
					speedVectors[0]= new Vector3(0, -speed, 0);
					speedVectors[1]= new Vector3(speed, 0, 0);
					speedVectors[2]= new Vector3(0, speed, 0);
						
			
				break;


			case Constants.fromTheBottom:
			
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position=
									new Vector3(_x0 + _xGap*whichPoint, 
											_y0 - _offset - flockOffset,
											_zValue);
			
				//calling the Invoke Function.	
				MoveState=Constants.Vert_Move;				
				
			
				//preparing to enter the Fixed Update.	
				currentMoveState= Constants.Horiz_Move;	
				
					howMuchTimeCounts[0]=howMuchTime[0];
					howMuchTimeCounts[1]=howMuchTime[1];
					howMuchTimeCounts[2]=howMuchTime[2];

							
				//preparing the vector for the FixedUpdate.
				
					speedVectors[0]= new Vector3(0, speed, 0);
					speedVectors[1]= new Vector3(-speed, 0, 0);
					speedVectors[2]= new Vector3(0, -speed, 0);
				
			
				break;
			
			
			default:
				print ("EnemyScript.cs, SetTypeDirectionSpeed(): no direction argument given.");	
				break;
			
		}  //end of Switch statement.
		
		//once the enemy's rigidbody position, plus movement variables, are all set,
			//the enemy's rigidbody is woken up, ready for movement.
		this.rigidbody.WakeUp();
		
	}	
	
	
	//This update will move the enemy according to speed vectors specified.
	void FixedUpdate()
	{
		
		//if the enemy's state is set to move, apply the movement speed vector.
		if(currentMoveState!= Constants.No_Move)	
		{
			timeCounter += Time.deltaTime;
			
			if(timeCounter>=  howMuchTimeCounts[currentMoveDirectionIndex])
			{
			
				timeCounter=0f;
				
				if(currentMoveDirectionIndex<2)
				{
					currentMoveDirectionIndex++;
				}
				
			}	
				
				 //This is for a constant value.
				rigidbody.MovePosition(
				rigidbody.position + speedVectors[currentMoveDirectionIndex]);	
				
		}
			
	
	}
	
	
	//Returning the enemy rigidbody to a default position.
	public void returnToPosition()
	{
				
		timeCounter=0;	
		this.rigidbody.detectCollisions=true;
		this.rigidbody.MovePosition(waitPosition);		
		
	}
	

	//This function sets up the enemy sprite before starting movement.
		//It rotates the enemy sprite based on what direction it's coming from.	
	void setRotation(int whichDirection)
	{
		
		switch(whichDirection)
		{
			
			case Constants.fromTheLeft:
			
				//Flipping the object.
				transform.localScale= new Vector3(0.08f,
												transform.localScale.y,
												transform.localScale.z);
			
			break;		
			
				
			case Constants.fromTheTop:
		
				//Rotating the Object.
				transform.rotation= Quaternion.Euler(0,-90,90);
			break;
			
			
		case Constants.fromTheBottom:
			
				//Rotating the Object.
				transform.rotation= Quaternion.Euler(180,-90,90);
			
			break;
			
		default:
			
			//The default case has the enemy coming from the right of the screen.
				//For this case, we unflip the object.
				transform.localScale= new Vector3(-0.08f,
												transform.localScale.y,
												transform.localScale.z);
			
				//Rotating to Default.
				transform.rotation= Quaternion.Euler(-90,0,0);
			
			break;
			
		}	
		
	}
		
	//This event only checks for collision with the screen boundaries. 
		//If there is a collision, the enemy object is reset.	
	void OnTriggerEnter(Collider otherCollider)
	{
		
		if(otherCollider.tag=="limit")
			{
				
				//print ("Flock enemy hits limit wall.");	
				currentMoveState=Constants.No_Move;
				returnToPosition();
				MainScript.myMainScript.flockEnemyReset();

			
			}	
	
	}
	
	
	//This death sequence coroutine is called when the enemy is hit.
		IEnumerator deathSequence()	
		{
					
			//Stopping the enemy's rigidbody.
			currentMoveState=Constants.No_Move;		
			this.rigidbody.detectCollisions=false;
			this.rigidbody.Sleep ();
			
			
			//The enemy texture changes to an explosion texture.			
			renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.bomberDeath);	
			audio.Play ();
		
			//Then wait a little bit.
			yield return new WaitForSeconds(0.3f);

			returnToPosition();
			MainScript.myMainScript.flockEnemyReset();
			
		}
	
		//Handling the logic, if the enemy is hit by the player's ball.
		public void monsterHitByBall()
		{
			
			if(!isTouched)
				{
					
					//if the ball hits the enemy, 
						//the enemy is killed.
						
						isTouched=true;
						
						//the enemy's collider is turned off.
						collider.enabled=false;
					
						StartCoroutine(deathSequence());	
					
				}  
				
		}  
	
}
