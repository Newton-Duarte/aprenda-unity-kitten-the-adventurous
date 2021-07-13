using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    FadeController _fadeController;
    OptionsController _optionsController;

    private void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            startGame();
        }
    }

    internal void startGame()
    {
        _optionsController.StartCoroutine(_optionsController.changeMusic(_optionsController.startClip));
        _fadeController.startFade(2);
    }
}
