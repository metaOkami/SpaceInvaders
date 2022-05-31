using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    Rigidbody2D _playerRb;

    private void Awake() {
        _playerRb=GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        PlayerMovement();
        PlayerAttack();
    }
    private void PlayerMovement(){
            Vector3 inputMovement=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*playerSpeed*Time.fixedDeltaTime;
            transform.position=new Vector2(inputMovement.x+transform.position.x,inputMovement.y+transform.position.y);
    }
    private void PlayerAttack(){
        if(Input.GetButtonDown("Fire1")){
            GameObject bullet = ObjectPool.sharedInstance.GetPooledObject();
            if(bullet!=null){
                bullet.transform.position=transform.position;
                bullet.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="TransporteParedes"){
            if(transform.position.x<other.gameObject.transform.position.x){
                transform.position=new Vector2(GameObject.Find("ParedIzquierda").transform.position.x+1,transform.position.y);
            }else{
                transform.position=new Vector2(GameObject.Find("ParedDerecha").transform.position.x-1,transform.position.y);
            }
        }
    }
}
