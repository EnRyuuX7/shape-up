using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour {

	public TextMeshProUGUI text;
	// public int highScore;

	public static HighScoreController instance;
	
	private Vector2 vel = Vector2.zero;
	private float speed = 0.05f;
	private float backSpeed = 0.2f;
	private Vector2 maxSize;
	private bool growing;

    
	
	private void Start() {
		instance = this;
		maxSize = new Vector2(1.6f, 1.6f);
	}

	private void Update() {
		text.text = "High Score: " + Score.highScore;

		// if(ScoreController.score > ScoreController.highScore)
		// {
		// 	highScore = ScoreController.score;
        //     growing = true;
		// }
        // else{
        //     growing = false;
        // }
		
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

}
