using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        InvokeRepeating("Spawn", 20f, 10f);
    }

    private void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
