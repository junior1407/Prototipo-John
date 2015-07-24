using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Menu : MonoBehaviour {

	public GameObject textgameobject;
	public string  texto;

	public void onClick(){
		textgameobject.GetComponent<Text> ();
		Text text = textgameobject.GetComponent<Text>();
		//text.text = texto;


		 if (text.text == "1") {
			text.text = "2";
			//textgameobject.tag = "n2";
		}

		else if (text.text == "2") {
			text.text = "3";
			//textgameobject.tag = "n2";
		}

		else if (text.text == "3") {
			text.text = "4";
			//textgameobject.tag = "n2";
		}

		else if (text.text == "4") {
			text.text = "5";
			//textgameobject.tag = "n2";
		}

		else if (text.text == "5") {
			text.text = "6";
			//textgameobject.tag = "n2";
		}

	}


	
	// Update is called once per frame
	void Update () {

	}
}
