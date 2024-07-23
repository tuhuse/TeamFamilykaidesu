using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour {
    private float _shakeMagnitude = 1f;// �U���̋����𐧌䂷��p�����[�^
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
            // �����_���ȐU���𐶐�
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;
            _redColor.enabled = true;
            // �U�����J�����ɓK�p
            transform.localPosition += new Vector3(x, y, 0f);
            
        } 
        else if (!_isCameraShake&&_isShake) 
        {
            _isShake = false;
            // �U�����I�������猳�̈ʒu�ɖ߂�
            transform.localPosition = _originalPosition;
            _redColor.enabled = false;
        }
    }

    // �U�����J�n���郁�\�b�h
    public void StartCameraShake() {
        _isCameraShake = true;
    }
    public void StopCameraShake() {
        _isCameraShake = false;
    }
   
  
}
