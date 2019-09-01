using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score2P : MonoBehaviour {

    public int Point2P;

    public GameObject ResultScore = null;//テキストオブジェクト(リザルト)

    // Use this for initialization
    void Start () {

        //ScoreManager.AddMasterScore(10, PlayerMode.Player2);


        Point2P = ScoreManager.Score2P;

	}
	
	// Update is called once per frame
	void Update () {

        Text ScoreText_2P = ResultScore.GetComponent<Text>();

        ScoreText_2P.text = "" + ScoreManager.Score2P.ToString();
	}
}
