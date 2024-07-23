using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {
    //  //障害物に当たった時の挙動
    private float _speedDown = 80f;


    private void OnCollisionEnter2D(Collision2D collision) {
        //プレイヤー用
        if (collision.gameObject.layer == 12)//Mucusflog用
        {
            collision.gameObject.GetComponent<PlayercontrollerScript>().ObstacleCollision(_speedDown);
        } 
        //CPU用
        if (collision.gameObject.layer == 14)//Mucusflog用
        {
            collision.gameObject.GetComponent<FrogCpu>().ObstacleCollision(_speedDown);
        }
    }

}


