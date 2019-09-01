using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour {


    // Use this for initialization

    [SerializeField] private bool AliveFlag;
    
	
	// Update is called once per frame
	void Update () {


        if (AliveFlag)
        {
 


            if (this.transform.position.y > 3)
            {
                //position.y = -2.2f;
                AliveFlag = false;
                Destroy(gameObject);
            }

            this.transform.position += new Vector3(0.0f,0.1f,0.0f);
        }


	}

    public void CreateBalloon()
    {

        AliveFlag = true;
        this.transform.position = new Vector3(UnityEngine.Random.Range(-4.4f,4.4f), -0.75f, 5.0f);
        //position = this.transform.position;

    }

}
