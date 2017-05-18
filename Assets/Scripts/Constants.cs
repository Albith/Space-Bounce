
//This class contains several global variables.
static class Constants	
{
	
	//Game Status Messages.
	public const string startGametxt= "-SPACE BOUNCE-\n\nJust bouncing.\nTo start enemies, press space...";
	public const string firstMsgtxt= "m key= throw your ball.\narrow keys= move the ship \n\n\n\n\n\n\n\n\n r key= resets your ship.";
	public const string gameWintxt= "You win!\n\nBaller for life!";
	public const string gameOvertxt= "You lost... your ball drifts off into space";
	
	//Game Logic variables.
	public const int numberOfWaves=7;		//originally 20.
	public const int numberOfLives=3;		//originally 3.

	public const int numberOfEnemiesInArray=10;	
	public const int numberOfFlockEnemiesInArray=12;
	
	//Time delay values.
	public const float waitTimeBetweenWaves=1f;
	public const float waitTimeInsideWave=0.5f;
	
	//More Flock related variables.
	public const float flockEnemyGap=1.1f;
	public const int numberPerFlock=4;	

	
	//Ball Collision states.
	public const int BallOnTop=21;
	public const int BallOnBottom=22;
	public const int BallOnFront=23;
	public const int BallOnBack=24;
	
	public const int BallOnAir=25;
		
	public const int BallAttachedHittingWall=26;
	
	//Image file names, for any images that are frequently interchanged.
	public const string bomberName="Bomber_Monster1";
	public const string flockBomberName="Bomber_Monster2";
	
	public const string shipImageName="ship";

	public const string bomberDeath="Bomber_explosion";
	public const string shipDeath="Ship_explosion";
	
	//Audio Clip array indexes. (There are more sounds than the 2 listed here.)
	public const int catchSound=0;
	public const int dribble1Sound=1;	
	
	//Enemy movement types. I'm using 3 movement types in this game. 	
	public const int Straight_Movement_Type=3;
	//public const int Trio_Movement_Type=4;
	public const int Sine_Movement_Type=5;
	public const int Flock_Movement_Type=6;
	
		
	//Enemy movement state (specifically for rigidbodies)
	public const int Horiz_Move=70;
	public const int Vert_Move=71;
	public const int No_Move=72;
	
	//Background Scrolling speed.
	public const float scrollingSpeed=0.3f;
	
	//Animation speeds constants.
	public const int Slow_Animation=20;
	public const int Med_Animation=21;
	public const int Fast_Animation=22;
	
	//Enemy directions constants.
	public const int fromTheRight= 41;
	public const int fromTheLeft= 42;
	public const int fromTheTop= 43;
	public const int fromTheBottom= 44;

}