using UnityEngine;
using System.Collections;

public class ComecaJogo : MonoBehaviour {

	bool comecaJogo = false;
	public GameObject botaoPlay;
	void Start () {
	
	}
	
	public void comecaJoguinho()
	{
		comecaJogo = true;
		if (comecaJogo == true) {
			Application.LoadLevel ("asda");
		}
		
	}
}
