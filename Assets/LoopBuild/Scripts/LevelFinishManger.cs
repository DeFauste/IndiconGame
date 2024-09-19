using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LevelFinishManger : MonoBehaviour
{
    ParticleSystem confetti;

    public AudioSource audioSource;
    public AudioClip collect;

    [SerializeField] private int levelNumber;

    private void OnEnable() => Collectible.OnCollected += OnCollect;
    private void OnDisable() => Collectible.OnCollected -= OnCollect;

    private void Start()
    {
        confetti = GetComponent<ParticleSystem>();
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadSceneAsync(levelNumber);
    }

    void OnCollect()
    {
        confetti.Play();
        audioSource.clip = collect;
        audioSource.Play();
        UnlockNextLevel();
        StartCoroutine(StartLevel());
    }

    private void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("UnlockedLevelsNumber", 0))
        {
            Debug.Log("Save");

            PlayerPrefs.SetInt("UnlockedLevelsNumber", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }
}
