using UnityEngine;
using System.Collections;

public class Dado : MonoBehaviour
{

	public LayerMask dieValueColliderLayer = -1;
	public  int currentValue = 1;
	public bool rollComplete;
	public string buttonName = "Fire1";
	public GameObject dado;
	public float angularTorque = 10.0f;
	public float força = 10.0f;
	public ForceMode forceMode;
	public Rigidbody corpo;


	void Awake ()
	{
		corpo = gameObject.GetComponent<Rigidbody> ();


	}

	
	void Update ()
	{
	
		if (Input.GetButtonDown ("Fire1")) {

			rolarDados ();
		
		}




	
	}

	public void rolarDados ()
	{
		corpo.AddForce (Random.onUnitSphere * força, forceMode);
		corpo.AddTorque (Random.onUnitSphere * angularTorque, forceMode);
		StartCoroutine("Checkagem");
		
	}

	IEnumerator Checkagem(){

		for (float timer = 3; timer >= 0; timer -= Time.deltaTime) {
			yield return 0;
		}

		Debug.Log ("OI");
		dadosParados ();


	}



	public bool dadosParados(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.up, out hit, Mathf.Infinity, dieValueColliderLayer)) {	
			currentValue = hit.collider.GetComponent<valorDado> ().valor;
		}
		if (GetComponent<Rigidbody> ().IsSleeping () && !rollComplete) {
			rollComplete = true;
			//currentValue = hit.collider.GetComponent<valorDado>().valor;
		} else if (!GetComponent<Rigidbody> ().IsSleeping ()) {
			rollComplete = false;

		}

		if (rollComplete==true){
			return true;
	}
		return false;
	}}



