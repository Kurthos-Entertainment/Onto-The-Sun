using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public string Name;

    public void SetLevel(float value)
    {
        AudioMixer.SetFloat(Name, Mathf.Log10(Mathf.Clamp(value, 0.00001f, 1)) * 20);
    }
}