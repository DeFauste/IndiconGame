using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishManger : MonoBehaviour
{
ParticleSystem confetti;

public AudioSource audioSource;
public AudioClip collect;

private void OnEnable() => Collectible.OnCollected += OnCollect;
private void OnDisable() => Collectible.OnCollected -= OnCollect;

private void Start()
{
    confetti = GetComponent<ParticleSystem>();
}

void OnCollect()
{
    confetti.Play();
    audioSource.clip = collect;
    audioSource.Play();
}

}
