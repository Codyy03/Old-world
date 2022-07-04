using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScenManager : MonoBehaviour
{
    [SerializeField] GameObject[] objectToChangePositionOnLoad;
    [SerializeField] Vector2[] newPosition;
    [SerializeField] Vector2[] positionInCity;

    [SerializeField] GameObject[] backgrounds;
   
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(0);
        SelectBackground(backgrounds[0]);
    }


    public void LoadDesert()
    {
        SceneManager.LoadScene(1);
     
        SelectBackground(backgrounds[1]);
    }


    private void OnLevelWasLoaded(int level)
    {
       
        for (int i = 0; i < objectToChangePositionOnLoad.Length; i++)
        {
            if (level == 0)
                objectToChangePositionOnLoad[i].transform.position = positionInCity[i];
            else
            objectToChangePositionOnLoad[i].transform.position = newPosition[i];
        }
    }
}
