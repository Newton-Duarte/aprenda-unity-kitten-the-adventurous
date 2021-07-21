using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    internal FadeController _fadeController;
    internal OptionsController _optionsController;

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
}
