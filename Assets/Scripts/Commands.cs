using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commands : MonoBehaviour
{
    FadeController _fadeController;
    OptionsController _optionsController;

    LoadXMLFile _loadXMLFile;

    [SerializeField] Text txtOptions;
    [SerializeField] Text txtStart;

    private void Start()
    {
        _fadeController = FindObjectOfType(typeof(FadeController)) as FadeController;
        _optionsController = FindObjectOfType(typeof(OptionsController)) as OptionsController;
        _loadXMLFile = FindObjectOfType(typeof(LoadXMLFile)) as LoadXMLFile;

        loadTitleTexts();
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

    void loadTitleTexts()
    {
        txtOptions.text = _loadXMLFile.titleInterface[0];
        txtStart.text = _loadXMLFile.titleInterface[1];
    }

    public void changeLanguage(string language)
    {
        PlayerPrefs.SetString("language", language);

        _loadXMLFile.loadXMLData();

        loadTitleTexts();
    }
}
