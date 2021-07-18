using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{
    FadeController _fadeController;
    OptionsController _optionsController;
    LoadXMLFile _loadXMLFile;

    [SerializeField] Text txtStage;

    bool isVerified;

    // Start is called before the first frame update
    void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
        _loadXMLFile = FindObjectOfType(typeof(LoadXMLFile)) as LoadXMLFile;

        _optionsController.musicSource.loop = false;

        txtStage.text = _loadXMLFile.stageName[0];
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
