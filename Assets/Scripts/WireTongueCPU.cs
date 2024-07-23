using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTongueCPU : MonoBehaviour {
  


    public bool _isExtension = false;
    private bool _isAttack = false;
    public bool _isCoolDown = true;
    private bool _isJudge = false;
    private bool _isTime = false;
    public bool _underAttack = false;
    private bool _isFrogCatch = false;
    private bool _isJustOnes = false;

    //それぞれ親オブジェクトのカエルのみを入れる
    [Header("CPU1")] [SerializeField] private GameObject _enemy1 = default;
    [Header("CPU2")] [SerializeField] private GameObject _enemy2 = default;
    [Header("CPU3")] [SerializeField] private GameObject _enemy3 = default;

    [SerializeField] private GameObject _mySelf = default;


    private Rigidbody2D _cpuRB = default;

    private float _tongueScaleY = default;

    private const float DELTATIMEMULTIPLE = 1000f;
    private const float PLUSSCALESPEEDY = 0.1f;
    private const float TONGUEMAXEXTENSION = 15f;
    private const float TONGUESCALEX = 3f;
    private const float TONGUESCALEY = 0.01f;
    // Start is called before the first frame update
    void Start() {
        _tongueScaleY = this.transform.localScale.y;
        _cpuRB = GetComponentInParent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

        if (_isCoolDown && !_isTime) {
            _isTime = true;
            Judge();
        }
        if (_isExtension) {
            this.transform.localScale += new Vector3(0, PLUSSCALESPEEDY) * Time.deltaTime * DELTATIMEMULTIPLE;
            //_isCoolDown = true;

        } else if (!_isFrogCatch) {
            if (this.transform.localScale.y > _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMEMULTIPLE;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(3, 0.1f, 1);
            }
        } else {
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * DELTATIMEMULTIPLE;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);

                Judge();
            }
        }

        if (this.transform.localScale.y > TONGUEMAXEXTENSION) {
            _isExtension = false;

        }

    }
    private void Judge() {

        if (_isJudge) {
            StartCoroutine(LongCoolDown());
            _isJudge = false;
        } else {
            StartCoroutine(CoolDown());
        }
    }
    private IEnumerator CoolDown() {

        yield return new WaitForSeconds(8);
        _isCoolDown = false;
        _isTime = false;
    }
    private IEnumerator LongCoolDown() {

        yield return new WaitForSeconds(8);
        _isCoolDown = false;
        _isTime = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flor")) {
            _isExtension = false;

        }
        if (collision.gameObject.tag == "Player" && _underAttack) {
            collision.gameObject.GetComponent<PlayercontrollerScript>().PositionChange(_mySelf, collision.gameObject);
            _isExtension = false;
            _isFrogCatch = true;
            _isJudge = true;
        }

        if (collision.gameObject.tag == "CPU" && _underAttack) {
            collision.gameObject.GetComponent<FrogCpu>().PositionChange(_mySelf, collision.gameObject);
            _isExtension = false;
            _isFrogCatch = true;
            _isJudge = true;
        }
    }
}
