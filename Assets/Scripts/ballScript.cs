using UnityEngine;
using System.Collections;

public class ballScript : MonoBehaviour {
	
	
	public int ballState;
	
	public float ballSpeed;
	
	public float maxBallSpeed;
	
	public AudioClip[] ballAudioClipsArray;	
	
	// Use this for initialization
	void Start () {
	
		ballSpeed=1.7f;
		maxBallSpeed=2.5f;
		
		
		ballState=Constants.BallOnFront;
		
		
	}


	
	void shootBall()
	{
			
				//Lets destroy the fixed joint that holds the ball 
				//to the ship.
				
			Destroy(GetComponent<FixedJoint>());
				
			//print ("Ball Fixed Joint destroyed.");
				
			
			switch(ballState)
				{
				
				case Constants.BallOnFront:
				//shooting from front.
					
					//print ("shooting ball from Front.");
			
			
					this.rigidbody.AddRelativeForce(this.transform.right * ballSpeed, ForceMode.Impulse);
					

			
					break;
					
				case Constants.BallOnBack:
					//shooting from front.
					
					//print ("shooting ball from Back.");

			
			
					this.rigidbody.AddRelativeForce(this.transform.right*-1 * ballSpeed, ForceMode.Impulse);
					

			
					break;
					
					
				case Constants.BallOnTop:
					//shooting from front.
					
					//print ("shooting ball from Top.");

			
			
					this.rigidbody.AddRelativeForce(this.transform.up * ballSpeed, ForceMode.Impulse);
					
		
			
			
					break;
					
				
					case Constants.BallOnBottom:
					//shooting from front.
					
					//print ("shooting ball from Bottom.");

			
					this.rigidbody.AddRelativeForce(this.transform.up*-1 * ballSpeed, ForceMode.Impulse);

					
			
					break;
					
					
					
					default:
					print("ballScript.cs, shootBall(): no ball direction stated.");
					
					break;
					
					
				
			}	//end of switch
				
			//Lastly, changing the Ball State		
				ballState=Constants.BallOnAir;
						
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("m")) 
		{
		
			
			
			if((ballState!= Constants.BallOnAir) && (ballState!=Constants.BallAttachedHittingWall))
			{
				//print ("Ball shot.");
				shootBall();
				
			}
			
				} 

		}		//end of Update function.
	 
		
	void OnTriggerEnter(Collider myCollider)
	{
				
		
		if(myCollider.tag=="top")
				ballState=Constants.BallOnTop;
				
		else if(myCollider.tag=="front")
				ballState=Constants.BallOnFront;		
			
		else if(myCollider.tag=="bottom")
				ballState=Constants.BallOnBottom;		
		
		else if(myCollider.tag=="back")
				ballState=Constants.BallOnBack;
		
		//a monster has been hit.			
		else if(myCollider.tag=="monster")
		{
			
			myCollider.GetComponent<EnemyScript>().monsterHitByBall();	
		}	
	
		//a monster has been hit.			
		else if(myCollider.tag=="monster_f")
		{
			
			myCollider.GetComponent<FlockEnemyScript>().monsterHitByBall();	
		}
	
	
	}
	
	void OnCollisionExit(Collision myCollision)
	{
		
		
		
		
		if(myCollision.gameObject.tag=="NSwall")	
			{
				
				//play the dribble
				audio.clip=ballAudioClipsArray[Constants.dribble1Sound];
				audio.Play();
			
				
		
		}	
		

		else if(myCollision.gameObject.tag=="EWwall")	
			{
				
				//play the dribble
				audio.clip=ballAudioClipsArray[Constants.dribble1Sound];
				audio.Play();
			
			
			}		
		
		else if (myCollision.gameObject.tag=="ship")
		{
		
			//play the catch sound
				audio.clip=ballAudioClipsArray[Constants.catchSound];
				audio.Play();
			
			
			if(ballState!=Constants.BallOnAir)	
				{	//adding the component.
					FixedJoint ballJoint= gameObject.AddComponent<FixedJoint>();
					ballJoint.connectedBody= shipScript.myShip.rigidbody;
				}
		
			
			
			
		}	
		

		
			
		
		
		
		
		
	}//end of OnCollisionEnter
	
	
	
}
