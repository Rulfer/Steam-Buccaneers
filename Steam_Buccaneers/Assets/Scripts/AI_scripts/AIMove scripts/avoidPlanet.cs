using UnityEngine;
using System.Collections;

public class avoidPlanet : MonoBehaviour {
	public static avoidPlanet avoid;

	private GameObject player;
	private GameObject[] planets;

	private int planetI;

	private bool turnRight = false;
	private bool turnLeft = false;

	public static float force = 200.0f;
	public static int turnSpeed = 50;
	public static Rigidbody aiRigid;
	Vector3 maxVelocity = new Vector3 (3.5f, 0.0f, 3.5f);


	private float relativePoint;
	private float aiPlanetDistance;
	private float distanceToPlanet;
	private float distanceToPlayer;
	private float minPlanetForwardDistance = 60;
	private float minPlanetSideDistance = 40;

	public float minDist = 20f;
	public float maxDist = 40f;

	private Vector3 relativePlanetPoint;
	private Vector3 relativePlayerPoint;
	private Vector3 playerPrevPos;
	private Vector3 playerNewPos;

    void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		planets = GameObject.FindGameObjectsWithTag("Planet");
		planetI = detectPlanet();

		player = GameObject.FindGameObjectWithTag("Player");

		aiPlanetDistance = 100;
	}

	// Update is called once per frame
	void Update () {
		
		relativePoint = Vector3.Distance (this.transform.position, planets[planetI].transform.position); //Distance between AI and all planets
		relativePlayerPoint = transform.InverseTransformPoint(player.transform.position); //Used to check if the player is to the left or right of the AI
		relativePlanetPoint = transform.InverseTransformPoint(planets[planetI].transform.position); //Used to check if the planet is to the left or right of the AI
		distanceToPlanet = Vector3.Distance (this.transform.position, planets[planetI].transform.position); //distance between AI and planet
		distanceToPlayer = Vector3.Distance (this.transform.position, player.transform.position); //Distance between AI and player
		playerPrevPos = playerNewPos; //Updates old player position
		playerNewPos = player.transform.position; //Updates new player position

		if(relativePoint <= aiPlanetDistance) //AI to close to a planet
		{
			GetComponent<AImove> ().enabled = false;
			planNewRoute();
		}

		if(relativePoint >= aiPlanetDistance) //Out of danger
		{
			GetComponent<AImove> ().enabled = true;
		}

		if (aiRigid.velocity.x <= -maxVelocity.x)
		{
			aiRigid.velocity = new Vector3 (-maxVelocity.x, 0.0f, aiRigid.velocity.z);
		}

		if (aiRigid.velocity.z >= maxVelocity.z)
		{
			aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, maxVelocity.z);
		}

		if (aiRigid.velocity.z <= -maxVelocity.z)
		{
			aiRigid.velocity = new Vector3 (aiRigid.velocity.x, 0.0f, -maxVelocity.z);
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

	//Searches the array containing all planets and their position.
	//If a planet is to close, we will initiate the evasive maneuvers(I cant spell)
	private int detectPlanet()
	{
		float temp;
		for(int i = 0; i < planets.Length; i++) //Runs equal to array length
		{
			temp = Vector3.Distance (this.transform.position, planets[i].transform.position); //Distance between AI Ship and the chosen planet
			if(temp <= aiPlanetDistance) //If the planet is to close, this is true
			{
				planetI = i; //Saves the array index
			}
		}
		return planetI; //Returnes array index
	}

	void planNewRoute()
	{
		if(relativePlayerPoint.x >-0.1 && relativePlayerPoint.x <0.1) //The player is to the front of the AI
		{
			playerToTheFront();
		}

		else if(relativePlayerPoint.x < -0.1)//The player is to the left of the AI
		{
			playerToTheLeft();
		}

		else if(relativePlayerPoint.x > 0.1) //The player is to the right of the AI
		{
			playerToTheRight();
		}
	}

	private void playerToTheFront()
	{
		//The planet is in front of the AI
		if(relativePlanetPoint.x == 0)
		{
			if(distanceToPlanet < distanceToPlayer) //We now know that the planet is between the ai and player
			{
				if(playerPrevPos.x < playerNewPos.x) //Player is driving to the right
				{
					if(distanceToPlanet < minPlanetForwardDistance) //AI is too close to the planet
					{
						onlyRight();
					}
					else //AI is far enough away
					{
						rightandForward();
					}
				}

				else //The player is driving to the left
				{
					if(distanceToPlanet < minPlanetForwardDistance) //AI is too close to the planet
					{
						onlyLeft();
					}
					else//AI is far enough away
					{
						leftAndForward();
					}
				}
			}

			else //The planet is not between the player and AI
			{
				if(distanceToPlanet < minPlanetForwardDistance)//AI is too close to the planet
				{
					onlyLeft();
				}
				else//AI is far enough away
				{
					onlyForward();
				}
			}

		}

		//The planet is to the left of the AI
		if(relativePlanetPoint.x < 0)
		{
			if(relativePlanetPoint.z > 0) //The planet is to the front-left of the AI
			{
				if(distanceToPlanet < minPlanetSideDistance) //The planet is too close to the AI
				{
					rightandForward();
				}

				else
				{
					onlyForward();
				}
			}

			else //The planet is on the rear or side of the AI
			{
				onlyForward();
			}
		}

		if(relativePlanetPoint.x > 0) //The planet is to the right of the AI
		{
			if(relativePlanetPoint.z > 0) //The planet is to the front-right of the AI
			{
				if(distanceToPlanet < minPlanetSideDistance) //The planet is close
				{
					leftAndForward();
				}

				else //The planet is far enough away
				{
					onlyForward();
				}
			}

			else //The planet is on the rear or side of the AI
			{
				onlyForward();
			}
		}
	}

	private void playerToTheLeft()
	{
		if(relativePlanetPoint.x == 0) //the planet is right in front of the AI
		{
			if(distanceToPlanet < minPlanetForwardDistance)
			{
				onlyLeft();
			}
			else
			{
				leftAndForward();
			}
		}

		if(relativePlanetPoint.x < 0) //The planet is to the left of the AI
		{
			if(relativePlanetPoint.z > 0) //The planet is to the front
			{
				if(relativePlayerPoint.z > 0) //The player is to the front-left of the AI
				{
					if(relativePlayerPoint.z < relativePlanetPoint.z) //The player is not behind the planet
					{
						if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
						{
							onlyLeft();
						}
						else //The AI is far enough away from the planet
						{
							leftAndForward();
						}
					}

					else //The player could be behind the planet
					{
						if(relativePlayerPoint.x < relativePlanetPoint.x) //The player is not behind the planet
						{
							leftAndForward();
						}

						else //The player is behind the planet
						{
							if(playerPrevPos.x > playerNewPos.x) //We know the player is moving away from the AI
							{
								if(playerPrevPos.z < playerNewPos.z) //Upwards and away from the AI
								{
									if(PlayerMove2.turnLeft == true) //The player is turning, follow it!
									{
										if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
										{
											onlyLeft();
										}

										else
										{
											leftAndForward();
										}
									}

									else //The player is not turning
									{
										//Need to find a better way to calculate what way around the planet the AI should drive.
										//For all we know the player is right next to the damn thing, and the AI
										//is still driving to the right.
										if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
										{
											onlyRight();
										}

										else
										{
											rightandForward();
										}
									}
								}
								//Its driving down and left, so we follow it
								if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
								{
									onlyLeft();
								}

								else
								{
									leftAndForward();
								}
							}

							else //The player is driving to the right, towards the AI
							{
								//Seen as how both tests under this is equal, its unnessessary to include them both
								//Saves them for now, in case of needed change later.
								if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
								{
									onlyRight();
								}

								else
								{
									rightandForward();
								}
							}
						}
					}
				}
			}

			else //The planet is to the rear
			{
				if(relativePlayerPoint.z > 0) //The player is to the front-left of the AI
				{
					leftAndForward(); //We aim for the player
				}

				else //The player is back with the planet
				{
					if(relativePlanetPoint.z < relativePlayerPoint.z) //The player is not behind the planet
					{
						if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
						{
							onlyLeft();
						}
						else
						{
							leftAndForward();
						}
					}

					else //the player could be behind the planet
					{
						if(relativePlayerPoint.x > relativePlanetPoint.x) //The player is not behind the planet
						{
							if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
							{
								onlyLeft();
							}
							else
							{
								leftAndForward();
							}
						}

						else //The player is behind the planet
						{
							//It does not matter what direction the player is driving, the AI
							//must turn to the left anyway
							if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
							{
								onlyLeft();
							}
							else
							{
								leftAndForward();
							}
						}
					}
				}
			}
		}
	}

	private void playerToTheRight()
	{
		if(relativePlanetPoint.x == 0) //the planet is right in front of the AI
		{
			if(distanceToPlanet < minPlanetForwardDistance)
			{
				onlyRight();
			}
			else
			{
				rightandForward();
			}
		}

		if(relativePlanetPoint.x > 0) //The planet is to the right of the AI
		{
			if(relativePlanetPoint.z > 0) //The planet is to the front
			{
				if(relativePlayerPoint.z > 0) //The player is to the front-right of the AI
				{
					if(relativePlayerPoint.z < relativePlanetPoint.z) //The player is not behind the planet
					{
						if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
						{
							onlyRight();
						}
						else //The AI is far enough away from the planet
						{
							rightandForward();
						}
					}

					else //The player could be behind the planet
					{
						if(relativePlayerPoint.x < relativePlanetPoint.x) //The player is not behind the planet
						{
							rightandForward();
						}

						else //The player is behind the planet
						{
							if(playerPrevPos.x < playerNewPos.x) //We know the player is moving away from the AI
							{

								//Need a better way to ca
								if(playerPrevPos.z < playerNewPos.z) //Upwards and away from the AI
								{
									if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
									{
										leftAndForward();
									}

									else
									{
										rightandForward();
									}
								}

								else //Down and away from the AI
								{
									if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
									{
										onlyRight();
									}

									else
									{
										rightandForward();
									}
								}
							}

							else //The player is driving to the left, towards the AI
							{
								//Seen as how both tests under this is equal, its unnessessary to include them both
								//Saves them for now, in case of needed change later.
								if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
								{
									onlyRight();
								}

								else
								{
									rightandForward();
								}
							}
						}
					}
				}
			}

			else //The planet is to the rear
			{
				if(relativePlayerPoint.z > 0) //The player is to the front-right of the AI
				{
					if(playerPrevPos.x < playerNewPos.x)//The player is moving to the left
					{
						leftAndForward();
					}
					else
					{
						rightandForward(); //We aim for the player
					}
				}

				else //The player is back with the planet
				{
					if(relativePlanetPoint.z < relativePlayerPoint.z) //The player is not behind the planet
					{
						if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
						{
							onlyRight();
						}
						else
						{
							rightandForward();
						}
					}

					else //the player could be behind the planet
					{
						if(relativePlayerPoint.x < relativePlanetPoint.x) //The player is not behind the planet
						{
							if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
							{
								onlyRight();
							}
							else
							{
								rightandForward();
							}
						}

						else //The player is behind the planet
						{
							//It does not matter what direction the player is driving, the AI
							//must turn to the right anyway
							if(distanceToPlanet < minPlanetSideDistance) //The planet is to close to the AI
							{
								onlyRight();
							}
							else
							{
								rightandForward();
							}
						}
					}
				}
			}
		}
	}

	//Here comes a bunch of prefab code to decrease
	//the amount of reused lines of code.
	private void leftAndForward()
	{
		turnLeft = true;
		turnRight = false;
	}

	private void rightandForward()
	{
		turnLeft = false;
		turnRight = true;
	}

	private void onlyLeft()
	{
		turnLeft = true;
		turnRight = false;
	}

	private void onlyRight()
	{
		turnLeft = false;
		turnRight = true;
	}

	private void onlyForward()
	{
		turnLeft = false;
		turnRight = false;
	}
}
