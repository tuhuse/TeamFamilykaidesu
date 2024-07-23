using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameOverSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameOverText = new List<GameObject>(); //0��go,1��return�̃e�L�X�g������
    [SerializeField] GameObject _pauseManager = default; //canvasmanager������
    private int _selectButton = 2; //�����Ă���{�^��
    public GameObject _preButton = default;�@//�ЂƂO�ɑI�����Ă����{�^��

    private bool _isSelect;

    private bool _isFarstSelect = false;

    private bool _isGameOver = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update() {

        //�������������
        if (Input.GetAxis("ArrowButtonVartical") > 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;

            //���ł��{�^����I�����Ă�����
            if (_preButton != null) {
                //���Ă����{�^���̐F�𔖂�����
                _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            //���̃{�^�������鏀��
            _selectButton++;

            //��̃{�^�������Ă�����
            if (_selectButton >= 2) {
                //���̃{�^��������
                _selectButton = 0;
            }

            //��O�̃{�^���Ƃ��ē����
            _preButton = _gameOverText[_selectButton];

            //���Ă���{�^���̐F��Z������
            _gameOverText[_selectButton].GetComponent<Image>().color = new Color(1, 1, 1, 1f);

        }

        //�������������ꍇ
        if (Input.GetAxis("ArrowButtonVartical") < 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;

            //���ł��{�^����I�����Ă�����
            if (_preButton != null) {
                //���Ă����{�^���̐F�𔖂�����
                _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            //���̃{�^�������鏀��
            _selectButton--;

            //���̃{�^�������Ă�����
            if (_selectButton <= -1) {
                //��̃{�^�������Ă�����
                _selectButton = 1;
            }

            //��O�̃{�^���Ƃ��ē����
            _preButton = _gameOverText[_selectButton];

            //���Ă���{�^���̐F��Z������
            _gameOverText[_selectButton].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        }

        //���{�^���𗣂�����
        if (Input.GetAxis("ArrowButtonVartical") == 0) {
            //�܂��I���ł���悤�ɂ���
            _isSelect = false;
        }

        //A�{�^������������
        if (Input.GetButton("Submit") && _isFarstSelect) {
            _isFarstSelect = false;
            if (_selectButton == 0) //return�����Ă����ꍇ
            {
                //�^�C�g���ɖ߂�

                _pauseManager.GetComponent<GameOverMan>().GameOverTitleButton();
            } else //return�����Ă����ꍇ
              {

                //�Q�[�����I������
                _gameOverText[1].GetComponent<ExitButton>().Onclick();
            }
        }
    }
}