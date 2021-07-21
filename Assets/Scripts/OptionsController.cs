using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionsController : MonoBehaviour
{
    [Header("UI Config.")]
    [SerializeField] Text musicVolumeText;
    [SerializeField] Text fxVolumeText;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider fxSlider;

    [Header("Panel Config.")]
    [SerializeField] GameObject optionsPanel;

    [Header("Audio Config.")]
    [SerializeField] internal AudioSource musicSource;
    [SerializeField] internal AudioSource fxSource;

    [Header("Audio Clips")]
    [SerializeField] internal AudioClip titleClip;
    [SerializeField] internal AudioClip startClip;
    [SerializeField] internal AudioClip gameplayClip;
    [SerializeField] internal AudioClip pauseClip;
    [SerializeField] internal AudioClip unpauseClip;

    string _firstPlaythrough = "FirstPlaythrough";
    string _musicVolume = "MusicVolume";
    string _fxVolume = "FXVolume";
    

    // Start is called before the first frame update
    void Start()
    {
        initializePrefs();
        StartCoroutine(changeMusic(titleClip));
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            toggleOptions();
        }
    }

    void toggleOptions()
    {
        if (optionsPanel.activeInHierarchy)
        {
            musicSource.Play();
            fxSource.PlayOneShot(pauseClip);
        }
        else
        {
            musicSource.Pause();
            fxSource.PlayOneShot(unpauseClip);
        }

        Time.timeScale = optionsPanel.activeInHierarchy ? 1 : 0;
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }

    void initializePrefs()
    {
        if (PlayerPrefs.GetInt(_firstPlaythrough) == 0)
        {
            PlayerPrefs.SetInt(_firstPlaythrough, 1);
            PlayerPrefs.SetFloat(_musicVolume, 1f);
            PlayerPrefs.SetFloat(_fxVolume, 0.8f);
        }

        float musicVolume = PlayerPrefs.GetFloat(_musicVolume);
        float fxVolume = PlayerPrefs.GetFloat(_fxVolume);

        musicSource.volume = musicVolume;
        fxSource.volume = fxVolume;

        musicSlider.value = musicVolume;
        fxSlider.value = fxVolume;

        musicVolumeText.text = Mathf.Round(musicVolume * 100).ToString();
        fxVolumeText.text = Mathf.Round(fxVolume * 100).ToString();
    }

    public IEnumerator changeMusic(AudioClip clip, bool loop = true)
    {
        float currentVolume = musicSource.volume;

        for (float v = currentVolume; v > 0; v -= 0.01f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.pitch = 1;
        musicSource.Play();

        for (float v = 0; v < currentVolume; v += 0.01f)
        {
            musicSource.volume = v;
            yield return new WaitForEndOfFrame();
        }
    }

    public void changeMusicVolume()
    {
        float volume = musicSlider.value;

        musicSource.volume = volume;
        musicVolumeText.text = Mathf.Round(volume * 100).ToString();
        PlayerPrefs.SetFloat(_musicVolume, volume);
    }

    public void changeFXVolume()
    {
        float volume = fxSlider.value;

        fxSource.volume = volume;
        fxVolumeText.text = Mathf.Round(volume * 100).ToString();
        PlayerPrefs.SetFloat(_fxVolume, volume);
    }

    public void playFX(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }
}
