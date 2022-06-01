using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawners;
    Transform _choosedSpawn;
    public float secsBetweenSpawns;

    private void Start()
    {
        StartCoroutine("Spawner");
    }

    public IEnumerator Spawner(){
        while (Time.timeScale == 1)
        {
            int numberOfChoices = spawners.Length;
            int numberChoosen = Random.Range(0, numberOfChoices);
            _choosedSpawn = spawners[numberChoosen];

            GameObject enemy = ObjectPool.sharedInstance.GetEnemiesPooled();
            if (enemy != null && !enemy.activeInHierarchy)
            {
                enemy.transform.position = _choosedSpawn.position;
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(secsBetweenSpawns);
        }
    }

}
