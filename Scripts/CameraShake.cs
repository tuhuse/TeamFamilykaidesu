using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour {
    private float _shakeMagnitude = 1f;// 振動の強さを制御するパラメータ
    private bool _isCameraShake = false;
    private bool _isShake = false;
    private Vector3 _originalPosition;
    [SerializeField] private Image _redColor;
    // Start is called before the first frame update
    void Start() {
        _originalPosition = transform.localPosition;
    }

    void Update() {
        if (_isCameraShake) 
        {
            _isShake=true;
            // ランダムな振動を生成
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;
            _redColor.enabled = true;
            // 振動をカメラに適用
            transform.localPosition += new Vector3(x, y, 0f);
            
        } 
        else if (!_isCameraShake&&_isShake) 
        {
            _isShake = false;
            // 振動が終了したら元の位置に戻す
            transform.localPosition = _originalPosition;
            _redColor.enabled = false;
        }
    }

    // 振動を開始するメソッド
    public void StartCameraShake() {
        _isCameraShake = true;
    }
    public void StopCameraShake() {
        _isCameraShake = false;
    }
   
  
}
