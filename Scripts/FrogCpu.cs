using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class FrogCpu : MonoBehaviour {

    private Vector3 _anotherPosition = default;
    [SerializeField] private GameObject _cpuPosition = default;

    [Header("�v���C���[")]
    [SerializeField]
    private GameObject _player;
    [Header("�\�͔��ˈʒu")]
    [SerializeField]
    private Transform _spawn;

    [Header("�S�t�����")]
    [SerializeField] private GameObject _mucus;
    [Header("�E�����")]
    [SerializeField] private GameObject _beard;
    [Header("���ʓ����")]
    [SerializeField] private GameObject _waterBall;
    [SerializeField] private GameObject _waterCpuEffect;
    [SerializeField] private GameObject _pruduction;
    [SerializeField] private GameObject _downEffect;
    [SerializeField] private GameObject _enemyEffect;
    [SerializeField] private GameObject _distancetoCPU1;
    [SerializeField] private GameObject _distancetoCPU2;
    [SerializeField] private WireTongueCPU _tongue;
    [SerializeField] private GameObject _itemIcon;
    [SerializeField] private ItemSelects _item;
    private GameObject _projectile = default;


    private bool _isWaterball = false;
    private bool _isInstantiateWaterBall = false;
    private bool _isWaterAbility = false;
    private bool _isMucusAbility = false;
    private bool _isBeardAbility = false;
    private bool _isJump;
    private bool _isAlive;
    private bool _isPridictionAbility = false;
    private bool _isPridictionStart = true;
    private bool _isMucus = false;
    private bool _isBeard = false;
    private bool _isUseItem=false;
    private bool _isJumping = false;
    public bool _isBehindTrigger = false;
    public bool _isEnemyJump = false;
    private bool _isAudioOneShot = false;
    public bool _ishavingItem = false;
    private bool _isMucusJump = false;

    [Header("CPU�̑���")]
    [SerializeField]
    private float _movespeed;
    [Header("�A�r���e�B�{��")]
    [SerializeField]
    private float _abillitySpeed;
    [Header("CPU�W�����v��")]
    [SerializeField]
    private float _movejump;
    //��Q���ɓ����������̒�R��
    private float _downMultipl = 1f;
    //���ۂ̌�������X�s�[�h
    private float _downSpeed;
    //�X�s�[�h�_�E�����痧�Ē�������
    private float _returnCPUSpeed = 0.07f;

    //�m��
    private int _randamJump = 0;
    private int _randomITEM = 0;
    private const int MINRANDOMRANGE = 1;
    private const int MAXRANDOMRANGE = 10001;
    private Rigidbody2D _rb;


    //�v���C���[�Ƃ̋����̒萔
    private const float DISTANCEPLAYER = 70f;
    //�E�J�G���̍U�����󂯂����̃v���C���[�̑���
    private const float SPEEDMIN = 70f;
    //�v���C���[�̒ʏ�̑���
    private const float MOVESPEED = 100f;
    private const float MOVEJUMPMAX = 200f;
    private const float JUMPMIN = 17f;//�S�t���񂾎��̃W�����v��
    private const float MOVEJUMP = 35f;
    private Animator _mucasFrogCPUAnim;

    private AudioSource _frogSE = default;

    [SerializeField] AudioClip _jumpSE = default;
    [SerializeField] AudioClip _getFlySE = default;
    [SerializeField] AudioClip _speedDownSE = default;
    [SerializeField] AudioClip _damageSE = default;
    [SerializeField] AudioClip _mucasSE = default;
    [SerializeField] AudioClip _waterSE = default;
    [SerializeField] AudioClip _beardSE = default;
    [SerializeField] AudioClip _pridictionSE = default;
    //CPU�̋���
    private enum SwicthRandomJump {
        Easy,
        Hard,
        Harf
    }
    //�A�C�e���̊m��
    private enum RandomItem {
        Great,
        Good,
        Nomal,
        Bad
    }
    
    private SwicthRandomJump _swicthRandomJump = default;
    private RandomItem _randomItem = default;
    
    // Start is called before the first frame update
    void Start() {
        _frogSE = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        //�X�^�[�g����܂œ����Ȃ�����
        StartCoroutine(StartWait());
        _mucasFrogCPUAnim = this.GetComponent<Animator>();

    }
    private void FixedUpdate() {
        if (!_isJump && !_isMucusJump) {

            //_jumppower -= JUMPMAX / 40f;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_movejump / MOVEJUMP));//* Time.deltaTime)
        }
        if (!_isJump && _isMucusJump) {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_movejump / JUMPMIN));//* Time.deltaTime)
        }
    }
    // Update is called once per frame
    void Update() {

        //�v���C���[�Ƃ̋���
        float distancetoplayer =
            Vector3.Distance(this.transform.position, _player.transform.position);

        #region ���g�̏��ʎ擾
        float mySelf = this.transform.localPosition.x;
        float player = _player.transform.localPosition.x;
        float cpu1 = _distancetoCPU1.transform.localPosition.x;
        float cpu2 = _distancetoCPU2.transform.localPosition.x;
     
        //�����̏��ʂ�c��
        if (mySelf > player && mySelf > cpu1 && mySelf > cpu2) {
            _randomItem = RandomItem.Bad;//��ʂ̎�
        }
        if ((mySelf > player && mySelf > cpu1 && mySelf < cpu2) ||
           (mySelf > player && mySelf < cpu1 && mySelf > cpu2) ||
           (mySelf < player && mySelf > cpu1 && mySelf > cpu2)) {
            _randomItem = RandomItem.Nomal;//��ʂ̎�
        }
        if ((mySelf < player && mySelf < cpu1 && mySelf > cpu2) ||
           (mySelf > player && mySelf < cpu1 && mySelf < cpu2) ||
           (mySelf < player && mySelf > cpu1 && mySelf < cpu2)) {
            _randomItem = RandomItem.Good;//�O�ʂ̎�
        } else if (mySelf < cpu1 && mySelf < cpu2 && mySelf < player) {
            _randomItem = RandomItem.Great;//�ŉ��ʂ̎�
        }

        #endregion
        //print(_randomItem);
        //�����Ă���ꍇ
        #region ���g�̋�������
        if (_isAlive) {
            if (!_isWaterAbility) {
                NomalController();
            } else {
                AbillityController();
            }



            //�A�C�e���g�p
            BeardAttack();
            WaterBall();
            MucusAttack();
            UseAbility();
            //�v���C���[�Ƃ̋���������Ă���ꍇ
            if (distancetoplayer > DISTANCEPLAYER) 
            {
                
                //�v���C���[�������������ɂ����ꍇ
                if (player < mySelf) {

                    _swicthRandomJump = SwicthRandomJump.Hard;
                }
                //�v���C���[�����������O�ɂ����ꍇ
                if (mySelf < player) 
                {
                    _swicthRandomJump = SwicthRandomJump.Easy;

                }

            }
            if (_randomItem == RandomItem.Great || _randomItem == RandomItem.Good|| _randomItem == RandomItem.Nomal) {
                ISEXtension();
            }

            //�v���C���[�̂Ƃ̋������߂��Ƃ�
            else if (distancetoplayer < DISTANCEPLAYER) {
                if (mySelf < player) {

                    _swicthRandomJump = SwicthRandomJump.Easy;

                }

                _swicthRandomJump = SwicthRandomJump.Harf;

            }
            #endregion
            //�A�j���[�V����
            if (this._rb.velocity.x != 0) {

                _mucasFrogCPUAnim.SetBool("Run", true);
            } else {
                _mucasFrogCPUAnim.SetBool("Run", false);
            }          
        }
        //CPU�̃W�����v����
        if ((_isEnemyJump && _isJump)) {
            
            EnemyJump();
        }
        if (_isWaterball) {
            _item.ItemIcon(1);
        } 
        if (_isBeardAbility) {
            _item.ItemIcon(2);
        }
        if (_isPridictionAbility) {
            _item.ItemIcon(3);
        }
        if (_isMucusAbility) {
            _item.ItemIcon(4);
        }
        if (!_isWaterball && !_isBeardAbility && !_isPridictionAbility && !_isMucusAbility) {
            _item.ItemIcon(0);
        }

        if (_ishavingItem) {
            _itemIcon.SetActive(true);
        } else {
            _itemIcon.SetActive(false);
        }

        if (_rb.velocity.y <= 0.1f && !_isJump && !_isJumping) {
            _isJump = true;
        }
    }
    //private void FrogJump() {
    //    �^���
    //    _frogSE.PlayOneShot(_jumpSE);
    //    _rb.velocity = new Vector3(55, _movejump[0], 0);
    //}
    private void Jump() {
        if (_isJump) {
            _isJump = false;
            _frogSE.PlayOneShot(_jumpSE);           
            _rb.velocity = new Vector2(_rb.velocity.x, _movejump); //* Time.deltaTime;
        }
    }
    private void Jump2() {

        if (_isJump) {
            _isJump = false;
            _frogSE.PlayOneShot(_jumpSE);
            _rb.velocity = new Vector2(_rb.velocity.x, _movejump); //* Time.deltaTime;
            _isEnemyJump = false;
        }           
        
    }

    private void EnemyJump() {

        if (_swicthRandomJump == SwicthRandomJump.Easy) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //95%�ŃW�����v
            if (_randamJump >= 500) {
                Jump2();
            }
        }
        if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //80%�ŃW�����v
            if (_randamJump >= 2000) {
                Jump2();
            }

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //90%�ŃW�����v
            if (_randamJump >= 1000) {
                Jump2();
            }
        }
    }
 
    private void CliffJump() {
        if (_swicthRandomJump == SwicthRandomJump.Easy) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //96%�ŃW�����v
            if (_randamJump >= 400) {
                Jump();
            } //else {
            //    FrogJump();

            //}
        } else if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //95%�ŃW�����v
            if (_randamJump >= 500) {
                Jump();
            }// else {

            //    FrogJump();
            //}

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //90%�ŃW�����v
            if (_randamJump >= 1000) {
                Jump();
            } //else {

            //    FrogJump();
            //}
        }
    }

    private void ChooseSelct() {
        if (_swicthRandomJump == SwicthRandomJump.Easy) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //60%�ŃW�����v
            if (_randamJump >= 4000) {
                Jump();
            }
        } else if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //55%�ŃW�����v
            if (_randamJump >= 4500) {
                Jump();
            }

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //�m���v�Z
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //50%�ŃW�����v
            if (_randamJump >= 5000) {
                Jump();
            }
        }
    }
 
   
    private void OnTriggerEnter2D(Collider2D collision) {
        //���[�g���򂪗�������CPU�̃W�����v����
        if (collision.gameObject.CompareTag("ChooseSelect") && _isJump) {
            
            ChooseSelct();

        } //CPU�̃W�����v����
        //�������R
        if (collision.gameObject.CompareTag("cliff") && _isJump) {
            
            CliffJump();
        }
       
        if (collision.gameObject.CompareTag("Fly")) {
            _frogSE.PlayOneShot(_getFlySE);
            if (!_ishavingItem) {
                _isUseItem = false;
                _ishavingItem = true;
                StartCoroutine(ItemWait());
               
            }

        }
        //����    
        if (collision.gameObject.layer == 11 && _projectile != null && collision.gameObject == _projectile.gameObject) {
            _frogSE.PlayOneShot(_waterSE);
            _isWaterAbility = true;
            StartCoroutine(WaterAbility());
            collision.gameObject.SetActive(false);
        }
        //�E
        if (collision.gameObject.layer == 9) {
            if (!_isPridictionAbility&&!_isWaterAbility) 
            {
                if (_projectile != null && collision.gameObject != _projectile.gameObject) {
                    //�E�ɓ����������̌���
                    _movespeed = SPEEDMIN;
                    _frogSE.PlayOneShot(_damageSE);
                } 
                else 
                {
                    _movespeed = SPEEDMIN;
                    _frogSE.PlayOneShot(_damageSE);

                }
               
            }
        }
       
        if (collision.gameObject.CompareTag("edge")) {
            _isAlive = false;//�ǒ[�ŃW�����v���Ȃ��悤�ɂ�����
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            if (_isPridictionAbility) {
                {
                    PriictionAbility();
                }
            }
            StartCoroutine(CollisionEffect());
        }
        if (!_isPridictionAbility) {
            //�S�t�̏�
            if (collision.gameObject.layer == 7  ) 
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

        }

    }
   
    private void GetItem() {

        // �A�C�e���̊m��
        switch (_randomItem) {
            //�ŉ��ʂ̎�
            case RandomItem.Great:
                _randomITEM = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
                //80%
                if (_randomITEM >= 2000) {
                    _isWaterball = true;
                    
                   
                }//15% 
                else if (_randomITEM > 500 && _randomITEM < 2000) {
                    _isBeardAbility = true;
                 
                }//5%
                else {
                    _isPridictionAbility = true;
                  

                }
                break;
            //�O�ʂ̎�
            case RandomItem.Good:
                _randomITEM = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
                //50%
                if (_randomITEM >= 5000) {
                    _isWaterball = true;
                  
                }//30%
                else if (_randomITEM >= 2000 && _randomITEM < 5000) {
                    _isBeardAbility = true;
                    
                }//15% 
                else if (_randomITEM >= 500 && _randomITEM < 2000) {
                    _isPridictionAbility = true;
                 
                }//5% 
                else {
                    _isMucusAbility = true;
                  
                    
                }
                break;
            //��ʂ̎�
            case RandomItem.Nomal:
                _randomITEM = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
                //20%
                if (_randomITEM >= 8000) {
                    _isWaterball = true;
                  
                }//50%
                else if (_randomITEM >= 3000 && _randomITEM < 8000) {
                    _isBeardAbility = true;
                   
                }//20% 
                else if (_randomITEM >= 1000 && _randomITEM < 3000) {
                    _isPridictionAbility = true;
                  
                }//10% 
                else {
                    _isMucusAbility = true;
                    
                }
                break;
            //��ʂ̎�
            case RandomItem.Bad:
                _randomITEM = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
                //5%
                if (_randomITEM >= 9500) {
                    _isWaterball = true;
                    
                }//5%
                else if (_randomITEM >= 9000 && _randomITEM < 9500) {
                    _isBeardAbility = true;
                  
                }//30% 
                else if (_randomITEM >= 6000 && _randomITEM < 9000) {
                    _isPridictionAbility = true;
                   
                }//60% 
                else {
                    _isMucusAbility = true;
                   
                }
                break;
        }

       
    }
 

    #region ���G�\��
    //���G����
    private void PriictionAbility() {
        if (_isPridictionStart) {
            if (_isPridictionAbility) {
                _frogSE.PlayOneShot(_pridictionSE);
                _pruduction.SetActive(true);
                _ishavingItem = false;
                StartCoroutine(AbilityStop());
                _isPridictionStart = false;
            }

        }
    }
    //���G�I���
    private IEnumerator AbilityStop() {

        yield return new WaitForSeconds(5f);//�A�r���e�B�I��
        _isPridictionAbility = false;
        _isPridictionStart = true;
        if (!_isPridictionAbility) {
            _pruduction.SetActive(false);
        }
    }
    //�Ђ������ł����Ƃ�
    private void UseAbility() {
        if (_isPridictionAbility) {
            if (_isBehindTrigger) {
                PriictionAbility();
                _isBehindTrigger = false;
            }
        }
    }

    #endregion
    #region �S�t�\��
    //�S�t�o������
    private void MucusAttack() {
        if ((!_isUseItem &&_isMucusAbility && _randomItem == RandomItem.Bad) ||
            (!_isUseItem && _isMucusAbility && _randomItem == RandomItem.Good) ||
            (!_isUseItem && _isMucusAbility && _randomItem == RandomItem.Nomal)) {
            StartCoroutine(MucusWaitTime());
            _isUseItem = true;
        }
    }
    //�X�L���g�p
    private void MucusAbility() {
        //�X�L�����g����悤�ɂȂ����Ƃ�
        if (_isMucus) {
            _frogSE.PlayOneShot(_mucasSE);
            _projectile = Instantiate(_mucus, _spawn.position, Quaternion.identity);
            _isMucus = false;
            _ishavingItem = false;
        }
    }
    //�X�L���g���ҋ@����
    private IEnumerator MucusWaitTime() {
        _isMucus = true;
        yield return new WaitForSeconds(1);
        _isMucusAbility = false;
        MucusAbility();
    }
    private IEnumerator MucusJumpTime() {
        yield return new WaitForSeconds(3);
        _isMucusJump = false;
    }
    #endregion
    #region �Ђ��\��
    //�Ђ�����
    private void BeardAbility() {
        if (_isBeard) {
            _projectile = Instantiate(_beard, _spawn.position, Quaternion.identity);
            _frogSE.PlayOneShot(_beardSE);
            _isBeard = false;
            _ishavingItem = false;
        }

    }
    //�Ђ��o������
    private void BeardAttack() {
        if ((!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Great) ||
            (!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Good) ||
            (!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Nomal)) {
            StartCoroutine(BeardWaitTime());
            _isUseItem = true;
        }
    }
    //�Ђ��ҋ@����
    private IEnumerator BeardWaitTime() {
        _isBeard = true;
        yield return new WaitForSeconds(1);
        _isBeardAbility = false;
        BeardAbility();
    }
    #endregion
    #region ���\��
    //���ʐ���
    private void WaterBall() {
        //���ʐ���
        if (!_isUseItem && _isWaterball) {
            StartCoroutine(WaterWait());
            _isUseItem = true;
        }
    }
    //�X�L������
    private IEnumerator WaterAbility() {

        _ishavingItem = false;
        yield return new WaitForSeconds(3);
        _movespeed = MOVESPEED;
        _isWaterAbility = false;
        _waterCpuEffect.SetActive(false);

    }
    //�X�L���ҋ@����
    private IEnumerator WaterWait() {
        _isInstantiateWaterBall = true;
        yield return new WaitForSeconds(1);
        _isWaterball = false;
        if (_isInstantiateWaterBall) {
            _projectile = Instantiate(_waterBall, _spawn.position, Quaternion.identity, this.transform);
            _isInstantiateWaterBall = false;
        }
    }

    //�X�L���g���Ă鎞�̃X�s�[�h
    private void AbillityController() {
        if (_movespeed >= MOVESPEED)//�ʏ�̈ړ�
           {
            _waterCpuEffect.gameObject.SetActive(true);
            _downEffect.gameObject.SetActive(false);
            _rb.velocity = new Vector3(_movespeed * _abillitySpeed, _rb.velocity.y, 0);
        } else {
            _waterCpuEffect.gameObject.SetActive(false);
            _downEffect.gameObject.SetActive(true);
            _rb.velocity = new Vector3(_movespeed * _abillitySpeed, _rb.velocity.y, 0);
            _movespeed = Mathf.Abs(_movespeed + _returnCPUSpeed);
        }
    }
    #endregion
    private void NomalController() {
        //���i�͉��ړ�
        if (_movespeed >= MOVESPEED) {
            if (_isAudioOneShot) {
                _isAudioOneShot = false;
            }
            _downEffect.gameObject.SetActive(false);
            //�E�Ɉړ�
            _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
        }
        //�ړ����x�����X�Ɍ��ɖ߂�
        else {
            if (!_isAudioOneShot) {
                _isAudioOneShot = true;
                _frogSE.PlayOneShot(_speedDownSE);
            }
            _downEffect.gameObject.SetActive(true);
            //�E�Ɉړ�
            _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
            _movespeed = Mathf.Abs(_movespeed + _returnCPUSpeed);
        }
    }
    //�����̃J�G������
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(3);
        _isAlive = true;
    }
    private IEnumerator ItemWait() {
        yield return new WaitForSeconds(3);
        GetItem();
    }

    //��Q���ɓ����������̌���
    public void ObstacleCollision(float speedDownValue) {
        if (!_isPridictionAbility&&!_isWaterAbility) {
            _frogSE.PlayOneShot(_damageSE);
         
            _movespeed = speedDownValue;
        }
    }

    private IEnumerator CollisionEffect() {
        _enemyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        _enemyEffect.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        //���ɑ��������Ă�Ƃ�
        
        if (collision.gameObject.CompareTag("Flor")||collision.gameObject.CompareTag("CPU")||collision.gameObject.CompareTag("Player")) {
            _mucasFrogCPUAnim.SetBool("Jump", false);
            _isJumping = false;
            _movejump = MOVEJUMPMAX;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        
        //�����瑫�����ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Flor") || collision.gameObject.CompareTag("CPU") || collision.gameObject.CompareTag("Player")) {
            _mucasFrogCPUAnim.SetBool("Jump", true);
            _isJumping = true;
        }

    }
    private void ISEXtension() 
    {
        
        if (!_tongue._isCoolDown) {
            
            _tongue._isCoolDown = true;
            _tongue._isExtension = true;
            _tongue._underAttack = true;
            //StartCoroutine(ISExtension());
       }     
    }
    private IEnumerator ISExtension() {
        yield return new WaitForSeconds(0.11f);
        _tongue._isExtension=false;
    }

    public void PositionChange(GameObject partner, GameObject myself) //partner�ɂ͑O�Ƀe���|�[�g������ΏہA
                                                                      //myself�͌��Ƀe���|�[�g������Ώ�
     {
        if (!_isPridictionAbility) 
        {
            Vector3 teleportDestination = partner.transform.position;
            partner.transform.position = this.transform.position;
            this.transform.position = teleportDestination;

        }

    }

}
