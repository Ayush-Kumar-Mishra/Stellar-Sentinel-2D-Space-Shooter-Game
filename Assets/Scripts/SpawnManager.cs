using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnTripleShotPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7f, 7f), 8, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newTripleShot = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6,12));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
