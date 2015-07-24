﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoardController : MonoBehaviour {
	public List<Transform> boardPositions = new List<Transform>();
	public GameObject[] players;
	public List<PlayerScript> scriptsplayers = new List<PlayerScript> ();

	public float speed = 10f;
	public int ValorDado;
	public int CurrentPosition;
	Transform CasaAlvo;
	GameObject dado;
	GameObject alvo;
	MostrarValorDado mostrarValorDado;

	public void AdquirePosicoesTabuleiro(){
		for (int i=1; i<66; i++) {
			boardPositions.Add (GameObject.Find ("Casa" + i).transform);
		}
	}

	public void AdquireJogadores(){

		players = GameObject.FindGameObjectsWithTag ("jogador");
		foreach (GameObject atual in players) {
			atual.GetComponent<PlayerScript>().posicao_atual=1;
			scriptsplayers.Add(atual.GetComponent<PlayerScript>());
		}

	}


	void Awake(){
		AdquireJogadores ();

		AdquirePosicoesTabuleiro ();
	}

	public int getDadoValor(){
		dado = GameObject.FindGameObjectWithTag ("dado");
		mostrarValorDado = dado.GetComponent<MostrarValorDado> ();
		return mostrarValorDado.currentValue;
	}

	public void mover(int player, int valor_dado){
		Debug.Log (valor_dado);
		alvo = players [player];
		//alvo.GetComponent<PlayerScript> ().nome = "john sama";
		PlayerScript player_atual = alvo.GetComponent<PlayerScript> ();


		int inicio;
		inicio = player_atual.posicao_atual;
		for (int i=inicio ;i< inicio+valor_dado; i++) {
			Debug.Log (player_atual.posicao_atual);
				player_atual.posicao_atual ++;

			// Eu usei  =  pra receber a posiçao de uma vez.  Mas vc vai querer que va
			// Suavemente, entao vc adaptara pra translate;
			//No caso, ele ta trocando a posicao em todos os sentidos, so quero que ele va ate em cima.
			player_atual.transform.localPosition = boardPositions[i].transform.position;

	
		}
	}


	void Start () {

		       


		}


	void Update () {


		if (Input.GetKeyDown(KeyCode.J)){
			scriptsplayers [0].nome = "johnz-sama";
			
		mover (0, getDadoValor ());
		   
		}


	}
}
