using UnityEngine;
using System.Collections;

public class ChangeMaterial : MonoBehaviour {

	//array of Materials. Range from full health to reallty damaged
	public Material[] playerMat = new Material[3];

	//Health limits for change of material
	private float material2Limit;
	private float material3Limit;

	private float fullHealth;
	private Material currentMat;
	private bool firstTimeCheck = false;

	// Use this for initialization
	void Start () 
	{
		currentMat = GameObject.Find("Player_Ship_Collected").GetComponentInParent<MeshRenderer>().material;
		//Calculates max health player can have using hullupgrade
		fullHealth = 100 + ((GameControl.control.hullUpgrade-1) * 50);
		//Calculating limits for change of material
		material2Limit = fullHealth * 0.66f;
		material3Limit = fullHealth * 0.33f;
		//Changes material after player health. This happens when game starts or when player goes out of shop.
		checkPlayerHealth ();
	}

	public void checkPlayerHealth()
	{
		//Checks which material playership should have after what health
		if (GameControl.control.health > material2Limit && GameControl.control.health > material3Limit)
		{
			//Setting new materials and removing fire and smoke if it exist
			setNewMaterial (0);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == true)
				this.GetComponentInParent<DamagedPlayer>().removeSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == true)
				this.GetComponentInParent<DamagedPlayer>().removeFire();
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health > material3Limit)
		{
			//Player is at medium health. Smoke is present, but not fire
			setNewMaterial (1);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == false)
				this.GetComponentInParent<DamagedPlayer>().startSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == true)
				this.GetComponentInParent<DamagedPlayer>().removeFire();
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health < material3Limit)
		{
			//Last but of health. Smoking and on fire
			setNewMaterial (2);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == false)
				this.GetComponentInParent<DamagedPlayer>().startSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == false)
				this.GetComponentInParent<DamagedPlayer>().startFire();
		}
	}

	private void setNewMaterial(int matNr)
	{
		//Checking if it is first time entering game.
		//Setting player animation to angry if so
		if (firstTimeCheck == true)
		{
			GameObject.Find ("dialogue_elements").GetComponentInParent<CombatAnimationController> ().setAngry ("Player");
		} 
		else
		{
			firstTimeCheck = true;
		}

		//Changes material that fits health.
		if (playerMat[matNr].name != currentMat.name)
		{
			GameObject.Find ("dialogue_elements").GetComponentInParent<CombatAnimationController> ().setHappy ("Enemy");
			GetComponentInParent<Renderer> ().material = new Material (playerMat [matNr]);
			currentMat = playerMat [matNr];

		}
	}

}
