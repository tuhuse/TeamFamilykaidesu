using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("���̃X�s�[�h")]
    [SerializeField] private float _speed = 15;
    private CircleCollider2D _birdCollider;
    private BirdsInstantiate _birdInstantiate;
    private Vector3 _direction = new Vector3(-1, 1, 0);//�΂ߏ�̗�
    private bool _isCollision = false;//�v���C���[�̓����蔻��
    [SerializeField] private GameObject _enemyEffect;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start() {
        _birdInstantiate = GameObject.FindGameObjectWithTag("BirdInstantiate").GetComponent<BirdsInstantiate>();
        _birdCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {

        transform.position += Vector3.left * _speed * Time.deltaTime;//��ɍ��֍s��
        if (_isCollision)//�����v���C���[�ɓ���������
        {

            transform.position += _direction.normalized * _speed * Time.deltaTime;//�΂ߏ�ɔ��ł���
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
   /// ��ʒ[�ɍs������false�ɂ���
   /// </summary>
   /// <returns></returns>
    private IEnumerator SetActivefalse()
    {
        yield return new WaitForSeconds(1);//1�b��������
        this.gameObject.SetActive(false);//���̃I�u�W�F�N�g��false;
    }
    private IEnumerator CollisionEffect() {
        _enemyEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        _enemyEffect.SetActive(false);
    }

}
