using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //障害物に当たった時の挙動
    private float _speedDown = 75f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤー用
        if (collision.gameObject.layer == 12)
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
