using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    public static bool isLoading;
    // Start is called before the first frame update
    void Start()
    {
        switch (ScenManager.scenCount)
        {
            case 0: SceneManager.LoadScene(0); isLoading = true; break;
            case 1: SceneManager.LoadScene(1); isLoading = true; break;
            case 3: SceneManager.LoadScene(3); isLoading = true; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
