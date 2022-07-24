using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyOnLoad : MonoBehaviour
{
   
    [SerializeField] bool disableOnStart;

    bool wasDisable;
    private void Awake()
    {

       
    }
    void Start()
    {
    
        // zmienia obiekt na taki, który nie niszczy siê wraz ze znian¹ sceny
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroyOnLoad>().Length; i++)
        {

            if (Object.FindObjectsOfType<DontDestroyOnLoad>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroyOnLoad>()[i].name == gameObject.name)
                    Destroy(gameObject);
            }


        }
       
        DontDestroyOnLoad(gameObject);


    }

    // Update is called once per fra
    void Update()
    {
        if (disableOnStart && !wasDisable)
        {
            wasDisable = true;
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (disableOnStart)
                gameObject.SetActive(true);
            SceneManager.LoadScene(0);
        }
    }
   


   
}
