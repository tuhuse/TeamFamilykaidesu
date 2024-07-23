using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class FrogCpu : MonoBehaviour {

    private Vector3 _anotherPosition = default;
    [SerializeField] private GameObject _cpuPosition = default;

    [Header("プレイヤー")]
    [SerializeField]
    private GameObject _player;
    [Header("能力発射位置")]
    [SerializeField]
    private Transform _spawn;

    [Header("粘液入れて")]
    [SerializeField] private GameObject _mucus;
    [Header("髭入れて")]
    [SerializeField] private GameObject _beard;
    [Header("水玉入れて")]
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

    [Header("CPUの速さ")]
    [SerializeField]
    private float _movespeed;
    [Header("アビリティ倍率")]
    [SerializeField]
    private float _abillitySpeed;
    [Header("CPUジャンプ力")]
    [SerializeField]
    private float _movejump;
    //障害物に当たった時の抵抗力
    private float _downMultipl = 1f;
    //実際の減少するスピード
    private float _downSpeed;
    //スピードダウンから立て直す速さ
    private float _returnCPUSpeed = 0.07f;

    //確率
    private int _randamJump = 0;
    private int _randomITEM = 0;
    private const int MINRANDOMRANGE = 1;
    private const int MAXRANDOMRANGE = 10001;
    private Rigidbody2D _rb;


    //プレイヤーとの距離の定数
    private const float DISTANCEPLAYER = 70f;
    //髭カエルの攻撃を受けた時のプレイヤーの速さ
    private const float SPEEDMIN = 70f;
    //プレイヤーの通常の速さ
    private const float MOVESPEED = 100f;
    private const float MOVEJUMPMAX = 200f;
    private const float JUMPMIN = 17f;//粘液踏んだ時のジャンプ力
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
    //CPUの強さ
    private enum SwicthRandomJump {
        Easy,
        Hard,
        Harf
    }
    //アイテムの確立
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
        //スタートするまで動けなくする
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

        //プレイヤーとの距離
        float distancetoplayer =
            Vector3.Distance(this.transform.position, _player.transform.position);

        #region 自身の順位取得
        float mySelf = this.transform.localPosition.x;
        float player = _player.transform.localPosition.x;
        float cpu1 = _distancetoCPU1.transform.localPosition.x;
        float cpu2 = _distancetoCPU2.transform.localPosition.x;
     
        //自分の順位を把握
        if (mySelf > player && mySelf > cpu1 && mySelf > cpu2) {
            _randomItem = RandomItem.Bad;//一位の時
        }
        if ((mySelf > player && mySelf > cpu1 && mySelf < cpu2) ||
           (mySelf > player && mySelf < cpu1 && mySelf > cpu2) ||
           (mySelf < player && mySelf > cpu1 && mySelf > cpu2)) {
            _randomItem = RandomItem.Nomal;//二位の時
        }
        if ((mySelf < player && mySelf < cpu1 && mySelf > cpu2) ||
           (mySelf > player && mySelf < cpu1 && mySelf < cpu2) ||
           (mySelf < player && mySelf > cpu1 && mySelf < cpu2)) {
            _randomItem = RandomItem.Good;//三位の時
        } else if (mySelf < cpu1 && mySelf < cpu2 && mySelf < player) {
            _randomItem = RandomItem.Great;//最下位の時
        }

        #endregion
        //print(_randomItem);
        //生きている場合
        #region 自身の強さ調整
        if (_isAlive) {
            if (!_isWaterAbility) {
                NomalController();
            } else {
                AbillityController();
            }



            //アイテム使用
            BeardAttack();
            WaterBall();
            MucusAttack();
            UseAbility();
            //プレイヤーとの距離が離れている場合
            if (distancetoplayer > DISTANCEPLAYER) 
            {
                
                //プレイヤーが自分よりも後ろにいた場合
                if (player < mySelf) {

                    _swicthRandomJump = SwicthRandomJump.Hard;
                }
                //プレイヤーが自分よりも前にいた場合
                if (mySelf < player) 
                {
                    _swicthRandomJump = SwicthRandomJump.Easy;

                }

            }
            if (_randomItem == RandomItem.Great || _randomItem == RandomItem.Good|| _randomItem == RandomItem.Nomal) {
                ISEXtension();
            }

            //プレイヤーのとの距離が近いとき
            else if (distancetoplayer < DISTANCEPLAYER) {
                if (mySelf < player) {

                    _swicthRandomJump = SwicthRandomJump.Easy;

                }

                _swicthRandomJump = SwicthRandomJump.Harf;

            }
            #endregion
            //アニメーション
            if (this._rb.velocity.x != 0) {

                _mucasFrogCPUAnim.SetBool("Run", true);
            } else {
                _mucasFrogCPUAnim.SetBool("Run", false);
            }          
        }
        //CPUのジャンプ挙動
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
    //    蛙飛び
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
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //95%でジャンプ
            if (_randamJump >= 500) {
                Jump2();
            }
        }
        if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //80%でジャンプ
            if (_randamJump >= 2000) {
                Jump2();
            }

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //90%でジャンプ
            if (_randamJump >= 1000) {
                Jump2();
            }
        }
    }
 
    private void CliffJump() {
        if (_swicthRandomJump == SwicthRandomJump.Easy) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //96%でジャンプ
            if (_randamJump >= 400) {
                Jump();
            } //else {
            //    FrogJump();

            //}
        } else if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //95%でジャンプ
            if (_randamJump >= 500) {
                Jump();
            }// else {

            //    FrogJump();
            //}

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //90%でジャンプ
            if (_randamJump >= 1000) {
                Jump();
            } //else {

            //    FrogJump();
            //}
        }
    }

    private void ChooseSelct() {
        if (_swicthRandomJump == SwicthRandomJump.Easy) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //60%でジャンプ
            if (_randamJump >= 4000) {
                Jump();
            }
        } else if (_swicthRandomJump == SwicthRandomJump.Hard) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //55%でジャンプ
            if (_randamJump >= 4500) {
                Jump();
            }

        } else if (_swicthRandomJump == SwicthRandomJump.Harf) {
            //確率計算
            _randamJump = Random.Range(MINRANDOMRANGE, MAXRANDOMRANGE);
            //50%でジャンプ
            if (_randamJump >= 5000) {
                Jump();
            }
        }
    }
 
   
    private void OnTriggerEnter2D(Collider2D collision) {
        //ルート分岐が来た時のCPUのジャンプ挙動
        if (collision.gameObject.CompareTag("ChooseSelect") && _isJump) {
            
            ChooseSelct();

        } //CPUのジャンプ挙動
        //小さい崖
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
        //水玉    
        if (collision.gameObject.layer == 11 && _projectile != null && collision.gameObject == _projectile.gameObject) {
            _frogSE.PlayOneShot(_waterSE);
            _isWaterAbility = true;
            StartCoroutine(WaterAbility());
            collision.gameObject.SetActive(false);
        }
        //髭
        if (collision.gameObject.layer == 9) {
            if (!_isPridictionAbility&&!_isWaterAbility) 
            {
                if (_projectile != null && collision.gameObject != _projectile.gameObject) {
                    //髭に当たった時の効果
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
            _isAlive = false;//壁端でジャンプしないようにするやつ
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
            //粘液の床
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

        // アイテムの確立
        switch (_randomItem) {
            //最下位の時
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
            //三位の時
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
            //二位の時
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
            //一位の時
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
 

    #region 無敵能力
    //無敵発動
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
    //無敵終わり
    private IEnumerator AbilityStop() {

        yield return new WaitForSeconds(5f);//アビリティ終了
        _isPridictionAbility = false;
        _isPridictionStart = true;
        if (!_isPridictionAbility) {
            _pruduction.SetActive(false);
        }
    }
    //ひげが飛んできたとき
    private void UseAbility() {
        if (_isPridictionAbility) {
            if (_isBehindTrigger) {
                PriictionAbility();
                _isBehindTrigger = false;
            }
        }
    }

    #endregion
    #region 粘液能力
    //粘液出す条件
    private void MucusAttack() {
        if ((!_isUseItem &&_isMucusAbility && _randomItem == RandomItem.Bad) ||
            (!_isUseItem && _isMucusAbility && _randomItem == RandomItem.Good) ||
            (!_isUseItem && _isMucusAbility && _randomItem == RandomItem.Nomal)) {
            StartCoroutine(MucusWaitTime());
            _isUseItem = true;
        }
    }
    //スキル使用
    private void MucusAbility() {
        //スキルが使えるようになったとき
        if (_isMucus) {
            _frogSE.PlayOneShot(_mucasSE);
            _projectile = Instantiate(_mucus, _spawn.position, Quaternion.identity);
            _isMucus = false;
            _ishavingItem = false;
        }
    }
    //スキル使う待機時間
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
    #region ひげ能力
    //ひげ生成
    private void BeardAbility() {
        if (_isBeard) {
            _projectile = Instantiate(_beard, _spawn.position, Quaternion.identity);
            _frogSE.PlayOneShot(_beardSE);
            _isBeard = false;
            _ishavingItem = false;
        }

    }
    //ひげ出す条件
    private void BeardAttack() {
        if ((!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Great) ||
            (!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Good) ||
            (!_isUseItem && _isBeardAbility && _randomItem == RandomItem.Nomal)) {
            StartCoroutine(BeardWaitTime());
            _isUseItem = true;
        }
    }
    //ひげ待機時間
    private IEnumerator BeardWaitTime() {
        _isBeard = true;
        yield return new WaitForSeconds(1);
        _isBeardAbility = false;
        BeardAbility();
    }
    #endregion
    #region 水能力
    //水玉生成
    private void WaterBall() {
        //水玉生成
        if (!_isUseItem && _isWaterball) {
            StartCoroutine(WaterWait());
            _isUseItem = true;
        }
    }
    //スキル発動
    private IEnumerator WaterAbility() {

        _ishavingItem = false;
        yield return new WaitForSeconds(3);
        _movespeed = MOVESPEED;
        _isWaterAbility = false;
        _waterCpuEffect.SetActive(false);

    }
    //スキル待機時間
    private IEnumerator WaterWait() {
        _isInstantiateWaterBall = true;
        yield return new WaitForSeconds(1);
        _isWaterball = false;
        if (_isInstantiateWaterBall) {
            _projectile = Instantiate(_waterBall, _spawn.position, Quaternion.identity, this.transform);
            _isInstantiateWaterBall = false;
        }
    }

    //スキル使ってる時のスピード
    private void AbillityController() {
        if (_movespeed >= MOVESPEED)//通常の移動
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
        //普段は横移動
        if (_movespeed >= MOVESPEED) {
            if (_isAudioOneShot) {
                _isAudioOneShot = false;
            }
            _downEffect.gameObject.SetActive(false);
            //右に移動
            _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
        }
        //移動速度を徐々に元に戻す
        else {
            if (!_isAudioOneShot) {
                _isAudioOneShot = true;
                _frogSE.PlayOneShot(_speedDownSE);
            }
            _downEffect.gameObject.SetActive(true);
            //右に移動
            _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0);
            _movespeed = Mathf.Abs(_movespeed + _returnCPUSpeed);
        }
    }
    //初動のカエルたち
    private IEnumerator StartWait() {
        yield return new WaitForSeconds(3);
        _isAlive = true;
    }
    private IEnumerator ItemWait() {
        yield return new WaitForSeconds(3);
        GetItem();
    }

    //障害物に当たった時の減速
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
        //床に足が着いてるとき
        
        if (collision.gameObject.CompareTag("Flor")||collision.gameObject.CompareTag("CPU")||collision.gameObject.CompareTag("Player")) {
            _mucasFrogCPUAnim.SetBool("Jump", false);
            _isJumping = false;
            _movejump = MOVEJUMPMAX;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        
        //床から足が離れたとき
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

    public void PositionChange(GameObject partner, GameObject myself) //partnerには前にテレポートさせる対象、
                                                                      //myselfは後ろにテレポートさせる対象
     {
        if (!_isPridictionAbility) 
        {
            Vector3 teleportDestination = partner.transform.position;
            partner.transform.position = this.transform.position;
            this.transform.position = teleportDestination;

        }

    }

}
