using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip test;

    [SerializeField] private Button[] buttons;

    [SerializeField] private int unlockedLevelsNumber = 1;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            if (i <= unlockedLevelsNumber)
            {
                buttons[i].interactable = true;
            }
        }
    }

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void StartLevel(int levelNumber)
    {
        SceneManager.LoadSceneAsync(levelNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value)*20);
    }

    public void SetSFXVolume()
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(soundSlider.value)*20);
    }

    public void TestAudioClipPlay()
    {
        audioSource.clip = test;
        audioSource.Play();
    }
}
