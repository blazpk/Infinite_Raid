using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPanel : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sound;

    private void Start()
    {
        bgm.Stop();
        sound.Play();
    }
}
