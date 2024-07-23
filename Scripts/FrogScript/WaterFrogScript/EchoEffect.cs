using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    //残像の発生時間
    private float _timeSpawns;
    //残像の発生時間間隔
    [SerializeField] private float _startTimeSpawns;
    //残像を発生するオブジェクト
    [SerializeField] GameObject _echoObj;

    // Update is called once per frame
    void Update()
    {
        if(_timeSpawns <= 0)
        {
            Instantiate(_echoObj, transform.position, Quaternion.identity);
            _timeSpawns = _startTimeSpawns;
        }
        else
        {
            _timeSpawns -= Time.deltaTime;
        }
    }
}
