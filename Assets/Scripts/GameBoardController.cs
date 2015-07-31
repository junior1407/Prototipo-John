﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameBoardController : MonoBehaviour
{
	public List<Transform> boardPositions = new List<Transform> ();
	public BaseCasa[] casas = new BaseCasa[65];
	public GameObject[] players;
	public GameObject objectTextoVez;
	public GameObject quadro;
	Text quadroPrincipal;
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
	public Text texto_indicador_vez;

/*	public GameObject pai;
	public GameObject filho;
	BaseCasa teste;*/

	public void AdquirePosicoesTabuleiro ()
	{
		for (int i=1; i<66; i++) {
			Transform datransform = GameObject.Find ("Casa" + i).transform;

			switch (i) {

			case 2:
				casas [i - 1] = (new CasaAvancar (datransform, quadroPrincipal, i, "Pecado! Chame o espírito santo! Avance 3 casas", 3	));
				break;
			
			case 8:
				casas [i - 1] = (new CasaJogarNovamente (datransform, quadroPrincipal, i, "Peça perdao ao irmao e jogue outra vez."));
				break;
			case 14: casas [i - 1] = (new CasaVoltar (datransform, quadroPrincipal, i, "Grite: Meu Deus! Volte uma casa.",1));break;
			case 20: casas [i - 1] = (new CasaVoltar (datransform, quadroPrincipal, i, "Estou com medo. Volte 3 casas.",3));break;
			case 26: casas [i - 1] = (new CasaAvancar (datransform, quadroPrincipal, i, "Sem oração caímos no pecado. Pule 2 casas",2));break;
			case 32:  casas [i - 1] = (new CasaVoltar (datransform, quadroPrincipal, i, "O pecado me deixa sem luz. Volte uma casa",1));break;
			case 38: casas [i - 1] = (new CasaAvancar (datransform, quadroPrincipal, i, "Fujo das tentações. Avance três casas",3));break;
			case 44: casas [i - 1] = (new CasaVoltar (datransform, quadroPrincipal, i, "Não estava vigilante. Volte 3 casas",3));break;
			case 50: casas [i - 1] = (new CasaAvancar (datransform, quadroPrincipal, i, "Socorro jesus! Avance uma casa",1));break; 
			case 56:
				casas [i - 1] = (new CasaVoltar (datransform, quadroPrincipal, i, "O orgulho me faz voltar três casas.",3));
				break;
			 	
			default:
				casas [i - 1] = (new CasaComum (datransform, quadroPrincipal, i));
				break;
			}
		}

			




			




	}

	public void ProximoPlayer ()
	{
		Debug.Log ("Passando de Player");


		PlayerScript script_atual = player_atual.GetComponent<PlayerScript> ();
		int ordem_player_atual = script_atual.ordem;

		if (ordem_player_atual < players.Length - 1) {

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
		texto_indicador_vez.text = "Vez do jogador " + vez;
	
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
		//	quadroPrincipal = GameObject.Find ("textos").GetComponent<Text> ();
		texto_indicador_vez = objectTextoVez.GetComponent<Text> ();
		quadroPrincipal = quadro.GetComponent<Text> ();
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

	IEnumerator moveIndividual (int pos_casa, int ordem_player)
	{
	
		float tempo_passado = 0;
		float tempo_total = 0.1f;
		//	player_atual.transform.localPosition = casas[pos_casa].getPositionparaPlayer(ordem_player);

		while (tempo_passado<tempo_total) {

			player_atual.transform.localPosition = Vector3.Lerp (player_atual.transform.localPosition, casas [pos_casa].getPositionparaPlayer (ordem_player), tempo_passado / tempo_total);
			tempo_passado += Time.deltaTime;
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

			//	moveIndividual(i,splayer_atual.ordem);
			yield return StartCoroutine (moveIndividual (i, splayer_atual.ordem));
			//	player_atual.transform.localPosition = casas[i].getPositionparaPlayer(splayer_atual.ordem);
			splayer_atual.posicao_atual ++;
			if (i + 1 == inicio + valor_dado) {
				//Debug.Log ("last move");
				//Debug.Log (casas[i].mensagem);
				casas [i].Executar ();
				if (casas [i].GetType () == typeof(CasaJogarNovamente)) {
					yield break;
				}
				if (casas [i].GetType () == typeof(CasaAvancar)) {
					CasaAvancar ex = (CasaAvancar)casas [i];
					yield return StartCoroutine (mover_semPassar (ex.intensidade));
				}
				if (casas [i].GetType () == typeof(CasaVoltar)) {
					CasaVoltar ex = (CasaVoltar)casas [i];
					yield return StartCoroutine (mover_pratras(ex.intensidade));
				}
			}
		}
			
	
		ProximoPlayer ();

	}




	public IEnumerator mover_pratras(int valor_dado){

		Debug.Log ("BACK");

		PlayerScript splayer_atual = player_atual.GetComponent<PlayerScript> ();
		Debug.Log ("Pos:" + splayer_atual.posicao_atual);
		int inicio;
		inicio = splayer_atual.posicao_atual;
		for (int i=inicio; i> inicio-valor_dado; i--) {
			Debug.Log ("Ele agora esta na pos"+splayer_atual.posicao_atual);
			splayer_atual.posicao_atual --;
			//	moveIndividual(i,splayer_atual.ordem);
			Debug.Log ("Ele agora ira para pos"+splayer_atual.posicao_atual);
			Debug.Log ("i vale "+i);

		
			yield return StartCoroutine (moveIndividual (i-2, splayer_atual.ordem));
			//	player_atual.transform.localPosition = casas[i].getPositionparaPlayer(splayer_atual.ordem);

			Debug.Log ("Pos:" + splayer_atual.posicao_atual);


		
		}
		

		
	}



	public IEnumerator mover_semPassar (int valor_dado)
	{
		
		Debug.Log ("mover pega");
		PlayerScript splayer_atual = player_atual.GetComponent<PlayerScript> ();
		int inicio;
		inicio = splayer_atual.posicao_atual;
		for (int i=inicio; i< inicio+valor_dado; i++) {
			
			//	moveIndividual(i,splayer_atual.ordem);
			yield return StartCoroutine (moveIndividual (i, splayer_atual.ordem));
			//	player_atual.transform.localPosition = casas[i].getPositionparaPlayer(splayer_atual.ordem);
			splayer_atual.posicao_atual ++;
			if (i + 1 == inicio + valor_dado) {
				//Debug.Log ("last move");
				//Debug.Log (casas[i].mensagem);
				casas [i].Executar ();
				if (casas [i].GetType () == typeof(CasaJogarNovamente)) {
					yield break;
				}
				if (casas [i].GetType () == typeof(CasaAvancar)) {
					CasaAvancar ex = (CasaAvancar)casas [i];
					yield return StartCoroutine (mover (ex.intensidade));
					
					
					
				}
			}
		}
		
		

		
	}

	void Start ()
	{

		       


	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			StartCoroutine(mover (55));
		}


	


		if (Input.GetKeyDown (KeyCode.F)) {
			
			player_atual.transform.localPosition =  casas [6].getPositionparaPlayer (0);
			player_atual.GetComponent<PlayerScript>().posicao_atual=6;
		//	ProximoPlayer();
		}/*
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


