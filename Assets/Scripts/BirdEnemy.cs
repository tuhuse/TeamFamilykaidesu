using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {
    //  //��Q���ɓ����������̋���
    private float _speedDown = 80f;


    private void OnCollisionEnter2D(Collision2D collision) {
        //�v���C���[�p
        if (collision.gameObject.layer == 12)//Mucusflog�p
        {
            collision.gameObject.GetComponent<PlayercontrollerScript>().ObstacleCollision(_speedDown);
        } 
        //CPU�p
        if (collision.gameObject.layer == 14)//Mucusflog�p
        {
            collision.gameObject.GetComponent<FrogCpu>().ObstacleCollision(_speedDown);
        }
    }

}


