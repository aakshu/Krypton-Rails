﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    private void OnTriggerEnter(Collider other)
    {
        print("player triggered");
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        print("Player Dying");
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() //string reference used
    {
        SceneManager.LoadScene(1);
    }
}
