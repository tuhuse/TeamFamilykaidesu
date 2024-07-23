using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveButtonScript : MonoBehaviour {

    private float _jumpHeight = 200f; // •ú•¨ü‚ÌÅ‘å‚‚³
    private float _jumpDuration = 1f; // •ú•¨ü‚Ì‰^“®‚É‚©‚©‚éŽžŠÔ
    [SerializeField] private AnimationCurve _jumpCurve; // •ú•¨ü‚Ì‚‚³‚ÌƒJ[ƒu
    [SerializeField] private ButtonMethod _multiButtonMethod;
    [SerializeField] private ButtonMethod _soroButtonMethod;
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    private float _buttonMethodY;
    private bool _isCurve;
    private bool _isSelect = true;
    private bool _isButtonSelect = false;
    private bool _isWaitSelect = false;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _jumpTimer;

    [SerializeField] private GameObject _menuImage = default;
    private enum Situation {
        One,
        Every
    }
    private Situation _situation = default;
    void Start() {
        _jumpTimer = 1f;
        _situation = Situation.One;
    }

    void Update() 
    {
        if (Input.GetAxis("L_Stick_Horizontal") > 0f && !_isButtonSelect && !_isWaitSelect) {
            _isButtonSelect = true;
            _isWaitSelect = true;
            if (_situation == Situation.Every) 
            {
                _soroButtonMethod.OnButtonClick();
            } 
            else if (_situation == Situation.One) 
            {
                _multiButtonMethod.OnButtonClick();
            }


        } 
        else if (Input.GetAxis("L_Stick_Horizontal") < 0 && !_isButtonSelect&&!_isWaitSelect) 
        {

            _isButtonSelect = true;
            _isWaitSelect = true;
            if (_situation == Situation.Every) {
                _soroButtonMethod.OnButtonClick();
            } else if (_situation == Situation.One) {
                _multiButtonMethod.OnButtonClick();
            }
        } 
        else if (Input.GetAxis("L_Stick_Horizontal") == 0) 
        {
            _isButtonSelect = false;
        }
        SwSituation();

        if (Input.GetButtonDown("Submit")&&!_isWaitSelect&&!_isButtonSelect) {
            if (_situation == Situation.One) {
               
                _selectCharacterScript.SummonSneak();
                
            }

        }
        SwSituation();
    }

    public void JumpToPosition(Vector3 target) {
        if (_isSelect) {
            _startPosition = transform.position;
            _targetPosition = target;
            _jumpTimer = _jumpDuration;

            _isCurve = true;

        }

    }
    private void SwSituation() {
        switch (_situation) {
            case Situation.One:
                RightSpeed();

                break;
            case Situation.Every:

                LeftSpeed();
                break;
        }

    }
    private IEnumerator Cool() 
    {
        yield return new WaitForSeconds(1f);
        _situation = Situation.Every;
        _isWaitSelect = false;
        ;
    }
    private IEnumerator Cool2() 
    {
        yield return new WaitForSeconds(1f);
        _situation = Situation.One;
        _isWaitSelect = false;
    }

    private void RightSpeed() {
       
        if (_isCurve) 
        {
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _jumpTimer -= Time.deltaTime;
            // •ú•¨ü‚Ì‰^“®
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;
           
        }
        if (_jumpTimer <= 0f) {
            // ˆÚ“®Š®—¹
            StartCoroutine(Cool());
            _isCurve = false;
            _isSelect = true;
            _multiButtonMethod.Landing();

            _jumpTimer = _jumpDuration;
            
        }
    }
    private void LeftSpeed() {
        
        if (_isCurve) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _jumpTimer -= Time.deltaTime;
            // •ú•¨ü‚Ì‰^“®
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;
           
        }
        if (_jumpTimer <= 0f) {
            // ˆÚ“®Š®—¹
            StartCoroutine(Cool2());
            _isCurve = false;
            _isSelect = true;
            _soroButtonMethod.Landing();

            _jumpTimer = _jumpDuration;
           
        }
    }
}
