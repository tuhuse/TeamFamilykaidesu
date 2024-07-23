using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakAnim : MonoBehaviour
{
    private Animator _sneakAnim = default;

    private bool _isStartPositionMoveOut = false;
    private bool _isPositionMoveOut = false;
    private bool _isPositionMoveIn = false;

    private float _positionX = default;

    private const float STARTPOSITIONMOVEOUTX = 0.15f;

    private const float POSITIONMOVEOUTX = 0.25f;
    private const float POSITIONMOVEINX = 0.35f;

    

    private const float WAITSTARTINTIMIDATION = 3f;
    private const float WAITSTARTSCREENOUT = 1f;

    private const float OUTSTOPPOSITION = 154f;
    private const float INSTOPPOSITION = 109.5f;

    private const float TIMEDELTATIMEMULTIPLE = 1000f;

    [SerializeField] GameObject _camera = default;
    [SerializeField] SelectCharacter _selectScript = default;

    // Start is called before the first frame update
    void Start()
    {
        _sneakAnim = GetComponent<Animator>();
        _sneakAnim.SetBool("Intimidation", true);
        _positionX = this.transform.position.x;
        StartCoroutine(StartIntimidation());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPositionMoveOut) 
        {
            //�ւ��J�����O���肬��܂Ŏ����Ă���
            ScreenOut();
        }

        if (_isPositionMoveIn) 
        {
            //�J�����̒��ɓ����Ă���
            ScreenIn();
        
        }

        if (_isStartPositionMoveOut) 
        {
            //��ԍŏ��̈Њd�s��
            StartScreenOUT();
        }
    }

    public void Attack() 
    {
        //�J�G����H�ׂ�
        _sneakAnim.SetBool("ScreenIn", true);
        _isPositionMoveIn = true;
    }

    private IEnumerator StartIntimidation() 
    {
        //�Њd�s��
        yield return new WaitForSeconds(WAITSTARTINTIMIDATION);
           
            _sneakAnim.SetBool("Intimidation", false);
        yield return new WaitForSeconds(WAITSTARTSCREENOUT);
        _isStartPositionMoveOut = true;
    }

    private void StartScreenOUT() 
    {
        //��ԍŏ��ɂ������Ɖ�ʊO�ɍs��
        if (this.transform.position.x >= _camera.transform.position.x-OUTSTOPPOSITION) 
        {
            this.transform.position -= new Vector3(STARTPOSITIONMOVEOUTX, 0, 0)*Time.deltaTime* TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _sneakAnim.SetBool("ScreenIn", false);
            _isPositionMoveOut = false;
            _isStartPositionMoveOut = false;
            _selectScript.GoTxt();
        }
    }


    private void ScreenOut() 
    {
        //��ʊO�ɏo�Ă���
        if (this.transform.position.x >= _camera.transform.position.x - OUTSTOPPOSITION)
        {
            this.transform.position -= new Vector3(POSITIONMOVEOUTX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _sneakAnim.SetBool("ScreenIn", false);
            _isPositionMoveOut = false;
        }
    }

    private void ScreenIn() 
    {
        //��ʓ��ɓ���
        if (this.transform.position.x <= _camera.transform.position.x-INSTOPPOSITION)
        {
            this.transform.position += new Vector3(POSITIONMOVEINX, 0, 0) * Time.deltaTime * TIMEDELTATIMEMULTIPLE;
        } 
        else 
        {
            _isPositionMoveIn = false;
            _isPositionMoveOut = true;
        }
    }
}
