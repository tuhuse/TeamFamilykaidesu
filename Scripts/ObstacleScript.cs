using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //��Q���ɓ����������̋���
    private float _speedDown = 75f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�p
        if (collision.gameObject.layer == 12)
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
