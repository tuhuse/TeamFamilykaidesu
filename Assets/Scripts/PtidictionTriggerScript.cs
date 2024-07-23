using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtidictionTriggerScript : MonoBehaviour
{
    [SerializeField]private GameObject _cpu1;
    [SerializeField]private GameObject _cpu2;
    [SerializeField]private GameObject _cpu3;
  
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
  
    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.layer==9 &&
            !_cpu1.GetComponent<FrogCpu>()._isBehindTrigger) {
            _cpu1.GetComponent<FrogCpu>()._isBehindTrigger = true;
        } 
        else if (collision.gameObject.layer == 9 &&
                !_cpu2.GetComponent<FrogCpu>()._isBehindTrigger) {
            _cpu2.GetComponent<FrogCpu>()._isBehindTrigger = true;
        }
        else if (collision.gameObject.layer == 9 &&
                !_cpu3.GetComponent<FrogCpu>()._isBehindTrigger) {
           _cpu3.GetComponent<FrogCpu>()._isBehindTrigger = true;
        } 
       


    }

}

