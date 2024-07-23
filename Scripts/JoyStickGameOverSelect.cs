using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameOverSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameOverText = new List<GameObject>(); //0にgo,1にreturnのテキストを入れる
    [SerializeField] GameObject _pauseManager = default; //canvasmanagerを入れる
    private int _selectButton = 2; //今見ているボタン
    public GameObject _preButton = default;　//ひとつ前に選択していたボタン

    private bool _isSelect;

    private bool _isFarstSelect = false;

    private bool _isGameOver = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update() {

        //上矢印を押したら
        if (Input.GetAxis("ArrowButtonVartical") > 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;

            //一回でもボタンを選択していたら
            if (_preButton != null) {
                //見ていたボタンの色を薄くする
                _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            //次のボタンを見る準備
            _selectButton++;

            //上のボタンを見ていたら
            if (_selectButton >= 2) {
                //下のボタンを見る
                _selectButton = 0;
            }

            //一個前のボタンとして入れる
            _preButton = _gameOverText[_selectButton];

            //見ているボタンの色を濃くする
            _gameOverText[_selectButton].GetComponent<Image>().color = new Color(1, 1, 1, 1f);

        }

        //下矢印を押した場合
        if (Input.GetAxis("ArrowButtonVartical") < 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;

            //一回でもボタンを選択していたら
            if (_preButton != null) {
                //見ていたボタンの色を薄くする
                _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

            //次のボタンを見る準備
            _selectButton--;

            //下のボタンを見ていたら
            if (_selectButton <= -1) {
                //上のボタンを見ていたら
                _selectButton = 1;
            }

            //一個前のボタンとして入れる
            _preButton = _gameOverText[_selectButton];

            //見ているボタンの色を濃くする
            _gameOverText[_selectButton].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        }

        //矢印ボタンを離したら
        if (Input.GetAxis("ArrowButtonVartical") == 0) {
            //また選択できるようにする
            _isSelect = false;
        }

        //Aボタンを押したら
        if (Input.GetButton("Submit") && _isFarstSelect) {
            _isFarstSelect = false;
            if (_selectButton == 0) //returnを見ていた場合
            {
                //タイトルに戻る

                _pauseManager.GetComponent<GameOverMan>().GameOverTitleButton();
            } else //returnを見ていた場合
              {

                //ゲームを終了する
                _gameOverText[1].GetComponent<ExitButton>().Onclick();
            }
        }
    }
}