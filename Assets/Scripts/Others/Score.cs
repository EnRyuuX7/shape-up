using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour {

	public TextMeshProUGUI text;

    public TextMeshProUGUI hiText;
	public int score;
	public static int highScore;

	public static Score instance;
	
	private Vector2 vel = Vector2.zero;
	private float speed = 0.05f;
	private float backSpeed = 0.2f;
	private Vector2 maxSize;
	private bool growing;
	
	private void Start() {
		score = 0;
        highScore = PlayerPrefs.GetInt ("highScore", highScore);
		hiText.text = "High Score: " + highScore.ToString();
        instance = this;
		maxSize = new Vector2(1.6f, 1.6f);

	}

	private void Update() {
		text.text = "Score: " + score;

		if(score > highScore)
		{
			highScore = score;
            hiText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt ("highScore", highScore);
            
		}
		
		Vector2 newScale;

		if (growing) {
			newScale = Vector2.SmoothDamp(text.transform.localScale, maxSize, ref vel, speed);
			if (newScale.x > maxSize.x - 0.1f)
				growing = false;
		}
		else {
			newScale = Vector2.SmoothDamp(text.transform.localScale, new Vector2(1,1), ref vel, backSpeed);
		}

		text.transform.localScale = newScale;
	}
	
    public static void ResetScore(){
        PlayerPrefs.SetInt ("highScore", 0);

    }
	public static void AddScore(int points) {
		instance.score += points;
		instance.growing = true;
	}
}
