using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMananger : MonoBehaviour
{
    public GameObject[] Spawner;

    public float _maxEnemy;
    public float _enemiesAlive;
    public GameObject enemy;
    GameObject[] enemies;
    public float _totalEnemies;
    public float _totalWaves;
    float _wave;

    
    bool _enemySpawned;
    public GameObject _gateOpen;
    public GameObject _gateClosed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
      if(_enemySpawned && enemies.Length<=0)
        {
            _gateClosed.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gateOpen.SetActive(true);
            foreach (GameObject spawn in Spawner)
            {
                spawn.gameObject.SetActive(true);
            }
            _enemySpawned = true;
        }
        
        
    }
}
