using UnityEngine;
using System.Collections;

public class changeMaterial : MonoBehaviour {

	public Material[] playerMat = new Material[3];

	private float material2Limit;
	private float material3Limit;
	private float fullHealth;
	private Material currentMat;
	private bool firstTimeCheck = false;

	// Use this for initialization
	void Start () 
	{
		currentMat = GameObject.Find("Player_Ship_Collected").GetComponentInParent<MeshRenderer>().material;
		//Calculates moments of material change
		fullHealth = 100;
		material2Limit = fullHealth * 0.66f;
		material3Limit = fullHealth * 0.33f;
		//Changes material after player health. This happens when game starts or when player goes out of shop.
		checkPlayerHealth ();
	}

	public void checkPlayerHealth()
	{
		//Checks which material playership should have.
		if (GameControl.control.health > material2Limit && GameControl.control.health > material3Limit)
		{
			setNewMaterial (0);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == true)
				this.GetComponentInParent<DamagedPlayer>().removeSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == true)
				this.GetComponentInParent<DamagedPlayer>().removeFire();
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health > material3Limit)
		{
			setNewMaterial (1);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == false)
				this.GetComponentInParent<DamagedPlayer>().startSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == true)
				this.GetComponentInParent<DamagedPlayer>().removeFire();
		} 
		else if (GameControl.control.health < material2Limit && GameControl.control.health < material3Limit)
		{
			setNewMaterial (2);
			if(this.GetComponentInParent<DamagedPlayer>().isSmoking == false)
				this.GetComponentInParent<DamagedPlayer>().startSmoke();
			if(this.GetComponentInParent<DamagedPlayer>().isBurning == false)
				this.GetComponentInParent<DamagedPlayer>().startFire();
		}
	}

	private void setNewMaterial(int matNr)
	{
		Debug.Log ("Material change = " + matNr);
		Debug.Log (playerMat [matNr].name);
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
			Debug.Log (playerMat [matNr].name);
			Debug.Log (currentMat.name);
			GetComponentInParent<Renderer> ().material = new Material (playerMat [matNr]);
			currentMat = playerMat [matNr];

		}
	}

}
