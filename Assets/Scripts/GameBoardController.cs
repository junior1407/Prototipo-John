using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameBoardController : MonoBehaviour
{
	public List<Transform> boardPositions = new List<Transform> ();
	public GameObject[] players;
	public List<PlayerScript> scriptsplayers = new List<PlayerScript> ();
	public float speed = 10f;
	public GameObject player_atual;
	public int ValorDado;
	public int CurrentPosition;
	Transform CasaAlvo;
	 GameObject dado;
	GameObject alvo;
	Dado scriptDado;
	public int num_players;

	public void AdquirePosicoesTabuleiro ()
	{
		for (int i=1; i<66; i++) {
			boardPositions.Add (GameObject.Find ("Casa" + i).transform);
		}
	}

	public void ProximoPlayer(){
		Debug.Log ("inicio funcao");


		PlayerScript script_atual = player_atual.GetComponent<PlayerScript> ();
		int ordem_player_atual = script_atual.ordem;
		Debug.Log (ordem_player_atual);
		Debug.Log (players.Length-1);
		if (ordem_player_atual < players.Length-1) {
			Debug.Log ("chegou ao final");
			player_atual = players [ordem_player_atual + 1];
			
		} else {
			player_atual = players [0];

			//Debug.Log (players.FindIndex (player_atual));
//		players.Indexof ();
			/*	foreach (GameObject player in players) {

		}*/
		}
	}
	public void AdquireJogadores ()
	{

		/*
		players= GameObject.FindGameObjectsWithTag ("jogador");

		foreach (GameObject atual in players) {
				
			atual.GetComponent<PlayerScript> ().posicao_atual = 2;
			scriptsplayers.Add (atual.GetComponent<PlayerScript> ());
		}
		num_players = scriptsplayers.Count;
		player_atual = players [0];
		*/

		GameObject[] temp= GameObject.FindGameObjectsWithTag ("jogador");	
		players = new GameObject[temp.Length];
		foreach (GameObject atual in temp) {
			players[atual.GetComponent<PlayerScript>().ordem] = atual;
			
			atual.GetComponent<PlayerScript> ().posicao_atual = 2;
		//	scriptsplayers.Add (atual.GetComponent<PlayerScript> ());
		}
		//num_players = scriptsplayers.Count;
		player_atual = players [0];
		num_players = players.Length;	

	}

	void Awake ()
	{
		//Debug.Log ("temos "+GameObject.Find("propriedades_jogo").GetComponent<Propriedes>().numero_de_jogadores + " jogadores");
		AdquireJogadores ();

		AdquirePosicoesTabuleiro ();

		InicializaDado ();
	}

	public void InicializaDado(){
		dado = GameObject.FindGameObjectWithTag ("dado");
		scriptDado = dado.GetComponent<Dado> ();
	
	}

	public int getDadoValor ()
	{

		return scriptDado.currentValue;
	}

	public void mover (int valor_dado)
	{
			

		//alvo.GetComponent<PlayerScript> ().nome = "john sama";
		PlayerScript splayer_atual = player_atual.GetComponent<PlayerScript> ();


		int inicio;
		inicio = splayer_atual.posicao_atual;
		for (int i=inicio; i< inicio+valor_dado; i++) {
			splayer_atual.posicao_atual ++;

			// Eu usei  =  pra receber a posiçao de uma vez.  Mas vc vai querer que va
			// Suavemente, entao vc adaptara pra translate;
			//No caso, ele ta trocando a posicao em todos os sentidos, so quero que ele va ate em cima.
			player_atual.transform.localPosition = boardPositions [i].transform.position;
		}
			ProximoPlayer();
		
	}

	void Start ()
	{

		       


	}

	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.J)) {
		
			ProximoPlayer();
		}



		

	}}


