﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text scoreText = GetComponent<Text> ();
		scoreText.text = "Congratulations! \n\n Score: " + ScoreKeeper.totalScore.ToString ();
		ScoreKeeper.Reset ();
	}

}