using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    internal FadeController _fadeController;
    internal OptionsController _optionsController;

    [Header("Collect Config.")]
    [SerializeField] internal GameObject getCoinPrefab;

    [Header("Damage Config.")]
    [SerializeField] internal GameObject hitPrefab;
    [SerializeField] internal int hammerDamage;
    [SerializeField] internal int ballDamage;
    [SerializeField] internal int hitBoxDamage;
    internal int coins;

    [Header("UI Config.")]
    [SerializeField] Text txtCoins;

    [Header("Audio Clips")]
    [SerializeField] AudioClip coinClip;

    void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
        loadSavedCoins();
    }

    internal void addCoins(int value)
    {
        playFX(coinClip);
        coins += value;
        txtCoins.text = $"x{coins}";
    }

    internal void playFX(AudioClip clip)
    {
        _optionsController.playFX(clip);
    }

    public IEnumerator loadLevelWithDelay(int level, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        _optionsController.StartCoroutine(_optionsController.changeMusic(_optionsController.startClip));
        _fadeController.startFade(level);
    }

    internal void loadSavedCoins()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");
        coins = savedCoins;
        txtCoins.text = $"x{coins}";
    }

    internal void saveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }

    internal void resetCoins()
    {
        PlayerPrefs.DeleteKey("Coins");
    }
}
