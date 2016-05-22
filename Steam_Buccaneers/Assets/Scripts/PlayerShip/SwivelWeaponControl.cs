using UnityEngine;
using System.Collections;

public class SwivelWeaponControl : MonoBehaviour 
{
	private Camera mainCam;
	public float speed;
	//public GameObject MainCam;

	private Vector3 mousePos;
	private Vector3 direction;

	//private float distanceFromObject;

	// Use this for initialization
	void Start () 
	{
		mainCam = Camera.main;
		//MainCam = GameObject.Find ("MainCamera");
		//main = MainCam.GetComponent<Camera>();

	}

	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//mousePos = Input.mousePosition;
		//henter musas pos.
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.y));
		Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.back) * 100, Color.green);
		//roterer mot musa
		//Den linja her er ganske "bugga" fungerer ikke som den skal, må fikses, alt annet er quick fixes.
		//den roterer objektet den er kobla til, så objektet følger ikke musa på riktig måte.
		//EDIT: kanonløpet følger musa riktig nå, alt som trengtes var skrive -90. Objektet roteres fortsatt på en ganske rar måte.
		//av en eller annen grunn som roterer linja under objektet i 90 grader eller noe sånt, og det funker ikke riktig.

		//transform.rotation = Quaternion.Euler(0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 90, 0);
		//transform.eulerAngles = new Vector3(0, Mathf.Atan2((mousePos.x - transform.position.x), (mousePos.z - transform.position.z)) * Mathf.Rad2Deg, 0);

		//husk at også projectile(scriptet) er endra på til å skyte "fremover" i sin y-akse, ikke x/z som den egentlig skal.
		transform.eulerAngles = new Vector3 (0,-Mathf.Atan2((mousePos.z - transform.position.z), (mousePos.x - transform.position.x))*Mathf.Rad2Deg +90,0);
		//transform.rotation = Quaternion.Euler (0,0,Mathf.Atan2((mousePos.y - transform.rotation.y), (mousePos.x - transform.rotation.x))*Mathf.Rad2Deg);

		//distanse mellom mus og swivelen
		//distanceFromObject = (Input.mousePosition - main.WorldToScreenPoint(transform.position)).magnitude;
		//flytte seg mot musa
		//GetComponent<Rigidbody>().AddForce(direction * speed * distanceFromObject * Time.deltaTime);
	}
}
