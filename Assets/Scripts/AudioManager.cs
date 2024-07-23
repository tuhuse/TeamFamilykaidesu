using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] AudioClip[] _audioClips;
    private AudioSource[] _audioSources;//âπê∫ÇÃçƒê∂Çä«óù
    void Start() {
        _audioSources = new AudioSource[_audioClips.Length];
        for (int i = 0; i < _audioClips.Length; i++) {
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
            _audioSources[i].clip = _audioClips[i];
            _audioSources[i].loop = false; //ÉãÅ[Évçƒê∂ÇÇ‚ÇﬂÇÈ
           
        }
    }

    // Update is called once per frame
    void Update() {

    }
    public void PlayAudio(int clipIndex) {
        if (clipIndex >= 0 && clipIndex < _audioClips.Length) {
            _audioSources[clipIndex].Play();

        }
    }
    public void StopAudio(int clipIndex) {
        if (clipIndex >= 0 && clipIndex < _audioSources.Length) {
            if (_audioSources[clipIndex].isPlaying) {
                _audioSources[clipIndex].Stop();
            }
        }
    }
}
