using UnityEngine;
using System.Collections;

public class chasePlayer : MonoBehaviour {

	private GameObject player;

	private bool turnRight = false;
	private bool turnLeft = false;
	private bool moveForward = false;

	private float forwardSpeed;
	private float turnSpeed;
	private float relativePoint;
	private float distanceToPlayer;
//	private float minDistance = 12;
//	private float goodDistance =25;
//	private float maxDistance = 40;

	private Vector3 relativePlayerPoint;
	private Vector3 playerRotation;
	private Vector3 aiRotation;
	private Vector3 playerPrevPos;
	private Vector3 playerNewPos;
	private Vector3 rotationDif;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		turnSpeed = PlayerMove2.turnSpeed;
		forwardSpeed = 20;
	}

	// Update is called once per frame
	void Update () {
		relativePoint = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and all planets
		//		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI
		//
		//		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and player
		//		Debug.Log("distanceToPlayer " + distanceToPlayer);
		//		Debug.Log("relativePlayerPoint " + relativePlayerPoint);

		playerPrevPos = playerNewPos; //Updates old player position
		playerNewPos = player.transform.position; //Updates new player position

		planNewRoute();

		//		if(relativePoint <= minDistance) //AI to close to a planet
		//		{
		//			GetComponent<avoidPlanet> ().enabled = false;
		//			followPlayer();
		//		}

		//		if(relativePoint >= aiPlanetDistance) //Out of danger
		//		{
		//			GetComponent<AImove> ().enabled = true;
		//			MoveTo.newBall = true;
		//		}

		if(moveForward == true)
		{
			transform.Translate (Vector3.forward/forwardSpeed);
		}

		if (turnLeft == true) 
		{
			transform.Rotate (Vector3.down, turnSpeed * Time.deltaTime);
		}

		if (turnRight == true) 
		{
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		}
	}

	void planNewRoute()
	{
		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and player
		playerRotation =  gameObject.transform.localEulerAngles;
		aiRotation = this.gameObject.transform.localEulerAngles;
		rotationDif = aiRotation-playerRotation;

		//if(rotationDif < 0) {rotationDif *= -1;}

		Debug.Log(relativePlayerPoint);


//		if(relativePlayerPoint.x >-0.1 && relativePlayerPoint.x <0.1) //The player is to the front of the AI
//		{
//			playerToTheFront();
//			Debug.Log("Player fremover");
//		}
//
//		if(relativePlayerPoint.x <= -0.1)//The player is to the left of the AI
//		{
//			playerToTheLeft();
//			Debug.Log("Player til venstre");
//		}
//
//		if(relativePlayerPoint.x >= 0.1) //The player is to the right of the AI
//		{
//			playerToTheRight();
//			Debug.Log("Player til høyre");
//		}
	}

	void playerToTheFront()
	{

		if(relativePlayerPoint.z > 0 && relativePlayerPoint.z < 0.2)
		{
			if(playerPrevPos.x < playerNewPos.x) //Player is drivnig to the right
			{
				onlyLeft();
			}
			else 
			{
				onlyRight();
			}
		}

		else if(relativePlayerPoint.z > 0.1 && relativePlayerPoint.z < 0.4)
		{
			if(playerRotation.y <= 90) //Player is driving up-right
			{
				rightandForward();
			}
			else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-right
			{
				onlyRight();
			}
			else if(playerRotation.y > 180 && playerRotation.y <= 270) //Player is driving down-left
			{
				onlyLeft();
			}
			else if(playerRotation.y > 270 && playerRotation.y <= 360) //Player is driving up-left
			{
				leftAndForward();
			}
		}

		else if(relativePlayerPoint.z > 0.4)
		{
			if(playerRotation.y <= 90) //Player is driving up-right
			{
				rightandForward();
			}
			else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-right
			{
				rightandForward();
			}
			else if(playerRotation.y > 180 && playerRotation.y <= 270) //Player is driving down-left
			{
				leftAndForward();
			}
			else if(playerRotation.y > 270 && playerRotation.y <= 360) //Player is driving up-left
			{
				leftAndForward();
			}
		}
	}

	void playerToTheLeft()
	{
		if(relativePlayerPoint.x > -0.5)
		{
			if(relativePlayerPoint.z > -0.1 && relativePlayerPoint.z < 0.1)
			{
				//Unsure if I should test this at the beginning or in every test in the code
				//Checking in the beginning might be the least popular option, because if the
				//AI is driving one direction and the player in the other while the difference is small enough
				//they will forever increase the distance between them.
//				if(rotationDif < 5) 
//				{
//
//				}
				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
				{
					if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
					{
						onlyForward(); //We continue driving forwards
					}
					else if(aiRotation.y <= 180) //ai is driving to the right
					{
						leftAndForward();
					}
					else if(aiRotation.y > 180) //ai is facing the wrong way
					{
						rightandForward();
					}
				}
				else if(playerRotation.y <= 45) //Player is driving up-right
				{
					if(PlayerMove2.turnRight == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{
							leftAndForward();
						}
						else if(aiRotation.y <= 180) //ai is driving to the right
						{
							leftAndForward();
						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{
							rightandForward();
						}
					}

					else if(PlayerMove2.turnLeft == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{
							onlyForward();
						}
						else if(aiRotation.y <= 180) //ai is driving to the right
						{
							leftAndForward();
						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{
							rightandForward();
						}
					}

					else
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{
							onlyRight();
						}
						else if(aiRotation.y <= 180) //ai is driving to the right
						{
							leftAndForward();
						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{
							rightandForward();
						}
					}
				}
				else if(playerRotation.y > 45 && playerRotation.y <= 90) //Player is driving up-right
				{
					if(PlayerMove2.turnRight == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{
							leftAndForward();
						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}

					else if(PlayerMove2.turnLeft == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{
							
						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}

					else
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{

						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}
				}
				else if(playerRotation.y > 90) //Player is driving down-left
				{
					if(PlayerMove2.turnLeft == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{

						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}

					if(PlayerMove2.turnRight == true)
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{

						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}
					else
					{
						if(aiRotation.y >= 358 || playerRotation.y <= 2) //the ai is basically driving straight forward
						{

						}
						else if(aiRotation.y <= 45) //ai is driving to the right
						{

						}
						else if(aiRotation.y > 180) //ai is facing the wrong way
						{

						}
					}
				}
			}

			else if(relativePlayerPoint.z >= 0.1)
			{
				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
				{
					if(aiRotation.y >= 358 || playerRotation.y <= 2)
					{
						onlyForward();
					}


				}

				else if(playerRotation.y <= 90) //Player is driving up-right
				{
					if(PlayerMove2.turnRight == true || PlayerMove2.turnLeft == true)
					{
						onlyForward();
					}
						
					else rightandForward();
				}
				else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-left
				{
					if(PlayerMove2.turnLeft == true)
					{
						onlyForward();
					}

					else leftAndForward();
				}

				else if(playerRotation.y > 180) //Player is driving down-left
				{
					if(PlayerMove2.turnLeft == true)
					{
						leftAndForward();
					}

					else if(PlayerMove2.turnRight == true)
					{
						onlyForward();
					}

					else leftAndForward();
				}
			}

//			else if(relativePlayerPoint.z >= 0.2)
//			{
//
//			}
//
			else if(relativePlayerPoint.z <= -0.1)
			{
				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
				{
					onlyForward();
				}

				else if(playerRotation.y <= 90) //Player is driving up-right
				{
					if(PlayerMove2.turnRight == true || PlayerMove2.turnLeft == true)
					{
						onlyForward();
					}

					else rightandForward();
				}
				else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-left
				{
					if(PlayerMove2.turnLeft == true)
					{
						onlyForward();
					}

					else leftAndForward();
				}

				else if(playerRotation.y > 180) //Player is driving down-left
				{
					if(PlayerMove2.turnLeft == true)
					{
						leftAndForward();
					}

					else if(PlayerMove2.turnRight == true)
					{
						onlyForward();
					}

					else leftAndForward();
				}
			}
		}
	}

//	void playerToTheRight()
//	{
//		if(relativePlayerPoint.x > 0.5)
//		{
//			if(relativePlayerPoint.z > -0.1 && relativePlayerPoint.z < 0.1)
//			{
//				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
//				{
//					onlyForward();
//					if(aiRotation.y >= 358 || aiRotation.y <= 2) //The ai is basically driving straight forward
//					{
//
//					}
//				}
//				else if(playerRotation.y <= 45) //Player is driving up-right
//				{
//					if(PlayerMove2.turnRight == true)
//					{
//						leftAndForward();
//					}
//
//					else if(PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else onlyRight();
//				}
//				else if(playerRotation.y > 45 && playerRotation.y <= 90) //Player is driving up-right
//				{
//					if(PlayerMove2.turnRight == true)
//					{
//						leftAndForward();
//					}
//
//					else if(PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//				else if(playerRotation.y > 90) //Player is driving down-left
//				{
//					if(PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//			}
//
//			else if(relativePlayerPoint.z >= 0.1)
//			{
//				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
//				{
//					if(aiRotation.y >= 358 || playerRotation.y <= 2)
//					{
//						onlyForward();
//					}
//
//
//				}
//
//				else if(playerRotation.y <= 90) //Player is driving up-right
//				{
//					if(PlayerMove2.turnRight == true || PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else rightandForward();
//				}
//				else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-left
//				{
//					if(PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//
//				else if(playerRotation.y > 180) //Player is driving down-left
//				{
//					if(PlayerMove2.turnLeft == true)
//					{
//						leftAndForward();
//					}
//
//					else if(PlayerMove2.turnRight == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//			}
//
//			//			else if(relativePlayerPoint.z >= 0.2)
//			//			{
//			//
//			//			}
//			//
//			else if(relativePlayerPoint.z <= -0.1)
//			{
//				if(playerRotation.y >= 358 || playerRotation.y <= 2) //The player is basically driving straight forward
//				{
//					onlyForward();
//				}
//
//				else if(playerRotation.y <= 90) //Player is driving up-right
//				{
//					if(PlayerMove2.turnRight == true || PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else rightandForward();
//				}
//				else if(playerRotation.y > 90 && playerRotation.y <= 180) //Player is driving down-left
//				{
//					if(PlayerMove2.turnLeft == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//
//				else if(playerRotation.y > 180) //Player is driving down-left
//				{
//					if(PlayerMove2.turnLeft == true)
//					{
//						leftAndForward();
//					}
//
//					else if(PlayerMove2.turnRight == true)
//					{
//						onlyForward();
//					}
//
//					else leftAndForward();
//				}
//			}
//		}
//	}

	//Here comes a bunch of prefab code to decrease
	//the amount of reused lines of code.
	private void leftAndForward()
	{
		turnLeft = true;
		turnRight = false;
		moveForward = true;
	}

	private void rightandForward()
	{
		turnLeft = false;
		turnRight = true;
		moveForward = true;
	}

	private void onlyLeft()
	{
		turnLeft = true;
		turnRight = false;
		moveForward = false;
	}

	private void onlyRight()
	{
		turnLeft = false;
		turnRight = true;
		moveForward = false;
	}

	private void onlyForward()
	{
		turnLeft = false;
		turnRight = false;
		moveForward = true;
	}

}
