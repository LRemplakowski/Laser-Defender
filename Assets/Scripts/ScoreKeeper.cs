using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int totalScore;
	Text scoreText;
	
	void Start () {
		scoreText = GetComponent<Text>();
		scoreText.text = "Score: 0";
	}
	
	public void Score (int points) {
		totalScore += points;
		scoreText.text = "Score: " + totalScore.ToString();
	}
	
	public static void Reset () {
		totalScore = 0;
	}
}
