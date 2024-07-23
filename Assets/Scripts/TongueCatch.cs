using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCatch : MonoBehaviour
{
    private float _timeCount = 0;
    private const float TIMEMAX = 3;
    private bool _isTimeCount = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.localScale = Vector3.one;
        if (_isTimeCount) {
            _timeCount += Time.deltaTime;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Fly")) {
            collision.gameObject.transform.SetParent(transform, true);
            collision.gameObject.GetComponent<FlyScript>()._flyAnimator.SetBool("Stop",true);
            _isTimeCount = true;
            if (_timeCount > TIMEMAX) {
                collision.gameObject.SetActive(false);
                _isTimeCount = false;
                _timeCount = 0;
            }

        }
       

    }


}
