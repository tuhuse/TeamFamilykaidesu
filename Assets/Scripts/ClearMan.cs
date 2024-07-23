using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearMan : MonoBehaviour {
    [SerializeField] private GameObject _cpuParent;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _gameClearUI;
    [SerializeField] private float _cameraRimit = default;
    [SerializeField] private float _cameraSizeAdjust = default;
    private List<GameObject> _frogs=new List<GameObject>();
    private List<Rigidbody2D> _frogsrb2d = new  List<Rigidbody2D>();
  
    [SerializeField] private List<GameObject> _anotherEnamys = new List<GameObject>();
    private float _fallMin = -60f;
    public int _switchNumber = 1;
    private float _sizeLimit = 20;
    private float _countTime = default;
    private float _time = 80;

    [SerializeField] private JoyStickGameClearSelect _joyStickGameClear = default;

    // Start is called before the first frame update
    void Start() {

     
        for (int number = 0; number < 3; number++) {
            _anotherEnamys.Add(_cpuParent.transform.GetChild(number).gameObject);
        }

    }

    // Update is called once per frame
    void Update() {
        //print(_anotherEnamys.Count);

        _countTime += Time.deltaTime;

        //�Q�[����ʂ��~�A�J�������Y�[���A�b�v�������UI��\��        
        switch (_switchNumber) {
            case 0:

                //�G�I�u�W�F�N�g�̓������z��̒��g��null��
                if (_anotherEnamys.Count == 0) {
                    _switchNumber = 2;
                } 
                else {
                    _switchNumber = 1;
                }
                break;
            case 1:
                if (_countTime >= _time) {
                    _cameraRimit = 80f;
                }
                //�G�I�u�W�F�N�g���擾
                foreach (GameObject arrayEnamy in _anotherEnamys) {
                    //�����������͉�ʒ[�ɂԂ���Ɣz�񂩂�폜
                    if (arrayEnamy.transform.position.y < _fallMin ||
                                arrayEnamy.transform.position.x < _camera.transform.position.x - _cameraRimit) {
                        _anotherEnamys.Remove(arrayEnamy);
                        arrayEnamy.SetActive(false);
                        _switchNumber = 0;
                        break;
                    }
                }
                break;
            case 2:

                _frogsrb2d[0].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[1].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[2].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[3].constraints = RigidbodyConstraints2D.FreezePosition;
                _switchNumber = 3;

                break;

            case 3:

                _camera.transform.position -= Vector3.down;

                _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust;

                if (_camera.GetComponent<Camera>().orthographicSize <= _sizeLimit) {
                    _switchNumber = 4;
                }

                break;
            case 4:

                _gameClearUI.GetComponent<Canvas>().enabled = true;
                _joyStickGameClear.enabled = true;



                break;
            default:
                break;
        }

    }

    public void ClearRetryButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //������x�����ꏊ���v���C
        }
    }


    public void ClearTitleButton() {
     
            SceneManager.LoadScene("TitleScene");
      
    }
    public void InFrogs(GameObject frog) 
    {
        _frogs.Add(frog);
        _frogsrb2d.Add(frog.GetComponent<Rigidbody2D>());
        
    }
}
