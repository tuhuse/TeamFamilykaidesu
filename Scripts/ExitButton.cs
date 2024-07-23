using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
   public void Onclick() 
   {
        
        Application.Quit();//ゲームプレイ終了
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (_animator != null) {
            _animator.SetBool("ExitButton", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_animator != null) {
            _animator.SetBool("ExitButton", false);
        }
    }

    public void EnableCanvasAndTriggerAnimation(Canvas canvas) {
        _audioSource.PlayOneShot(_audioClip);
        canvas.enabled = true;

        // 必要であれば、ここでアニメーションを強制的に再トリガーする
        if (_animator != null) {
            // 現在の状態をリセットまたは強制的に設定する
            _animator.SetBool("ExitButton", false);
        }
    }
}
