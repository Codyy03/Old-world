using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UB.Simple2dWeatherEffects.Standard;
public class ScenManager : MonoBehaviour
{
    
    public static int scenCount;
   
    [SerializeField] GameObject[] objectToChangePositionOnLoad;
    [SerializeField] Vector2[] newPosition;
    [SerializeField] Vector2 newPlayerPositionInCementary, newPlayerPositionInGreenLand, newPlayerPositionInGothicvania;
    [SerializeField] Vector2[] positionInCity;

    [SerializeField] GameObject[] backgrounds;
    [SerializeField] AudioClip cityMusic, desertMusic, cementaryMusic, greenlandMusic, gothicvaniaMusic;

    AudioSource audioSource;
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        scenCount = SceneManager.GetActiveScene().buildIndex;
    }
    
    void SelectBackground(GameObject background)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }
        background.SetActive(true);
    }

    public void LoadFirstCity()
    {
     SetNewSceneVariables(cityMusic, 0, 0, false);
    }


    public void LoadDesert()
    {
     SetNewSceneVariables(desertMusic, 1, 1,false);
    }

    public void LoadCementary()
    {
      SetNewSceneVariables(cementaryMusic, 3, 2,true);

    }
    public void LoadGreenLand()
    {
        SetNewSceneVariables(greenlandMusic, 4, 3, false);

    }
    public void LoadGothicvania()
    {
        SetNewSceneVariables(gothicvaniaMusic, 5, 4, false);

    }

    void SetNewSceneVariables(AudioClip music, int sceneCount, int backgroundCount, bool eneableFog)
    {
        audioSource.clip = music;
        audioSource.Play();
        SceneManager.LoadScene(sceneCount);
        SelectBackground(backgrounds[backgroundCount]);
        GameObject.Find("Camera").GetComponent<D2FogsPE>().enabled = eneableFog;
    }

    public void SetPositionToCity()
    {
        for (int i = 0; i < objectToChangePositionOnLoad.Length; i++)
            objectToChangePositionOnLoad[i].transform.position = positionInCity[i];
    }
   public void SetPotionToDesert()
    {
        for (int i = 0; i < objectToChangePositionOnLoad.Length; i++)
            objectToChangePositionOnLoad[i].transform.position = newPosition[i];
    }
    public void SetPositionToCementary()
    {
        objectToChangePositionOnLoad[0].transform.position = newPlayerPositionInCementary;
    }
    public void SetPositionToGreenLand()
    {
        objectToChangePositionOnLoad[0].transform.position = newPlayerPositionInGreenLand;
    }
    public void SetPositionToGothicvania()
    {
        objectToChangePositionOnLoad[0].transform.position = newPlayerPositionInGothicvania;
    }
}
