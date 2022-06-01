using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    

    private void Update() {
        PlayerMovement();
        PlayerAttack();
    }
    private void PlayerMovement(){
            Vector3 inputMovement=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*playerSpeed*Time.deltaTime;
            transform.position=new Vector2(inputMovement.x+transform.position.x,inputMovement.y+transform.position.y);
    }
    private void PlayerAttack(){
        if(Input.GetButtonUp("Fire1")){
            GameObject bullet = ObjectPool.sharedInstance.GetBulletsPooled();
            if(bullet!=null){
                bullet.transform.position=transform.position;
                bullet.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="DeathEnemyZone"){
            if(transform.position.x<other.gameObject.transform.position.x){
                transform.position=new Vector2(GameObject.Find("ParedIzquierda").transform.position.x+1,transform.position.y);
            }else{
                transform.position=new Vector2(GameObject.Find("ParedDerecha").transform.position.x-1,transform.position.y);
            }
        }
    }
}


