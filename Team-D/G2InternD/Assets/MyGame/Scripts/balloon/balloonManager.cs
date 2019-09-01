using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloonManager : MonoBehaviour {

    public balloon balloon_blue;
    public balloon balloon_red;
    
    public void CreateBaloon(PlayerMode mode)
    {
        if(mode == PlayerMode.Player1)
        {
            Instantiate(balloon_blue).CreateBalloon();
        }
        else
        {
            Instantiate(balloon_red).CreateBalloon();
        }
    }
}
