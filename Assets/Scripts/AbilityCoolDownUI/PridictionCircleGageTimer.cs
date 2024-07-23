using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PridictionCircleGageTimer : MonoBehaviour {
    [SerializeField] private float _abilityCooldownTimer;
    private float _abilityMeasureTimer;
    [SerializeField] private Image _circleGauge;

    private bool _isCooldownActive = false;

    // Start is called before the first frame update
    void Start() {
        _abilityMeasureTimer = 0;
        _circleGauge.fillAmount = 1f; // 初期値を設定
    }

    // Update is called once per frame
    void Update() {
        if (_isCooldownActive) {
            _abilityMeasureTimer -= Time.deltaTime;
            float fillValue = 1 - (_abilityMeasureTimer / _abilityCooldownTimer);
            _circleGauge.fillAmount = Mathf.Clamp01(fillValue);

            if (_abilityMeasureTimer <= 0) {
                _isCooldownActive = false;
                _circleGauge.fillAmount = 1f; // クールダウン後にゲージを満タンにする
            }
        }
    }

    // クールダウンを開始するメソッド
    public void PridictionUIStartCooldown() {
        _abilityMeasureTimer = _abilityCooldownTimer;
        _circleGauge.fillAmount = 0f; // ゲージを一気に減少させる
        _isCooldownActive = true;
    }

    // クールダウンを停止するメソッド
    public void PridictionUIStopCooldown() {
        _isCooldownActive = false;
        _circleGauge.fillAmount = 1f; // クールダウンが手動で停止された場合にゲージを満タンにする
    }
    public void PridictionCoolDownFloat(float cooldown) {
        _abilityCooldownTimer = cooldown;
    }
}
