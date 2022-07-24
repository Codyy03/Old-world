using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options,autros;
    [SerializeField] TextMeshProUGUI playTimeText;
    public Slider slider, soundsSlider;
    public AudioMixer musicMixer, soundsMixer;

    int minuty;
    GameManager gameManger;
    // Start is called before the first frame update
    void Start()
    {
        gameManger = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per fr
    void Update()
    {
        minuty = (int)gameManger.playTime / 60;
        if(minuty>=1)
        playTimeText.text = "Czas spêdzony w grze: "+ minuty +" minut";
    }

    public void Return()
    {
        options.SetActive(false);
        gameObject.SetActive(false);
        if(autros!=null)
        autros.SetActive(false);
        Time.timeScale = 1f;
    }

  
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetLevel(float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    public void SetLevelSounds(float volume)
    {
        soundsMixer.SetFloat("SoundsVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SoundsVolume", volume);
        PlayerPrefs.Save();

    }

    public void SetResolution()
    {
        string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (index)
        {
            case "0": Screen.SetResolution(1152, 648, true); break;
            case "1": Screen.SetResolution(1280, 768, true); break;
            case "2": Screen.SetResolution(1360, 796, true); break;
            case "3": Screen.SetResolution(1440, 900, true); break;
            case "4": Screen.SetResolution(1600, 900, true); break;
            case "5": Screen.SetResolution(1680, 1050, true); break;
            case "6": Screen.SetResolution(1980, 1080, true); break;
        }


    }

}
