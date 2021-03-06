using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{
    
    public Transform[] Points;
    public float speed;
    int _numberChoosen;
    
    public GameObject EnemyGameobject;
    
    GameObject _ParedIzquierda;
    GameObject _ParedDerecha;
    GameObject _ParedBaja;

    GameObject Texto;
    ObserverPattern.Subject subject=new ObserverPattern.Subject();
    
    private void Awake()
    {
        
        
        _ParedBaja = GameObject.Find("SueloParaEnemigos");
        _ParedDerecha = GameObject.Find("ParedDerecha");
        _ParedIzquierda = GameObject.Find("ParedIzquierda");
        
    }
    private void Start()
    {
        Texto = GameObject.Find("ScoreText");
        ObserverPattern.Score score = new ObserverPattern.Score(Texto, new ObserverPattern.OneHit());
        subject.AddObserver(score);
        _numberChoosen = Random.Range(0, Points.Length);
    }
    private void Update()
    {
        EnemyMovement();
    }
    private void EnemyMovement(){
        transform.position = Vector2.MoveTowards(transform.position, Points[_numberChoosen].position,speed*Time.deltaTime);
        if (transform.position.x < _ParedIzquierda.transform.position.x)
        {
            EnemyGameobject.SetActive(false);
        }else if (transform.position.x > _ParedDerecha.transform.position.x)
        {
            EnemyGameobject.SetActive(false);
        }else if (transform.position.y < _ParedBaja.transform.position.y)
        {
            EnemyGameobject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            subject.Notify();
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
