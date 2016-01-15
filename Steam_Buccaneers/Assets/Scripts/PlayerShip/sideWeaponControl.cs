﻿using UnityEngine;
using System.Collections;

public class sideWeaponControl : MonoBehaviour 
{
	public AudioSource pangPang;
	public GameObject cannonball;
	public GameObject[] leftCannons;
	public GameObject[] rightCannons;
	public float fireRate;
	public float fireDelayLeft;
	public float fireDelayRight;
	public GameObject cannonL1;
	public GameObject cannonL2;
	public GameObject cannonL3;
	public GameObject cannonR1;
	public GameObject cannonR2;
	public GameObject cannonR3;

	// Use this for initialization
	void Start () 
	{
		//AudioSource pangPang = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		AudioSource pangPang = GetComponent<AudioSource> ();
		if (Input.GetKey (KeyCode.Q) && Time.time > fireDelayLeft) // && Inventory.mainAmmo > 0
		{
			//Debug.Log ("pang");
			fireDelayLeft = Time.time + fireRate;
			//Hadde helst sett til at de var i en array, men det fikk jeg ikke til akkurat nå, ballene spawner på de andre når de er i en array av en eller 
			//annen grunn.

			/*
			 * Hvis jeg noengang får denne arrayen her til å fungere, som jeg veldig gjerne vil, så kan alle kanonene være prefabs i steden for hver sitt objekt, da
			 * er det bare å ha forskjellige scripts til hver kanon, legge disse på prefabsa, og så pushe de inn i arrayen på riktig plass.
			 */

			for (int i = 0; i <= 2; i++)
			{
				// fungerer ikke dette fordi left cannons blir på en måte lik cannonball som man skyter, så her blir "leftCannons[i] = gameobject dvs cannonball, og derfor
				// spawner alle nye kuler på de som allerede finnes?
				Instantiate (cannonball, leftCannons[i].transform.position, leftCannons[i].transform.rotation);
				//Debug.Log ("Left pew");
				//Debug.Log (i);
			}
			//må finne ut av, array er mye ryddigere.
			/*leftCannons[0]=(GameObject)Instantiate (cannonball, leftCannons[0].transform.position, transform.rotation);
			leftCannons[1]=(GameObject)Instantiate (cannonball, leftCannons[1].transform.position, transform.rotation);
			leftCannons[2]=(GameObject)Instantiate (cannonball, leftCannons[2].transform.position, transform.rotation);*/

			//akkurat det her er egentlig ganske rotete side vi på lage så mange GameObjects i klassa, men det funker for nå.
			//array er antakeligvis lettere å bruke uansett om man skal kunne bytte våpen på skipet og sånt, bare fjerne et våpen fra arrayen og pushe inn det nye.
			//Det som er "rart" med den her, er at den skyter i rotasjonen til parenten, ikke rart, en selvfølge, men med transform.rotation er det ingen enkel måte
			//å skrive (-)rotation på kanonløpa. Derfor har jeg også rotert kanonløpa i selve scenen for at det skal gå på denne måten. Ser rart ut, men funker foreløpig.
			// som en quick fix på en demo/proto.

			//denne er den som fungerer
			//Instantiate (cannonball, cannonL1.transform.position, cannonL1.transform.rotation);
			//Instantiate (cannonball, cannonL2.transform.position, cannonL2.transform.rotation);
			//Instantiate (cannonball, cannonL3.transform.position, cannonL3.transform.rotation);

			//Inventory.mainAmmo -= 1;
			//Debug.Log (PlayerStatus.mainAmmo);
			pangPang.Play();
			//AudioSource pangPang = GetComponent<AudioSource> ();
			//transform.Translate (Vector3.up/forwardSpeed);
		}

		if (Input.GetKey (KeyCode.E) && Time.time > fireDelayRight) 
		{

			Debug.Log ("pang");
			fireDelayRight = Time.time + fireRate;
			/*rightCannons[0]=(GameObject)Instantiate (cannonball, rightCannons[0].transform.position, transform.rotation);
			rightCannons[1]=(GameObject)Instantiate (cannonball, rightCannons[1].transform.position, transform.rotation);
			rightCannons[2]=(GameObject)Instantiate (cannonball, rightCannons[2].transform.position, transform.rotation);*/

			for (int i = 0; i <= 2; i++)
			{
				Instantiate (cannonball, rightCannons[i].transform.position, transform.rotation);
				//Debug.Log ("right pew");
			}
			/*
			Instantiate (cannonball, cannonR1.transform.position, transform.rotation);
			Instantiate (cannonball, cannonR2.transform.position, transform.rotation);
			Instantiate (cannonball, cannonR3.transform.position, transform.rotation);*/
			//Inventory.mainAmmo -= 1;
			pangPang.Play();
			//Debug.Log (PlayerStatus.mainAmmo);
			//AudioSource pangPang = GetComponent<AudioSource> ();
			//transform.Translate (Vector3.up/forwardSpeed);
		}
	
	}
}