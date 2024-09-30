using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScreen : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Slider slider;
    public float volume;
    private void Start()
    {
        volume = 0.5f;
        slider.value = volume;
        backgroundMusic.volume = volume;
    }
    public void VolumeSetting()
    {
        volume = slider.value;
        backgroundMusic.volume = volume;
    }
}
