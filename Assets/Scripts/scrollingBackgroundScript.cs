using UnityEngine;
using System.Collections;

public class scrollingBackgroundScript : MonoBehaviour {
	
	//This simple class scrolls the background endlessly.

	float scrollingSpeed;
	bool isFirstTimeScrolling;
	

	// Use this for initialization
	void Start () {
	
		scrollingSpeed=Constants.scrollingSpeed;
			
	}
	
	// Update scrolls the background texture at a specified rate.
	void Update () 
	{
	
		
		transform.position= 
			new Vector3(transform.position.x - Time.deltaTime*scrollingSpeed,
				  		transform.position.y,
						transform.position.z);
		
		if(transform.position.x < -13.3051f)
			resetPosition();		
					
	}

	void resetPosition()
	{
		
			transform.position= new Vector3(6.696f, 
										transform.position.y,
										transform.position.z);
		
	}


}
