using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;


public class Game : MonoBehaviour {
		
	//Player prefab
	public GameObject player;

	public GameObject spikes;
	public GameObject DeathMenuUI;

	public static GameObject currentPlayer;
	public static Player currentPlayerScript;

	public static int health = 100;

	public static bool inGame;

	//public PostProcessingProfile ppProfile;

	private static Game instance;
	
	private float newMoveSpeed;
	private float newMaxSpeed;
	private float newJumpForce;
	private int   newJumps;


	public static bool abilityAwake;

	private Vector3 Pos;
	private Quaternion Ang;

	public GameObject abilityStatus;


	void Start () {
		abilityAwake = true;
		instance = this;
		//EnterLobby();
		StartGame();
	}

	public void StartGame() {
		inGame = true;
		Time.timeScale = 1f;	
		
		GameObject newPlayer = Instantiate(player, transform.position, transform.rotation);
		newPlayer.name = "Player";
		currentPlayer = newPlayer;
		currentPlayerScript = currentPlayer.GetComponent(typeof(Player)) as Player;


		Invoke("controls",10f);
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);

	}

	private void Update() {
		
		if(currentPlayer != null){
			if(abilityAwake == true)
			{

			}
			Pos = Game.currentPlayer.transform.position;
			Ang = Game.currentPlayer.transform.rotation;
		}
		

		
		if (Game.currentPlayer == null ) {
			Invoke("GameOver", 1f);
			inGame = false;
		}

		if (currentPlayer != null && !DeathMenuUI.activeInHierarchy &&! pauseMenu.GameIsPaused)
		{
			Invoke("Scored", 0f);
		
		}
		
		

		if (Input.GetKeyDown(KeyCode.U)){
			if(abilityAwake && Game.currentPlayer != null)    
			{
				GameObject Spikes = Instantiate(spikes, Pos, Ang);
				Spikes.transform.parent = GameObject.Find("Player").transform;
				abilityAwake = false;
				Invoke("Timer", 5f);
			}
		}
	

		
	}

	protected void Timer(){
		GameObject Spikes = GameObject.FindGameObjectWithTag("Ability");
		if(Spikes != null)
		{
			Destroy(Spikes);
			Invoke("Cooldown", 10f);
		}
		
	}

	protected void controls(){
		GameObject Control = GameObject.FindGameObjectWithTag("ControlText");
		Destroy(Control);
	}

	protected void Cooldown(){
		abilityAwake = true;
	}


	protected void Scored(){
		Score.AddScore(1);
	}

	public void GameOver(){
		DeathMenuUI.SetActive(true);
	}

	public void EnterLobby() {		
		health = 100;
		inGame = false;
		MapGenerator.instance.CleanMap();
		// EnemyController.instance.ClearEnemies();
		//Camera.main.orthographicSize = 2;
	}
	


}
