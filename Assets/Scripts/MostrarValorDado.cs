using UnityEngine;
using System.Collections;

public class MostrarValorDado : MonoBehaviour {

	public LayerMask dieValueColliderLayer = -1;
	public  int currentValue = 1;
	private bool rollComplete = false;

	void Update () 
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up,out hit,Mathf.Infinity, dieValueColliderLayer))
		{
			currentValue = hit.collider.GetComponent<valorDado>().valor;
		}
		if (GetComponent<Rigidbody>().IsSleeping () && !rollComplete) {
			rollComplete = true;
			//currentValue = hit.collider.GetComponent<valorDado>().valor;
		} else if (!GetComponent<Rigidbody>().IsSleeping()) {
			rollComplete = false;
		}
	}
}
