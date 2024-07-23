using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPLAY : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audio;
    private AudioSource _audioSource;
    [SerializeField]
    private ClearMan _clear;
    private bool _isPlay;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audio;
    }

    // Update is called once per frame
    void Update() {
        if (_isPlay) {
            if (_clear._switchNumber == 4) {
                _audioSource.Play();
                _isPlay = false;
            }
            
        }
       

      
    }
    
    

}
