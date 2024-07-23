using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour
{
    private float _moveIncreased = 0;
    private float _moveValue = 0.025f;
    private float _startWaitProgress = 3.5f;
    private float _waitProgress = 10f;
    private bool _move = false;
    private int _movenumber = default;

   

    private const float TIMEDELTTIME = 500;
    private const int MAXMOVENUBBER = 4;
    private const float MAXMOVEVALUE = 25f;
    private void Update() 
    {
        //i‚ŞH’ö‚ğ‚S‰ñŒJ‚è•Ô‚·
        if (_move && _movenumber <= MAXMOVENUBBER) 
        {
            if (_moveIncreased <= MAXMOVEVALUE) 
            {
                this.transform.position += new Vector3(_moveValue, 0, 0) * Time.deltaTime*TIMEDELTTIME;
                _moveIncreased += _moveValue*Time.deltaTime*TIMEDELTTIME;
            } 
            else 
            {
                _moveIncreased = 0;
                _move = false;
                StartCoroutine(Progress());
                _movenumber++;
            }
        }
        
    }
    public IEnumerator StartProgress() 
    {
        //ˆê”Ô‰‚ß‚Í­‚µ‘‚¢ŠÔ‚Å‘O‚Öi‚Ş
        yield return new WaitForSeconds(_startWaitProgress);
        {
            _move = true;
        }
    }

    private IEnumerator Progress() 
    {
        //‚P‚O•b‚²‚Æ‚É‘O‚Öi‚Ş‚ÌŒJ‚è•Ô‚µ
        yield return new WaitForSeconds(_waitProgress);
        {
            _move = true;
        }
    }
}
