using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerScr : MonoBehaviour
{
    public float timeToSpawn = 4;
    int spawnCount = 0;

    public GameObject enemyPref, wayPointParent;
    
    void Update()
    {
        if(timeToSpawn <= 0)
        {
            StartCoroutine(SpawnEnemy(spawnCount + 1));
            timeToSpawn = 4;
        }
        timeToSpawn -= Time.deltaTime;

        GameObject.Find("SpawnCountTxt").GetComponent<Text>().text = Mathf.Round(timeToSpawn).ToString();
    }
    IEnumerator SpawnEnemy (int enemyCount)
    {
        spawnCount++;
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject tmpEnemy = Instantiate(enemyPref);
            tmpEnemy.transform.SetParent(gameObject.transform, false);
            tmpEnemy.GetComponent<EnemyScr>().wayPointsParent = wayPointParent;

            yield return new WaitForSeconds(0.3f);

        }
    }
}
