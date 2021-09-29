using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    public GameObject[] enemyPrefabs;
    
   //random spawn point
    public GameObject FactoryMethod(int index, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoint);
        return enemy;
    }
}
