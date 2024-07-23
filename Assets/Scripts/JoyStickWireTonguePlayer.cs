using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JoyStickWireTonguePlayer : MonoBehaviour
{
    private bool _isExtension = false;
    private bool _isAttack = false;

    private bool _isFrogCatch = false;
    private bool _isJustOnes = false;
    private bool _underAttack = default;

    private Vector3 _playerposition = default;

    private float _tongueScaleY = default;

    private const float TIMEDELTATIME = 1000f;
    private const float PLUSSCALESPEEDY = 0.15f;
    private const float FAILEDTONGUECATCH = 1f;
    private const float SUCCESSTONGUECATCH = 8f;

    private const float TONGUESCALEX = 3f;
    private const float TONGUESCALEY = 0.01f;
    private const float MAXTANGUEEXTENSION = 10f;

    [Header("Player")] [SerializeField] private GameObject _player = default;

    private Rigidbody2D _playerRB = default;

    // Update is called once per frame
    private void Start() {
        _playerRB = GetComponentInParent<Rigidbody2D>();
        _tongueScaleY = this.transform.localScale.y;
    }
    void Update() {
        float vartical = Input.GetAxis("R_Stick_Vartical");
        float horizontal = Input.GetAxis("R_Stick_Horizontal");

        float degree = Mathf.Atan2(horizontal, -vartical) * Mathf.Rad2Deg;

        if (degree < 0) {
            degree += 360;
        }

        if (!_isAttack) {
            this.transform.localRotation = Quaternion.Euler(0, 0, -degree);
        }



        if (_isExtension) {
            //ベロを伸ばす
            this.transform.localScale += new Vector3(0, 0.05f, 0) * Time.deltaTime * TIMEDELTATIME;

        }
        //ベロが壁に当たらなかったら
        else if (!_isFrogCatch) {
            //ベロの縮小
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * TIMEDELTATIME;
            } else if (_isAttack && !_isJustOnes) {
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                _isJustOnes = true;
                StartCoroutine(Failed());
            }


        } else {
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * TIMEDELTATIME;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                StartCoroutine(Success());
            }
        }

        if (this.transform.localScale.y > MAXTANGUEEXTENSION) {
            //ベロを縮め始める
            _isExtension = false;
        }





        if (vartical <= 0.1f && horizontal >= 0.1f) {
            //ベロを伸ばし始める
            if (!_isAttack) {
                _isAttack = true;
                _isExtension = true;
                _underAttack = true;
            }

        }


    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Handle" && _isAttack) {
            ControllScriptOff();
            _isExtension = false;
        }
        if (collision.gameObject.CompareTag("Flor")) {
            _isExtension = false;
        }

        if (collision.gameObject.tag == "CPU" && _underAttack) {
            _playerposition = _player.transform.position;
            _player.transform.position = collision.gameObject.transform.position;
            collision.gameObject.transform.position = _playerposition;
            _isExtension = false;
            _isFrogCatch = true;
        }
    }

    private void ControllScriptOff() {
        //コントロールスクリプトの一時停止
        if (_player != null) {
            GetComponentInParent<PlayercontrollerScript>().enabled = false;
        }


        //カエルの一時停止
        _playerRB.velocity = Vector2.zero;
    }

    private void ControllScriptOn() {
        //コントロールスクリプトの復活
        if (_player != null) {
            GetComponentInParent<PlayercontrollerScript>().enabled = true;
        }

    }

    private IEnumerator Failed() {
        yield return new WaitForSeconds(FAILEDTONGUECATCH);
        _isAttack = false;
        _isJustOnes = false;

    }

    private IEnumerator Success() {
        yield return new WaitForSeconds(SUCCESSTONGUECATCH);
        _isAttack = false;
        _isJustOnes = false;
        _isFrogCatch = false;
    }
}
