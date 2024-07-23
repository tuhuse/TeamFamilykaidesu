using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyScript : MonoBehaviour
{
    public Animator _flyAnimator;

 
    private void Start() {
        _flyAnimator = GetComponent<Animator>();

    }
   
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("CPU")) {

            this.transform.SetParent(collision.gameObject.transform);
            this.gameObject.SetActive(false);

        }
    }

}

