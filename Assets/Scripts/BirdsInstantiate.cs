using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsInstantiate : MonoBehaviour
{
    [Header("生成位置")]
    [SerializeField] private Transform _instantiatePosition;
    [Header("鳥のプレファブを入れて")]
    [SerializeField] GameObject _birds;
    public bool _isInstantiateBird;//鳥が生成できるかの判定
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
    /// 鳥の生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator InstantiateBird()
    {
        _isInstantiateBird = false;
        yield return new WaitForSeconds(2);
        Instantiate(_birds, _instantiatePosition.position, Quaternion.identity);//生成
        yield return new WaitForSeconds(3);
        _isInstantiateBird = true;
    }
    private IEnumerator StartWait() {
        _isInstantiateBird = false;
        yield return new WaitForSeconds(3);
        _isInstantiateBird = true;
    }


}
