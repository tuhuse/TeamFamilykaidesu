using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnePStart());       
    }

    private IEnumerator OnePStart() {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
