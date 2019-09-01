using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public int Point1P;

    public bool Win, Lose;

    public GameObject ResultScore = null;//テキストオブジェクト(リザルト)

    // Use this for initialization
    void Start ()
    {

        //ScoreManager.AddMasterScore(10, PlayerMode.Player1);

        Point1P = ScoreManager.Score1P;
        //ScoreManager.AddMasterScore(3, PlayerMode.Player2);
	}
	
	// Update is called once per frame
	void Update () {

        Text ScoreText_1P = ResultScore.GetComponent<Text>();

        ScoreText_1P.text = "" + Point1P.ToString();

        

    }
}
