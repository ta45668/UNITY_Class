using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


	public Text scoreText;
	public static int score ;

	// Use this for initialization
	void Start () {
		
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "SCORE : " + score.ToString();
	}
}
