using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMethod : MonoBehaviour {
    [SerializeField] ParticleSystem _waveParticle;
    [SerializeField] private MoveButtonScript _moveButtonScript; // キャラクターを制御するスクリプト
    [SerializeField] private GameObject _moveCharacter;
    private bool _isCharacterLanding;
    private bool _isUp;
    private Vector2 _buttonPosition;
    [SerializeField] private Transform _syudou;
    private float _thisTransformPositionY = default;
    private float _speed = 500;
    void Start() {
        _waveParticle.Stop();
        _thisTransformPositionY = transform.localPosition.y;
    }
    private void Update() {
        float myposition = this.transform.position.y;
        if (_isCharacterLanding) {
            _waveParticle.Play();
            StartCoroutine(WaveCoolDown());
            this.transform.position += (Vector3.down * _speed * Time.deltaTime);
            _moveCharacter.transform.position += (Vector3.down * _speed * Time.deltaTime);
        }
        if (this.transform.localPosition.y < _thisTransformPositionY - 50f) {
            _isCharacterLanding = false;
            _isUp = true;
        }
        if (_isUp) {
            this.transform.position += Vector3.up * _speed * Time.deltaTime;
            _moveCharacter.transform.position += Vector3.up * _speed * Time.deltaTime;

        } else {

            //    myposition = _syudou.position.y;
        }
        if (myposition >= _syudou.position.y) {

            _isUp = false;
        }

    }
    private IEnumerator WaveCoolDown() {
      yield  return new WaitForSeconds(1f);
        _waveParticle.Stop();
    }
    public void OnButtonClick() {
        // ボタンが押された位置にキャラクターを飛び移らせる
        Vector3 buttonPosition = transform.position;
        _moveButtonScript.JumpToPosition(buttonPosition);
    }
    public void Landing() {
        _isCharacterLanding = true;
    }
}
