using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    [Header("分數顯示物件")]
    public Text scoreText;
    [Header("分數計算變數")]
    public int score;
    [Header("怪物血量變數")]
    public int monsterHP;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Score" + score.ToString();//更新所得分數
	}
}
