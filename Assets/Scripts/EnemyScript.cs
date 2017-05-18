using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	//Script used to control the single enemies that appear in waves.
		//Enemies that appear as part of a flock are controlled by the
		//FlockEnemyScript.

	//Variables used for setting up movement function.
	public int moveType;
	public int moveDirection;
	
	public Vector3 basePosition;
	
	//Sine movement variables.
	float myAmplitude=0.03f;
	float myFrequency=2.5f;
	float sineCount;
	
	//Variable for handling touch with ball or ship.
	public bool isTouched;
	
	//Movement speeds and directions for the FixedUpdate()
	public float speed;
	public Vector3 speedVector;
	public int MoveState;
	
		
	//<EnemyScript positioning constants>
		//These positioning variables are used to specify 
		//different spawn points on the screen.
		public const int _offset=2; 		//a 2 pixel offset.

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
 
	// </end of EnemyScript positioning variables>/
	
	//Setting the Flock class's initial values.		
	void Awake() {
	
		speed= 0f;
		basePosition= Vector3.zero;
		speedVector= Vector3.zero;
		sineCount=0f;
		
		
		isTouched=false;
		//Move states are assigned only when the enemy's wave is called to move.
		MoveState=Constants.No_Move;  
		
		//Setting the default position with the rigidbody.
		this.rigidbody.transform.position=(waitPosition);
		
						
	}
	
	
	//This function sets up the movement, speed and position of each enemy in a wave.
	public void SetTypeDirectionPointSpeed(int whichMoveType, 
										   int whichDirection, 
										   int whichPoint, 
										   float whichSpeed)
	{

		//Putting the rigidbody to sleep, so it can be moved.		
		this.rigidbody.Sleep();
		
		
		isTouched=false;
		collider.enabled=true;
		moveType= whichMoveType;
		
		//0.Setting an enemytype: for now we're using a single enemy sprite.
		renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.bomberName);
		
		//1.Speed: setting speed.
		speed= whichSpeed;
		
		//2.Setting the Enemy's Direction and from this, the enemy's movement variables.		
		switch(whichDirection)
		{
			
			case Constants.fromTheRight:
			
			
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
				//setting position, each enemy is offset slightly by 
					//the whichPoint value.	
				this.rigidbody.transform.position=(
											new Vector3(_x5 + _offset, 
											_y0 + _yGap*whichPoint,
											_zValue));
			
				//preparing to enter the Fixed Update.	
				MoveState=Constants.Horiz_Move;				
			
				//preparing the speed vector for the FixedUpdate.
					//this vector will set how the enemy moves.				
				if(moveType==Constants.Sine_Movement_Type)
					speed=speed*-1;
				else
					speedVector= new Vector3(-speed, 0, 0);
			
				break;
		

			case Constants.fromTheLeft:
			
				//setting the image rotation.
				setRotation(whichDirection);
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position=new Vector3(_x0 - _offset, 
											_y0 + _yGap*whichPoint,
											_zValue);
			
				//calling the Invoke Function.	
				MoveState=Constants.Horiz_Move;				
				
			
				if(moveType==Constants.Sine_Movement_Type)
					{}
				else
					speedVector= new Vector3(speed, 0, 0);

			
				break;	
			
			case Constants.fromTheTop:
			
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position= 
									new Vector3(_x0 + _xGap*whichPoint, 
											_y5 + _offset,
											_zValue);
			

			
				//calling the Invoke Function.	
				MoveState=Constants.Vert_Move;				
				
				if(moveType==Constants.Sine_Movement_Type)
					speed=speed*-1;
				else
					speedVector= new Vector3(0, -speed, 0);

			
			
				break;
			
			case Constants.fromTheBottom:
			
				//setting the image rotation, just in case.
				setRotation(whichDirection);
			
				//setting Position.  The point given must be between 1 to 4.
				this.rigidbody.transform.position=
									new Vector3(_x0 + _xGap*whichPoint, 
											_y0 - _offset,
											_zValue);
			
				//calling the Invoke Function.	
				MoveState=Constants.Vert_Move;				
				
			
				if(moveType==Constants.Sine_Movement_Type)
					{}
				else
					speedVector= new Vector3(0, speed, 0);

			
			
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
		

		//Checking the MoveState variable to modify the speed vector.
		if(MoveState==Constants.Horiz_Move)	
		{
			
			if(moveType==Constants.Sine_Movement_Type)
				{
				
				sineCount += Time.deltaTime;
				
				//Setting a sine wave movement for the speedVector.
				speedVector= new Vector3(speed, 
						myAmplitude*Mathf.Sin(myFrequency*sineCount), 
						0);
							
				}
					
				 //Moving the rigidbody with a speedVector.
				rigidbody.MovePosition(rigidbody.position + speedVector);	
			
		}
		
		else if(MoveState==Constants.Vert_Move)	
		{
			
			if(moveType==Constants.Sine_Movement_Type)
				{
				
				sineCount += Time.deltaTime;

				
				//Setting a sine wave movement for the speedVector.
				speedVector= new Vector3(
						myAmplitude*Mathf.Sin(myFrequency*sineCount), 
						speed,
						0);
				}
				
			
				 //Moving the rigidbody with a speedVector.
				rigidbody.MovePosition(rigidbody.position + speedVector);	
			
		}
		
			
	}
	
	
	//Returning the enemy rigidbody to a default position.	
	public void returnToPosition()
	{
	
		//resetting the sineCount(if a sine movement was being used).		
		sineCount=0;
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
				
			
				MoveState=Constants.No_Move;
				returnToPosition();
				MainScript.myMainScript.enemyReset();

			}	
	
	}
	
	
	//This death sequence coroutine is called when the enemy is hit.
	IEnumerator deathSequence()	
	{
		
		//Stopping the enemy's rigidbody.
		MoveState=Constants.No_Move;		
		this.rigidbody.detectCollisions=false;
		this.rigidbody.Sleep ();
		
		//The enemy texture changes to an explosion texture.			
		speedVector=Vector3.zero;
		
		//The enemy texture changes to an explosion texture.			
		renderer.material.mainTexture=(Texture2D)Resources.Load(Constants.bomberDeath);
		audio.Play ();
	
		//Then wait a little bit before resetting the enemy object.
		yield return new WaitForSeconds(0.3f);

		returnToPosition();
		MainScript.myMainScript.enemyReset();

		
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
