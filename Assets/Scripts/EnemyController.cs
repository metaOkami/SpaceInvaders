using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] Points;
    public float speed;
    
    int _numberChoosen;
    private void Start()
    {
        _numberChoosen = Random.Range(0, Points.Length);
    }
    private void Update()
    {
        EnemyMovement();
    }
    private void EnemyMovement(){
        transform.position = Vector2.MoveTowards(transform.position, Points[_numberChoosen].position,speed*Time.deltaTime);
    }

}
