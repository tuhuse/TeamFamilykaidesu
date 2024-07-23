using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WireTonguePlayer : MonoBehaviour
{
    private Rigidbody2D _playerRB = default;
    [SerializeField] private Camera _camera = default;

    //それぞれ親オブジェクトのカエルのみを入れる
    [SerializeField, Header("Player")] private GameObject _player = default;

    public GameObject _handlePoint;

    //private Vector3 _mousePosition = default;　//クリックしたポジション
    private Vector3 _triggerPosition = default;
    private Vector3 _wrapAround = default;//掴まった場所まで跳んでいく

    private float _tongueScaleY = default; //ベロの初期スケール(X軸)

    private const float PLUSSCALESPEEDY = 0.15f;
    private const float TONGUEMAXEXTENSION = 10f;
    private const float JUMPMULTIPLE = 1.5f;

    private const float FAILEDTONGUECATCH = 1f;
    private const float SUCCESSTONGUECATCH = 8f;

    private const float TONGUESCALEX = 3f;
    private const float TONGUESCALEY = 0.01f;

    private const float DELTATIMESPEED = 1000f;

    public bool _isExtension = false; //ベロの拡大
    private bool _isCatch = false; //ベロが何かしらに掴まる
    private bool _isShrink = false; //ベロの縮小
    private bool _isAttack = false; //攻撃しているかしていないか
    private bool _isJustOnes = false;
    private bool _isFrogCatch = false;
    private bool _underAttack = false;

    
    // Start is called before the first frame update
    void Start() {
        _playerRB = GetComponentInParent<Rigidbody2D>();
        _tongueScaleY = this.transform.localScale.y;
    }

    // Update is called once per frame
    void Update() {
        //ベロの拡大
        if (_isExtension) {
            this.transform.localScale += new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMESPEED;
        }
        //ベロがカエルに当たらなかったら
        else if (!_isFrogCatch) {
            _underAttack = false;
            //ベロの縮小
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMESPEED;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                StartCoroutine(Failed());
            }
        } 
        else 
        {
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMESPEED;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                StartCoroutine(Success());
            }
        }


        //ベロが最大まで伸びたら縮める
        if (this.transform.localScale.y >= TONGUEMAXEXTENSION) {
            _isExtension = false;
        }

        if (_isShrink) {
            if (this.transform.localScale.y > _tongueScaleY) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMESPEED;
            } else {
                ControllScriptOn();
                _isShrink = false;
            }
        }



        if (Input.GetMouseButtonDown(0) && !_isAttack) {
            if (_handlePoint != null) {
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, _handlePoint.transform.position - this.transform.position);
            }
            _isAttack = true;
            _underAttack = true;
            //クリックしたポジションにベロを向ける
            //_mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            //_mousePosition.z += 10;
            //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, _mousePosition - this.transform.position);

            //拡大開始
            _isExtension = true;
        }
        //else if (_isExtension && Input.GetMouseButtonUp(0)) 
        //{
        //    _isExtension = false;
        //}

        if (_handlePoint != null) {
            if (_handlePoint.transform.position.x <= this.transform.position.x) {
                _handlePoint = null;
                this.transform.rotation = Quaternion.Euler(0, 0, -90);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Handle" && _isAttack) {
            ControllScriptOff();
            _isExtension = false;
            _isCatch = true;
            _triggerPosition = collision.ClosestPoint(this.transform.position);
        }
        if (collision.gameObject.CompareTag("Flor") && !_isAttack) {
            _isExtension = false;
        }
        if (collision.gameObject.tag == "CPU" && _underAttack) 
        {
            collision.gameObject.GetComponent<FrogCpu>().PositionChange(_player, collision.gameObject);
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
        _isCatch = false;
        _isFrogCatch = false;
    }

    public void GetPointer(GameObject handleobject) {
        _handlePoint = handleobject;
    }
}
