using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SfxAudio : MonoBehaviour
{
    [Tooltip("O(s) clipe(s) de áudio que vai(ão) tocar para essa ação específica")]
    public AudioClip[] audioClips;
    [MinMax(0.5f, 1.5f), Tooltip("A extensão da variação de Pitch"), Header("  ")]
    public MinMaxPair pitchRange;
    [MinMax(0.5f, 1.5f), Tooltip("A extensão da variação de Volume")]
    public MinMaxPair volumeRange;

    AudioSource _as;

    void Start()
    {
        AssignParent();

        _as = GetComponent<AudioSource>();
    }
    private void Update()
    {
#if UNITY_EDITOR
        AssignParent();
#endif
    }
    void AssignParent()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("AudioManager");
        Transform parent = transform.parent;

        if (parent == null || parent.gameObject != manager)
            transform.SetParent(manager.transform);
    }

    public void PlayAudio()
    {
       try
       {
            int min = 0;
            int max = audioClips.Length - 1;
            int i = UnityEngine.Random.Range(min, max);

            AudioClip clip = audioClips[i];

            _as.pitch = pitchRange.RandomValue;
            _as.volume = volumeRange.RandomValue;
            _as.PlayOneShot(clip);
       }
       catch(IndexOutOfRangeException e)
       {
           Debug.LogError(gameObject.name + " não foi atribuído no Audio Manager");
       }
    }
}
