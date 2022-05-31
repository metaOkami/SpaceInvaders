using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawners;
    Transform _choosedSpawn;
    public float secsBetweenSpawns;

    private void Start() {
        int numberOfChoices=spawners.Length;
        int numberChoosen=Random.Range(0,numberOfChoices);
        _choosedSpawn=spawners[numberChoosen];
        StartCoroutine("Spawner");
        
    }

    public IEnumerator Spawner(){
        
        GameObject enemy=ObjectPool.sharedInstance.GetEnemiesPooled();
        if(enemy!=null){
            enemy.SetActive(true);
        }
        yield return new WaitForSeconds(secsBetweenSpawns);
    }

}
