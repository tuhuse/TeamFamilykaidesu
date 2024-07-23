using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [SerializeField] private GameObject _canvasManager;
    [SerializeField] private GameObject _cpuParent;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private GameObject _camera;
    [SerializeField] private Transform _camerainitialTransform;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private Transform[] _fistPosition;
    [SerializeField] private Animator _animator;
    private GameObject[] _frogs;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    

    private GameObject[] _cpu;


    // Start is called before the first frame update
    void Start() {
        //_frogs = _canvasManager.GetComponent<SelectCharacter>()._frog;
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnPointerEnter(PointerEventData eventData) {
        _animator.SetBool("ReturnButton", true);
    }
    public void OnPointerExit(PointerEventData eventData) {
        _animator.SetBool("ReturnButton", false);
    }
    public void OnPointerClick(PointerEventData eventData) {
        _audioSource.PlayOneShot(_audioClip);
        //SceneManager.LoadScene("title");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        ////CPUの親に入ってる子供移動
        //_cpuParent.transform.GetChild(0).gameObject.transform.position = _fistPosition[0].position;
        //_cpuParent.transform.GetChild(1).gameObject.transform.position = _fistPosition[1].position;
        //_cpuParent.transform.GetChild(2).gameObject.transform.position = _fistPosition[2].position;
        //_cpuParent.transform.GetChild(3).gameObject.transform.position = _fistPosition[3].position;
        ////プレイヤーの親に入ってる子供移動
        //_playerParent.transform.GetChild(0).gameObject.transform.position = _fistPosition[0].position;
        //_playerParent.transform.GetChild(1).gameObject.transform.position = _fistPosition[1].position;
        //_playerParent.transform.GetChild(2).gameObject.transform.position = _fistPosition[2].position;
        //_playerParent.transform.GetChild(3).gameObject.transform.position = _fistPosition[3].position;

        //    //カメラを初期位置に移動
        //    _camera.transform.position =
        //    _camerainitialTransform.transform.position;
        //    _camera.GetComponent<Camera>().orthographicSize = 60;
        //    Time.timeScale = 1;
        //    StartCoroutine(FlogsCoroutin());

        //    //_camera.transform.position =
        //    // _playerParent.transform.GetChild(1).gameObject.transform.position;

        //    //_camera.transform.position =
        //    //   _playerParent.transform.GetChild(2).gameObject.transform.position;

        //    //_camera.transform.position =
        //    //  _playerParent.transform.GetChild(3).gameObject.transform.position;

        //    _gameOverCanvas.SetActive(false);
        //}
        //private IEnumerator FlogsCoroutin() {
        //    yield return new WaitForSeconds(3);

        //}
    }
}
