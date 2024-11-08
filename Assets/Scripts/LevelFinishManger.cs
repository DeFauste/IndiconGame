using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Assets.Scripts.Services;
using Zenject;

public class LevelFinishManger : MonoBehaviour
{
    ParticleSystem confetti;

    public AudioSource audioSource;
    public AudioClip collect;

    [SerializeField] private int levelNumber;
    [SerializeField] private GameObject _particle;

    private void OnEnable() => Collectible.OnCollected += OnCollect;
    private void OnDisable() => Collectible.OnCollected -= OnCollect;

    private LevelService _levelService;
    [Inject]
    public void Construct(LevelService levelService)
    {
        _levelService = levelService;
    }
    private void Start()
    {
        confetti = _particle.GetComponent<ParticleSystem>();
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        _levelService.CurrentLevelIsFinish();
        var level = _levelService.LoadNextLevel();
        
    }

    void OnCollect()
    {
        confetti.Play();
        audioSource.clip = collect;
        audioSource.Play();
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
