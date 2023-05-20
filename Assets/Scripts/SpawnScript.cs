using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    float timeToSpawn;


    void Update()
    {
        timeToSpawn += Time.deltaTime;
        if (timeToSpawn >= 2.5f)
        {
            var randomSpawn = Random.Range(0, enemyPrefabs.Length);
            var Clone = Instantiate(enemyPrefabs[randomSpawn], transform.position, enemyPrefabs[randomSpawn].transform.rotation);
            Destroy(Clone, 3f);
            timeToSpawn = 0;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        ScoreSystem.score = 0;
    }
}
