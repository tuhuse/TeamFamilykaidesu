using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    
   
    //起動するカエル
    [SerializeField] private GameObject[] _frog;
    [SerializeField] private GameObject _countDown;
    [SerializeField] private GameObject _stage;  
    [SerializeField] private GameObject _birdCanvas;
    [SerializeField] private GameObject _sneak = default;
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip[] _audioClip;
    public GameObject _player;
    public GameObject _cpu;
    [SerializeField] private GameObject _pauseManager;
    [SerializeField] private AudioManager _audiomanager;
    [SerializeField] private ClearMan _clearManScript;
     [SerializeField] private GameOverMan _gameoverManScript;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _frogcutin;
    ////[SerializeField] private GameObject[] _flogButtons; f
    // Start is called before the first frame update
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
      
        _audiosource.PlayOneShot(_audioClip[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
   
    public void GoTxt()//GoButtonを押したらメイン画面に行く
    {
        _countDown.SetActive(true);
        _birdCanvas.SetActive(true);       
        StartCoroutine(FrogInstantiate());
    }

    public void SummonSneak() {
        StartCoroutine(CutIn());       
    }

    private IEnumerator CutIn() {
        
        _audiosource.Stop();
        _frogcutin.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        
        _sneak.SetActive(true);
        _menu.SetActive(false);
        _stage.SetActive(true);
    }

    private IEnumerator FrogInstantiate() {
        
        yield return new WaitForSeconds(0.1f);
        _audiomanager.PlayAudio(3);
       
                _frog[0].SetActive(true);
                
                _clearManScript.InFrogs(_frog[0]);
                _gameoverManScript.InFrogs(_frog[0]);
                _player = _frog[0];

                _frog[1].SetActive(true);
                
                _gameoverManScript.InFrogs(_frog[1]);
                _clearManScript.InFrogs(_frog[1]);
                _cpu = _frog[1];

                _frog[2].SetActive(true);
               
                _gameoverManScript.InFrogs(_frog[2]);
                _clearManScript.InFrogs(_frog[2]);
                _cpu = _frog[2];

                _frog[3].SetActive(true);
               
                _gameoverManScript.InFrogs(_frog[3]);
                _clearManScript.InFrogs(_frog[3]);
                _cpu = _frog[3];

                _pauseManager.SetActive(true);
                     
    }
  
}
