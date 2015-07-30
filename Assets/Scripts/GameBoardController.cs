using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameBoardController : MonoBehaviour
{
	public List<Transform> boardPositions = new List<Transform> ();
	public BaseCasa[] casas = new BaseCasa[65];
	public GameObject[] players;
	public GameObject text;
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
	public Text textinho;
/*	public GameObject pai;
	public GameObject filho;
	BaseCasa teste;*/

	public void AdquirePosicoesTabuleiro ()
	{/*
		for (int i=1; i<66; i++) {

			boardPositions.Add (GameObject.Find ("Casa" + i).transform);
		}*/
		for (int i=1; i<66; i++) {
			Transform datransform = GameObject.Find ("Casa" + i).transform;
			casas [i - 1] = (new CasaComum (datransform, textinho, i));
		}

	}

	public void ProximoPlayer ()
	{
		Debug.Log ("inicio funcao");


		PlayerScript script_atual = player_atual.GetComponent<PlayerScript> ();
		int ordem_player_atual = script_atual.ordem;
		Debug.Log (ordem_player_atual);
		Debug.Log (players.Length - 1);
		if (ordem_player_atual < players.Length - 1) {
			Debug.Log ("chegou ao final");
			player_atual = players [ordem_player_atual + 1];

			
		} else {
			player_atual = players [0];

			//Debug.Log (players.FindIndex (player_atual));
//		players.Indexof ();
			/*	foreach (GameObject player in players) {

		}*/
		}
		Atualizar_Texto ();
	}

	public void Atualizar_Texto ()
	{
	
		//player_atual = player;

		int vez = player_atual.GetComponent<PlayerScript> ().ordem + 1; 
		textinho.text = "Vez do jogador " + vez;
	
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

		GameObject[] temp = GameObject.FindGameObjectsWithTag ("jogador");	
		players = new GameObject[temp.Length];
		foreach (GameObject atual in temp) {
			players [atual.GetComponent<PlayerScript> ().ordem] = atual;
			
			atual.GetComponent<PlayerScript> ().posicao_atual = 1;
			//	scriptsplayers.Add (atual.GetComponent<PlayerScript> ());
		}
		//num_players = scriptsplayers.Count;
		player_atual = players [0];
		Atualizar_Texto ();

		num_players = players.Length;	

	}

	void Awake ()
	{
		textinho = text.GetComponent<Text> ();
		//Debug.Log ("temos "+GameObject.Find("propriedades_jogo").GetComponent<Propriedes>().numero_de_jogadores + " jogadores");
		AdquireJogadores ();

		AdquirePosicoesTabuleiro ();

		InicializaDado ();

		//teste = new BaseCasa (boardPositions [2], 2); 
/*
		pai = GameObject.Find ("Casa2");
		Transform trans_pai = pai.GetComponent<Transform> ();

		foreach (Transform atual in pai.GetComponent<Transform>()) {
			Debug.Log (atual.gameObject.name);
		}*/
	}

	public void InicializaDado ()
	{
		dado = GameObject.FindGameObjectWithTag ("dado");
		scriptDado = dado.GetComponent<Dado> ();
	
	}

	public int getDadoValor ()
	{

		return scriptDado.currentValue;
	}


	IEnumerator moveIndividual(int pos_casa, int ordem_player){
		Debug.Log ("mover individual startoui");
		float tempo_passado = 0;
		float tempo_total = 2;
	//	player_atual.transform.localPosition = casas[pos_casa].getPositionparaPlayer(ordem_player);
		while(tempo_passado<tempo_total){

		player_atual.transform.localPosition = Vector3.Lerp(player_atual.transform.localPosition,casas[pos_casa].getPositionparaPlayer(ordem_player),tempo_passado/tempo_total);
			tempo_passado+=Time.deltaTime;
			yield return 0;
	  }
	}
	public IEnumerator mover (int valor_dado)
	{

		Debug.Log ("mover pega");
		PlayerScript splayer_atual = player_atual.GetComponent<PlayerScript> ();
		int inicio;
		inicio = splayer_atual.posicao_atual;
		for (int i=inicio; i< inicio+valor_dado; i++) {
			splayer_atual.posicao_atual ++;
		//	moveIndividual(i,splayer_atual.ordem);
			yield return StartCoroutine(moveIndividual(i,splayer_atual.ordem));
		//	player_atual.transform.localPosition = casas[i].getPositionparaPlayer(splayer_atual.ordem);
			
		}
		ProximoPlayer ();

	}

	void Start ()
	{

		       


	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			mover(4);
		}
	

		/*
		if (Input.GetKeyDown (KeyCode.F)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(1);
			ProximoPlayer();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(2);ProximoPlayer();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(3);ProximoPlayer();
		}
		if (Input.GetKeyDown (KeyCode.J)) {
		
			player_atual.transform.position = teste.getPositionparaPlayer(4);ProximoPlayer();
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(5);ProximoPlayer();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(0);
		}
		/*if (Input.GetKeyDown (KeyCode.H)) {
			
			player_atual.transform.position = teste.getPositionparaPlayer(0);
		}*/


		
		

	}
}


