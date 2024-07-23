using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMoveScript : MonoBehaviour {
    private const float MAXSTRENGTH = 1f;
    private const string PROPNAME = "_MainTex";

    [SerializeField]
    private Vector2 _offsetSpeed;

    private Material _material;

    private void Start() {
        if (GetComponent<Image>() is Image i) {
            _material = i.material;
        }
    }

    private void Update() {
        if (_material) {
            // xとyの値が0 ～ 1でリピートするようにする
            float x = Mathf.Repeat(Time.time * _offsetSpeed.x, MAXSTRENGTH);
            float y = Mathf.Repeat(Time.time * _offsetSpeed.y, MAXSTRENGTH);
            Vector2 offset = new Vector2(x, y);
            _material.SetTextureOffset(PROPNAME, offset);
        }
    }

    private void OnDestroy() {
        // ゲームをやめた後にマテリアルのOffsetを戻しておく
        if (_material) {
            _material.SetTextureOffset(PROPNAME, Vector2.zero);
        }
    }
}
