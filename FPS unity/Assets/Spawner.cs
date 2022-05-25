using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public int _enemiesToSpawn;
    // Start is called before the first frame update
    int i;

    private void Start()
    {
        for ( i = 1; i < _enemiesToSpawn; i++)
        {
            Instantiate(enemy, transform);
        }
        

    }


}
