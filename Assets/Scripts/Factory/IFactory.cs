
using UnityEngine;

public interface IFactory
{
    GameObject FactoryMethod(int index, Transform spawnPoint);
}
