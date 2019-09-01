using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kuis
{
    public class KuiManager : MonoBehaviour
    {
        [SerializeField] Kui KuiPrefab = null;
        private ObjectPool<Kui> Kuis = null;

        private Respawner Respawner = null;

        [SerializeField] float RespawnPosition_y = 0.0f;

        [SerializeField] float RESPAWN_TIME_MIN = 2.0f;
        [SerializeField] float RESPAWN_TIME_MAX = 5.0f;
        private float RespawnTimer = 1.0f;

        [SerializeField] int RESPAWN_NUM = 10;

        void Awake()
        {
            MakePool();
            Respawner = new Respawner(RespawnPosition_y);
        }
        
        //　Coinのオブジェクトプールを作成
        void MakePool()
        {
            Kuis = new ObjectPool<Kui>(KuiPrefab.gameObject);
            Kuis.CreatePool(RESPAWN_NUM);
        }
        
        void Update()
        {
            if (RespawnTimer > 0.0f) RespawnTimer -= Time.deltaTime;
            else Respawn();
        }

        private void Respawn()
        {
            //Debug.Log("Kui Respawn!!");
            var respawnPostion = Respawner.GetRespawnPosition();
            var kui = Kuis.GetObject();
            if (kui != null)
            {
                kui.Initialize(respawnPostion);
                RespawnTimer = Random.Range(RESPAWN_TIME_MIN, RESPAWN_TIME_MAX);
            }
        }
    }
}
