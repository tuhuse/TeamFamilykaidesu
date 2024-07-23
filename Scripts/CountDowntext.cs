using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDowntext : MonoBehaviour {
    [SerializeField] private int _countTime;
    [SerializeField] private TextMeshProUGUI _countTimeText;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _startText;
   
   public AudioSource _bgm;
    // Start is called before the first frame update
    void Start() {
        _bgm = GetComponent<AudioSource>();
        _bgm.Stop();
       
    }

    // Update is called once per frame
    void Update() {
       
    }
    public void AnimationCountDown() {
        _countTime--;
        if (_countTime >= 1) {
            _countTimeText.text = "" + _countTime;
            _animator.Play("CountDownText");
        }
 
       else  {
            _startText.SetActive(true);
            _countTimeText.enabled = false;
            //this.gameObject.SetActive(false);
        }
    }
}
