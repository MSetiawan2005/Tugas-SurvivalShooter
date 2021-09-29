using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;
    public Transform spawnPointContainer;
    public List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private MonoBehaviour factory;
    IFactory Factory => factory as IFactory;

    private void Start()
    {
        spawnPoints.AddRange(spawnPointContainer.GetComponentsInChildren<Transform>());
        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }


    private void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        var spawnPointIndex = Random.Range(0, spawnPoints.Count);
        var spawnEnemy = Random.Range(0, 3);

        Factory.FactoryMethod(spawnEnemy, spawnPoints[spawnPointIndex]);
    }
}