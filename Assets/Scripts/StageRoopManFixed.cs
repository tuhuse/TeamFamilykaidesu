using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRoopManFixed : MonoBehaviour {

    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _addPrefabs = new List<GameObject>();
    [SerializeField] private GameObject _playerObjct = null;
    [SerializeField] private GameObject _stageParents;
    [SerializeField] private GameObject _outLineObject = default;
    [SerializeField] private float _cameraTargetY;
    [SerializeField] private GameObject _hurryUpText;
    [SerializeField] private GameOverMan _gameOverMan;
    [SerializeField] private ClearMan _gameClearMan;
    [SerializeField] private GameObject _first;
   
    [SerializeField] CameraShake _cameraShake;
    private Camera _camera;
    private float _cameraSizeDecrease;
    private bool _isCameraSize = false;
    private float _startSize;
    private Vector3 _startPosition;
    private float _timer;
    private float _changeDuration = 10f;
    [SerializeField] private CountDowntext _count;
    private bool _isOneShot = false;

    [SerializeField] AudioClip _warningSE = default;

    private AudioSource _audio = default;

    //ステージの移動距離定数
    [SerializeField] private float _stageXPosition = default;
    //ステージの現在位置
    [SerializeField] private float _stageNowPosition = default;

    //配列番号
    private int _arrayNumber = 10;
    private int _randomMax = default;
    private int _randomMin = 0;
    private int _prefabNumber = 7;
    private int _switchNumber = 0;
    //private float _playerNowposition = default;
    private float _countTime = default;
    private float _time = 45;
    private float _nextTime = 55f;

    public bool _isRoop = false;
    private void Start() {
        _camera = Camera.main;
        _startSize = _camera.orthographicSize;
        _startPosition = _camera.transform.position;
        _audio = this.GetComponent<AudioSource>();
        _first=_camera.GetComponent<CameraRankScript>()._ranking[0];

    }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _countTime += 100;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _countTime += 100;
        }
        _countTime += Time.deltaTime;

        float playerNowposition = _first.transform.position.x;

        if (_gameOverMan._switchNumber == 3 || _gameClearMan._switchNumber == 3) {
            _isCameraSize = false;
        }
        if (_isCameraSize && _camera.orthographicSize > 45) 
        {
            _timer += Time.deltaTime; // 経過時間を加算する
            // 時間が経過した割合を計算する
            float timeRatio = _timer / _changeDuration;
            timeRatio = Mathf.Clamp01(timeRatio); // 0から1の間にクランプ

            // 現在のサイズから目標サイズにカメラのサイズを変更する
            _camera.orthographicSize = Mathf.Lerp(_startSize, 45f, timeRatio);

            float targetY = _cameraTargetY;//cameraが向かうY座標
            Vector3 targetPosition = new Vector3(_camera.transform.position.x, Mathf.Lerp(_startPosition.y, targetY, timeRatio), _camera.transform.position.z);
            _camera.transform.position = targetPosition;

        }

        

        //ランダムの値を保持
        //プレイヤーが先頭のひとつ前のステージについたらランダムに続きを移動
        if (playerNowposition >= _stageNowPosition) {

            //現在の最後尾を避けて配列数を取得
            _randomMax = _prefabs.Count - 2;

            _arrayNumber = Random.Range(_randomMin, _randomMax);
            //idou
            _stageNowPosition += _stageXPosition;


            _prefabs[_arrayNumber].transform.position =
                new Vector2(_stageNowPosition, _prefabs[_arrayNumber].transform.position.y);

            //移動したステージを配列の末尾に追加し、行を詰める
            _prefabs.Add(_prefabs[_arrayNumber]);

            _prefabs.Remove(_prefabs[_arrayNumber]);


        } else {
            _isRoop = false;
        }


        switch (_switchNumber) {
            case 0:
                if (_countTime >= _time) {

                    for (int number = 0; number < _prefabNumber; number++) {
                        _prefabs.Insert(_randomMax, _addPrefabs[number]);

                    }

                    _switchNumber = 1;
                }
                break;

            case 1:

               
                if (_countTime >= _nextTime) {
                    
                    
                    for (int number = _prefabNumber; number < _addPrefabs.Count; number++) {
                        _prefabs.Insert(_randomMax, _addPrefabs[number]);
                        
                    }
                    

                    if (!_isOneShot) {
                        _isOneShot = true;
                        _audio.PlayOneShot(_warningSE);

                    }

                    StartCoroutine(_outLineObject.GetComponent<ProgressScript>().StartProgress());
                    StartCoroutine(HurryUpText());
                  
                    _isCameraSize = true;

                    _switchNumber = 2;
                }
                break;
            default:
                break;
        }
    }


    public void CameraHurryUp() {
        float timeRatio = _timer / _changeDuration;
        float targetY = _cameraTargetY;//cameraが向かうY座標
        Vector3 targetPosition = new Vector3(_startPosition.x, Mathf.Lerp(_startPosition.y, targetY, timeRatio), _startPosition.z);
        _camera.transform.position = targetPosition;
    }
    private IEnumerator HurryUpText() {
        yield return new WaitForSeconds(1);
        _cameraShake.StartCameraShake();
        _hurryUpText.SetActive(true);
        yield return new WaitForSeconds(3);
        _cameraShake.StopCameraShake();
        _hurryUpText.SetActive(false);
        _count._bgm.pitch = 1.4f;
    }
   

}

