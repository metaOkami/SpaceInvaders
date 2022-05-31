using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D _BulletRb;
    public float bulletSpeed;
    private void Awake() {
        _BulletRb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(this.gameObject.activeInHierarchy){
            _BulletRb.AddForce(new Vector2(0,bulletSpeed*Time.fixedDeltaTime),ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="DeadZone"){
            this.gameObject.SetActive(false);
        }
    }
}
