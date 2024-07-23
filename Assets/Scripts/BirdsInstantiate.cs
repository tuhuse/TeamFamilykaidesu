using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsInstantiate : MonoBehaviour
{
    [Header("�����ʒu")]
    [SerializeField] private Transform _instantiatePosition;
    [Header("���̃v���t�@�u������")]
    [SerializeField] GameObject _birds;
    public bool _isInstantiateBird;//���������ł��邩�̔���
    private void Start() {
        StartCoroutine(StartWait());
    }
    // Update is called once per frame
    void Update()
    {
        if (_isInstantiateBird) {

            StartCoroutine(InstantiateBird());

        }
    }
    /// <summary>
    /// ���̐���
    /// </summary>
    /// <returns></returns>
    private IEnumerator InstantiateBird()
    {
        _isInstantiateBird = false;
        yield return new WaitForSeconds(2);
        Instantiate(_birds, _instantiatePosition.position, Quaternion.identity);//����
        yield return new WaitForSeconds(3);
        _isInstantiateBird = true;
    }
    private IEnumerator StartWait() {
        _isInstantiateBird = false;
        yield return new WaitForSeconds(3);
        _isInstantiateBird = true;
    }


}
