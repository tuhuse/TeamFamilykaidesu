using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("鳥のスピード")]
    [SerializeField] private float _speed = 15;
    private CircleCollider2D _birdCollider;
    private BirdsInstantiate _birdInstantiate;
    private Vector3 _direction = new Vector3(-1, 1, 0);//斜め上の力
    private bool _isCollision = false;//プレイヤーの当たり判定
    [SerializeField] private GameObject _enemyEffect;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start() {
        _birdInstantiate = GameObject.FindGameObjectWithTag("BirdInstantiate").GetComponent<BirdsInstantiate>();
        _birdCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {

        transform.position += Vector3.left * _speed * Time.deltaTime;//常に左へ行く
        if (_isCollision)//もしプレイヤーに当たったら
        {

            transform.position += _direction.normalized * _speed * Time.deltaTime;//斜め上に飛んでいく
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("CPU"))
        {
            
                StartCoroutine(CollisionEffect());
            _birdCollider.isTrigger = true;
            _isCollision = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reset"))
        {
            //_birdInstantiate._isInstantiateBird = true;
            StartCoroutine(SetActivefalse());
            
        }
    }
   /// <summary>
   /// 画面端に行ったらfalseにする
   /// </summary>
   /// <returns></returns>
    private IEnumerator SetActivefalse()
    {
        yield return new WaitForSeconds(1);//1秒たったら
        this.gameObject.SetActive(false);//このオブジェクトをfalse;
    }
    private IEnumerator CollisionEffect() {
        _enemyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        _enemyEffect.SetActive(false);
    }

}
