using UnityEngine;
using System.Collections.Generic;

public class gravityPull : MonoBehaviour
{
	//Setter størrelse på gravtasjonsfeltet
	public float range = 10f;
	//Lager en rigidbody
	Rigidbody ownRb;

	void Start()
	{
		//Henter rigidbody til objektet scriptet er koblet til
		ownRb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//Lager en array av collidere som holder alle colliderene som er 
		//innenfor gravitasjonsfeltet
		Collider[] cols = Physics.OverlapSphere(transform.position, range);
		//Lager en list som holder på Rigidbodies
		List<Rigidbody> rbs = new List<Rigidbody>();

		//Kjøres for hver collider som er i arrayen cols
		foreach (Collider c in cols)
		{
			//Finner rigidbody til objektet som er inne i gravitasjonsfeltet.
			Rigidbody rb = c.attachedRigidbody;
			//Hvis objektet som er inne i gravitasjonsfeltet har en rigidbody,
			//ikke er rigidbody-en til objektet scriptet er koblet til 
			//eller at rigidboien allerede er lagt til i lista med rigidbodies,
			//så kjøres dette.
			if (rb != null && rb != ownRb && !rbs.Contains(rb))
			{
				//Den nye rigidbodien blir lagt til i lista.
				rbs.Add(rb);
				//Regner ut avstanden mellom objektet med gravitasjon og det andre objektet.
				Vector3 offset = transform.position - c.transform.position;
				//Regner ut gravitasjonskrefter og dytter objektet mot objektet med gravitasjon.
				rb.AddForce(offset / offset.sqrMagnitude * ownRb.mass);
			}
		}
	}

	void OnDrawGizmos()
	{
		//Lager en Gizmos som viser gravitasjonsfeltet i scenen.
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}