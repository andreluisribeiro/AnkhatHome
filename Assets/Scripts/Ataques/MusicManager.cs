using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public struct PairClip {
        [SerializeField]
        public string name;
        [SerializeField]
        public AudioClip clip;
    }
    public PairClip[] clips;

    public AudioSource source;

    private void Start() {
        this.source = this.GetComponent<AudioSource>();
    }

    public void Play(string name) {
        foreach(PairClip e in clips) {
            if(e.name == name) {
                source.clip = e.clip;
                source.Play();
            }
        }
    }
}
