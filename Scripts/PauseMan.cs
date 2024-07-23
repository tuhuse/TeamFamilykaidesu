using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMan : MonoBehaviour {
    [SerializeField] private GameObject _pauseUI;
    private int _switchNumber = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        switch (_switchNumber) {
            case 1:
                if (Input.GetKeyDown(KeyCode.T)) {
                    Time.timeScale = 0f;
                    _pauseUI.GetComponent<Canvas>().enabled = true;
                    _switchNumber = 2;
                }

                break;
            case 2:

                if (Input.GetKeyUp(KeyCode.T)) {
                    _switchNumber = 3;
                }
                break;

            case 3:


                if (Input.GetKeyDown(KeyCode.T)) {
                    _pauseUI.GetComponent<Canvas>().enabled = false;
                    Time.timeScale = 1f;
                    _switchNumber = 1;
                }

                break;
            default:
                break;
        }
    }

    public void PauseRetryButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //もう一度同じ場所をプレイ
        }
    }


    public void PauseTitleButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //ステージセレクトへ移動
        }
    }
}
