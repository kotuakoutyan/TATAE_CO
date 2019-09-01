using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner
{
    private float RayStartPosition = 10f;

    private float RespawnPosition_y = 0.0f;

    private Vector3 GroundCenter;
    private Vector3 GroundSize;

    public Respawner(float respawnPosition_y)
    {
        RespawnPosition_y = respawnPosition_y;
        GetGround();
    }

    //　地面を取得して、その大きさと中心点を取得する
    void GetGround()
    {
        var ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Renderer>().bounds;
        GroundCenter = ground.center;
        GroundSize = new Vector3(ground.size.x, RayStartPosition, ground.size.z);
    }

    //　地面の大きさ・中心点に応じてランダムに座標を生成する
    public Vector3 GetRespawnPosition()
    {
        var respawnPosition = GroundCenter
            + new Vector3(GroundSize.x * Random.Range(-1f, 1f), GroundSize.y, GroundSize.z * Random.Range(-1f, 1f));

        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(respawnPosition, Vector3.down), out hit, RayStartPosition * 2) && hit.collider.gameObject.tag == "Ground")
        {
            return hit.point + new Vector3(0, RespawnPosition_y, 0);
        }
        else return GetRespawnPosition();
    }
}
