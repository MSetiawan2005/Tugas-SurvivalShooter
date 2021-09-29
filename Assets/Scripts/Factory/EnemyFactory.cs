using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    public GameObject[] enemyPrefabs;
    
    // TODO: implement random spawn point
    public GameObject FactoryMethod(int index, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoint);
        return enemy;
    }
}
