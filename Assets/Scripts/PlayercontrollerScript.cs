using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrollerScript : MonoBehaviour {
    [SerializeField] private GameObject _downEffect;
    [SerializeField] private GameObject _tongue;
    [SerializeField] private GameObject _pruduction;
    [SerializeField] private GameObject _enemyEffect;
    [SerializeField] private GameObject _itemIcon = default;
    [SerializeField] private GameObject _player = default;

    private GameObject _projectile = default;

    [Header("�E�A�S�t�A�C�e�����ˈʒu")] [SerializeField] private Transform _spawn = default;

    [Header("�E")] [SerializeField] private GameObject _beard = default;

    [Header("�S�t")] [SerializeField] private GameObject _mucus;

    [Header("����")] [SerializeField] private GameObject _waterBall;
    [Header("���ʔ��ˈʒu")] [SerializeField] private Transform _waterSpawn;

    [Header("�X�s�[�h�A�b�v�G�t�F�N�g")] [SerializeField] private GameObject _waterEffect;

    private Rigidbody2D _rb;

    private bool _isAlive = false;
    private bool _isJump = false;
    private bool _isOneshot = false;
    private bool _isJumping = false;

    private bool _isBeardItem = false;
    private bool _isWaterItem = false;
    private bool _isPridictionItem = false;
    private bool _isMucasItem = false;
    private bool _isGetItem = false;
    private bool _isInvincivle = false;
    private bool _isMucusJump = false;
    private bool _isFrogjump = false; //�΂ߔ�т����Ă��邩
    private bool _isGetWater = false;


    private int _rank = default;

    private float _downMultipl = 1f;�@//��Q���ɓ����������̒�R��
    private float _downSpeed;�@//���ۂ̌�������X�s�[�h
    private float _returnSpeed = 0.07f;//�X�s�[�h�_�E�����痧�Ē�������
    private float _randomItemLottery = default;//�A�C�e���Ŏg��Random.Range�̒l������

    [Header("�v���C���[���x")] [SerializeField] private float _movespeed = 100;
    [Header("�v���C���[�W�����v")] [SerializeField] private float _jumppower = 200f;

    private const float WATERSPEEDUPMULTIPLE = 1.2f; //���A�C�e���g�p���̃X�s�[�h�A�b�v�{��
    private const float MOVESPEED = 100f;//�v���C���[�̃X�s�[�h�̊
    private const float JUMPMIN = 17f;//�S�t���񂾎��̃W�����v��
    private const float JUMPMAX = 200f;
    private const float SPEEDMIN = 80f;
    private const float SPEEDRESETWAITTIME = 3f; //�X�s�[�h�A�b�v���Ă��鎞��
    private const float INVINCIBLETIME = 5f; //���G����

    private const float MAXRANDOMRANGE = 10001;
    private const float MINRANDOMRANGE = 1;

    private const float ITEMSELECTWAIT = 3f;
    private const float MOVEJUMP = 35f;
    private const float FROGJUMPY = 115f;//�΂ߔ�т̂x


    private SpriteRenderer _pridictionSpriterenderer = default;
    [SerializeField] private ItemSelects _itemSelectScript = default;


    [SerializeField] AudioClip _jumpSE = default;
    [SerializeField] AudioClip _speeddownSE = default;
    [SerializeField] AudioClip _damageSE = default;

    [SerializeField] AudioClip _beardSE = default;
    [SerializeField] AudioClip _waterSE = default;
    [SerializeField] AudioClip _pridictionSE = default;
    [SerializeField] AudioClip _mucasSE = default;

    private AudioSource _frogSE = default;

    private Vector3 _cpuposition = default;

    // Start is called before the first frame update

    private Animator _pridictionFrogAnim;
    void Start() {

        _frogSE = this.GetComponent<AudioSource>();
        _pridictionSpriterenderer = this.GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartWait());
        _pridictionFrogAnim = this.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (!_isJump && !_isMucusJump) {

            //_jumppower -= JUMPMAX / 40f;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumppower / MOVEJUMP));//* Time.deltaTime)
        }
        if (!_isJump && _isMucusJump) {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumppower / JUMPMIN));//* Time.deltaTime)
        }
    }
    // Update is called once per frame
    void Update() {
       

        if (_isAlive && !_isFrogjump)//�����Ă鎞�ɓ�����悤��
        {
            //�ړ�
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("L_Stick_Horizontal") < 0) {
                //���΂Ɍ�������
                if (!_pridictionSpriterenderer.flipX) {
                    _pridictionSpriterenderer.flipX = true;
                }

                //�ʏ�̈ړ�
                if (_movespeed >= MOVESPEED) {
                    SEReproduction();

                    _downEffect.gameObject.SetActive(false);
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0);//*Time.deltaTime;
                }

                //�X�s�[�h�_�E��������
                else {

                    SpeedDownSE();
                    //�ړ����x�����X�Ɍ��ɖ߂�
                    _downEffect.gameObject.SetActive(true);
                    _rb.velocity = new Vector3(-_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + _returnSpeed); //* Time.deltaTime;

                }

            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("L_Stick_Horizontal") > 0) {
                //���΂Ɍ�������
                if (_pridictionSpriterenderer.flipX) {
                    _pridictionSpriterenderer.flipX = false;
                }

                //�ʏ�̈ړ�
                if (_movespeed >= MOVESPEED) {
                    SEReproduction();

                    _downEffect.gameObject.SetActive(false);
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                }

                //�X�s�[�h�_�E��������
                else {
                    SpeedDownSE();

                    //�ړ����x�����X�Ɍ��ɖ߂�
                    _downEffect.gameObject.SetActive(true);
                    _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;
                    _movespeed = Mathf.Abs(_movespeed + _returnSpeed); //* Time.deltaTime ;
                }
            }

            //�W�����v


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit")) {


                if (_isJump) {
                    
                    _isJump = false;
                   
                    _frogSE.PlayOneShot(_jumpSE);
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumppower);
                }
            }


            if (_rb.velocity.y <= 0.1f && !_isJump && !_isJumping){
                _isJump = true;
            } 
            //else {
            //    _isJump = false;
            //}

            //if (this._rb.velocity.y > 50) {
            //    if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Submit")) {
            //        _rb.velocity = new Vector3(_rb.velocity.x, _jumppower / 12, 0); //* Time.deltaTime ;
            //    }

            //}
            if (this._rb.velocity.x != 0) {

                _pridictionFrogAnim.SetBool("Run", true);
            } else {
                _pridictionFrogAnim.SetBool("Run", false);
            }

        }

        //�A�C�e���擾��

        //�E���o����
        if (_isBeardItem) {
            Beard();
        }

        //�����o����
        if (_isWaterItem) {
            Water();
        }

        //���G���o����
        if (_isPridictionItem) {
            Pridiction();
        }

        //�S�t���o����
        if (_isMucasItem) {
            Mucas();
        }

    }

    private void OnCollisionStay2D(Collision2D collision) {
        //���ɑ��������Ă�Ƃ�
        if (collision.gameObject.CompareTag("Flor")) {
            _rb.gravityScale = 10;           
            _jumppower = JUMPMAX;
            _pridictionFrogAnim.SetBool("Jump", false);
            _isJumping = false;
            
        }


    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Flor" ) {
            _isFrogjump = false;
           
        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        //�����瑫�����ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Flor") ) {
            //_isJump = false;
            _isJumping = true;
            _isJump = false;
            _pridictionFrogAnim.SetBool("Jump", true);
            if (_player.activeSelf) {
                StartCoroutine(JUMP());
            }
            

        }

    }
    private IEnumerator JUMP() {
        yield return new WaitForSeconds(0.28f);
        _rb.gravityScale = 40f;
    }
    private void SpeedDownSE() {
        if (!_isOneshot) {
            _isOneshot = true;
            _frogSE.PlayOneShot(_speeddownSE);
        }
    }

    private void SEReproduction() {
        if (_isOneshot) {
            _isOneshot = false;

        }

    }




    private IEnumerator RandomItem() //�A�C�e�����I
    {
        if (!_isGetItem) {
            _itemIcon.SetActive(true);
            _itemSelectScript.ItemIcon(0);
            //�A�C�e������������Ԃɕς���
            _isGetItem = true;
            yield return new WaitForSeconds(ITEMSELECTWAIT);
            //�A�C�e���������Ă��Ȃ������璊�I����



           


            //�S�ʂ̎�
            if (_rank == 4) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //�W�O���Ő�
                if (_randomItemLottery >= 2001) {
                    _isWaterItem = true;
                    _itemSelectScript.ItemIcon(1);
                }
                //�P�T���łЂ�
                else if (_randomItemLottery >= 500) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //�T���Ŗ��G
                else {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }

            }
            //�R�ʂ̎�
            else if (_rank == 3) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //�T�O���Ő�
                if (_randomItemLottery >= 5001) {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }
                //30���łЂ�
                else if (_randomItemLottery >= 2001) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //�P�T���Ŗ��G
                else if (_randomItemLottery >= 501) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //�T���ł˂񂦂�
                else {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }

            }
            //�Q�ʂ̎�
            else if (_rank == 2) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //�T�O���łЂ�
                if (_randomItemLottery >= 5001) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //�Q�O���Ő� 
                else if (_randomItemLottery >= 3001) {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }
                //�Q�O���Ŗ��G 
                else if (_randomItemLottery >= 1001) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //�P�O���ŔS�t
                else {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }

            }
            //�P�ʂ̎�
            else if (_rank == 1) {
                _randomItemLottery = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);

                //�U�O���ŔS�t
                if (_randomItemLottery >= 4001) {
                    _itemSelectScript.ItemIcon(4);
                    _isMucasItem = true;
                }
                //�R�O�Ŗ��G
                else if (_randomItemLottery >= 1001) {
                    _itemSelectScript.ItemIcon(3);
                    _isPridictionItem = true;
                }
                //�T���łЂ�
                else if (_randomItemLottery >= 501) {
                    _itemSelectScript.ItemIcon(2);
                    _isBeardItem = true;
                }
                //�T���Ő�
                else {
                    _itemSelectScript.ItemIcon(1);
                    _isWaterItem = true;
                }

            }
        }
    }

    private void Beard() {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Fire1")) {
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_beardSE);

            //�E�̐���
            _projectile = Instantiate(_beard, _spawn.position, Quaternion.identity);

            //�A�C�e�����O�̏�ԂɃ��Z�b�g
            _isGetItem = false;
            _isBeardItem = false;
        }
    }

    private void Mucas() {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Fire1")) {
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_mucasSE);

            //�S�t�̐���
            _projectile = Instantiate(_mucus, _spawn.position, Quaternion.identity);

            //�A�C�e�����O�̏�ԂɃ��Z�b�g
            _isGetItem = false;
            _isMucasItem = false;
        }
    }

    private void Pridiction() {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Fire1")) {
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_pridictionSE);

            //�J�G���𖳓G��Ԃɂ���
            _pruduction.SetActive(true);
            _isInvincivle = true;

            //�A�C�e�����O�̏�ԂɃ��Z�b�g
            _isGetItem = false;
            _isPridictionItem = false;
            StartCoroutine(InvincibleEnd());
        }
    }

    private void Water() {
        if (Input.GetKeyDown(KeyCode.G) || Input.GetButtonDown("Fire1")) {
            _itemIcon.SetActive(false);
            _frogSE.PlayOneShot(_waterSE);

            //���ʐ���
            _projectile = Instantiate(_waterBall, _waterSpawn.position, Quaternion.identity, this.transform);
            //��
            //OnTriggerEnter2D����collision.gameobject.layer == 11���̏����ɑ���

            //�A�C�e�����O�̏�ԂɃ��Z�b�g
            _isGetItem = false;
            _isWaterItem = false;

            _isGetWater = true;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 9 && !_isInvincivle && !_isGetWater)//�E�ɓ������Ă��A���G����Ȃ���
        {
            if (_projectile != null && collision.gameObject != _projectile.gameObject) {
                _frogSE.PlayOneShot(_damageSE);
                _movespeed = SPEEDMIN;
            }
            else 
            {
                _frogSE.PlayOneShot(_damageSE);
                _movespeed = SPEEDMIN;

            }

        }
        if (collision.gameObject.layer == 7 && !_isInvincivle)//�S�t�̏�
       {

            if (_projectile != null && collision.gameObject != _projectile.gameObject) {
                _isMucusJump = true;
                StartCoroutine(MucusJumpTime());
            } 
            else 
            {
                _isMucusJump = true;
                StartCoroutine(MucusJumpTime());
            }

        }
        if (collision.gameObject.CompareTag("Enemy") && !_isInvincivle) {
            StartCoroutine(CollisionEffect());
        }

        //���ʂɓ����������i�X�s�[�h�A�b�v�j
        if (collision.gameObject.layer == 11 && _projectile != null && collision.gameObject == _projectile.gameObject) {
            _movespeed = MOVESPEED * WATERSPEEDUPMULTIPLE;
            _waterEffect.gameObject.SetActive(true);
            collision.gameObject.SetActive(false);

            StartCoroutine(SpeedUpReset());
        }

        //�A�C�e�����������
        if (collision.gameObject.tag == "Fly") {
            StartCoroutine(RandomItem());
        }

    }


    private IEnumerator MucusJumpTime() {
        yield return new WaitForSeconds(3);
        _isMucusJump = false;
    }
   
    public void ObstacleCollision(float speedDownValue) {
        //���G����Ȃ�������
        if (!_isInvincivle&& !_isGetWater) {
            //��Q���ɓ���������

            _frogSE.PlayOneShot(_damageSE);
            _movespeed = speedDownValue;
        }



    }

    private IEnumerator SpeedUpReset() {

        //�R�b��A�X�s�[�h�A�b�v���~�߂�
        yield return new WaitForSeconds(SPEEDRESETWAITTIME);
        {
            _isGetWater = false;
            _waterEffect.gameObject.SetActive(false);

            if (_movespeed >= MOVESPEED) {
                _movespeed = MOVESPEED;
            }

        }
    }

    private IEnumerator InvincibleEnd() {
        //5�b�Ԃ̊ԁA���G�ɂȂ�
        yield return new WaitForSeconds(INVINCIBLETIME);
        //�T�b�������猳�ɖ߂�
        _isInvincivle = false;
        _pruduction.SetActive(false);

    }

    private IEnumerator StartWait() {
        _tongue.SetActive(false);
        yield return new WaitForSeconds(3);
        _tongue.SetActive(true);
        _isAlive = true;


    }
    private IEnumerator CollisionEffect() {
        _enemyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        _enemyEffect.SetActive(false);
    }

    public void RankChange(int nowrank) {
        _rank = nowrank;
    }

    public void PositionChange(GameObject partner, GameObject myself) //partner�ɂ͑O�Ƀe���|�[�g������ΏہA
                                                                      //myself�͌��Ƀe���|�[�g������Ώ�
    {
        if (!_isInvincivle) {
            Vector3 teleportDestination = partner.transform.position;
            partner.transform.position = this.transform.position;
            this.transform.position = teleportDestination;

        }

    }
}
