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
            //�{�^���ƃt�F�[�h�A�E�g�̃A�j���[�V�������J�n
            _titleButton.gameObject.GetComponent<Animator>().enabled = true;

            _titlePanel.gameObject.GetComponent<Animator>().enabled = true;

            _isStart = false;
            StartCoroutine(TitleButtonStay());
            
        }
      

    }



    IEnumerator TitleButtonStay()
    {
        //�t�F�[�h�A�E�g�I����ɃV�[���ړ�
        yield return new WaitForSeconds(_stayTime);
        SceneManager.LoadScene("StageScene");
    }
}
