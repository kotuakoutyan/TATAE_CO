using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coins
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] Coin CoinPrefab = null;
        private ObjectPool<Coin> Coins = null;

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
        void Start()
        {
            for (int i = 0; i < 5; i++) Respawn();
        }

        //　Coinのオブジェクトプールを作成
        void MakePool()
        {
            Coins = new ObjectPool<Coin>(CoinPrefab.gameObject);
            Coins.CreatePool(RESPAWN_NUM);
        }
        
        void Update()
        {
            if (RespawnTimer > 0.0f) RespawnTimer -= Time.deltaTime;
            else
            {
                Respawn();
                // Drop(Vector3.zero, 5);
            }
        }

        private void Respawn()
        {
            var respawnPostion = Respawner.GetRespawnPosition();
            var coin = Coins.GetObject();
            if (coin != null)
            {
                coin.Initialize(respawnPostion);
                RespawnTimer = Random.Range(RESPAWN_TIME_MIN, RESPAWN_TIME_MAX);
            }
        }

        public void Drop(Vector3 poistion, int coinNum)
        {
            for(int i = 0;i < coinNum; i++)
            {
                var coin = Coins.GetObject();
                if (coin != null) coin.Drop(poistion);
            }
        }
    }
}
