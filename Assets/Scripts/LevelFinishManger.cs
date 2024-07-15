using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishManger : MonoBehaviour
{
ParticleSystem confetti;

private void OnEnable() => Collectible.OnCollected += OnCollect;
private void OnDisable() => Collectible.OnCollected -= OnCollect;

private void Start()
{
    confetti = GetComponent<ParticleSystem>();
}

void OnCollect()
{
    confetti.Play();
}

}
