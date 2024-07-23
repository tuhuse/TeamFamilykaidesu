using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlysParentInstantiate : MonoBehaviour
{[SerializeField]
    private GameObject _flys;
    private bool _instantiate=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        int flycount = this.transform.childCount;
        if (flycount == 0&&_instantiate) {
            StartCoroutine(Instantiate());
        } 
       

    }
    private IEnumerator Instantiate() {
        _instantiate = false;
         yield return new WaitForSeconds(0.7f);    
            Instantiate(_flys, this.transform.position, Quaternion.identity, this.transform);
        yield return new WaitForSeconds(0.5f);
        _instantiate = true;
       
    }
}
