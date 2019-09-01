using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    //１P
    public static int Score1P { get; private set; }
    //2P
    public static int Score2P { get; private set; }

	// Use this for initialization
	void Start () {
        //ResetMasterScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void AddMasterScore(int add,PlayerMode mode)
    {
        if (mode == PlayerMode.Player1)
        {
            Score1P += add;
        }
        else if(mode == PlayerMode.Player2)
        {
            Score2P += add;
        }
       // Debug.Log(Score1P + "."  + Score2P);
    }
    public static void ResetMasterScore()
    {
        Score1P = 0;
        Score2P = 0;
    }
}
