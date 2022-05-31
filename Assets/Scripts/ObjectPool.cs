using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    public List<GameObject> pooledBullets;
    public List<GameObject> pooledEnemies;
    public GameObject Bullets;
    public GameObject Enemies;
    public int BulletsToPool;
    public int EnemiesToPool;

    private void Awake() {
        sharedInstance=this;
    }

    private void Start() {
        pooledBullets=new List<GameObject>();
        GameObject bulletTmp;
        for(int i=0;i<BulletsToPool;i++){
            bulletTmp=Instantiate(Bullets);
            bulletTmp.SetActive(false);
            pooledBullets.Add(bulletTmp);
        }
        GameObject enemieTmp;
        for (int i = 0; i < EnemiesToPool; i++)
        {
            enemieTmp=Instantiate(Enemies);
            enemieTmp.SetActive(false);
            pooledEnemies.Add(enemieTmp);
        }
    }

    public GameObject GetBulletsPooled(){
        for (int i = 0; i < BulletsToPool; i++)
        {
            if(!pooledBullets[i].activeInHierarchy){
                return pooledBullets[i];
            }
        }
        return null;
    }

    public GameObject GetEnemiesPooled(){
        for (int i = 0; i < EnemiesToPool; i++)
        {
            if(!pooledEnemies[i].activeInHierarchy){
                return pooledEnemies[i];
            }
        }
        return null;
    }
}
