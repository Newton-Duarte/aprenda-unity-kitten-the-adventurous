using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    internal FadeController _fadeController;

    [Header("Damage Config.")]
    [SerializeField] internal GameObject hitPrefab;
    [SerializeField] internal int hammerDamage;
    [SerializeField] internal int ballDamage;
    [SerializeField] internal int hitBoxDamage;
    internal int coins;

    [Header("UI Config.")]
    [SerializeField] Text txtCoins;

    void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
    }

    internal void addCoins(int value)
    {
        coins += value;
        txtCoins.text = $"x{coins}";
    }
}
