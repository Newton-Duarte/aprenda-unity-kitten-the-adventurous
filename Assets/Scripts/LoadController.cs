using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadController : MonoBehaviour
{
    FadeController _fadeController;
    OptionsController _optionsController;

    bool isVerified;

    // Start is called before the first frame update
    void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;

        _optionsController.musicSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVerified && !_optionsController.musicSource.isPlaying)
        {
            isVerified = true;
            _optionsController.StartCoroutine(_optionsController.changeMusic(_optionsController.gameplayClip));
            _fadeController.startFade(3);
        }
    }
}
