using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneMove : MonoBehaviour
{
    [SerializeField] private float _stayTime = default;
    [SerializeField] private GameObject _titleButton;
    [SerializeField] private GameObject _titlePanel;
    private bool _isStart = true;
    //[SerializeField] private GameObject _selectParent;
    //[SerializeField] private GameObject _titleParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        if (!_isStart)
        {
            return;
        }
        if (Input.anyKeyDown) {
            //ボタンとフェードアウトのアニメーションを開始
            _titleButton.gameObject.GetComponent<Animator>().enabled = true;

            _titlePanel.gameObject.GetComponent<Animator>().enabled = true;

            _isStart = false;
            StartCoroutine(TitleButtonStay());
            
        }
      

    }



    IEnumerator TitleButtonStay()
    {
        //フェードアウト終了後にシーン移動
        yield return new WaitForSeconds(_stayTime);
        SceneManager.LoadScene("StageScene");
    }
}
