using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToGothicvania : MonoBehaviour
{
    public enum Place {
     Gothicvania,
     Greenland
    }
    public Place place;
    ScenManager scenManager;
    // Start is called before the first frame update
    void Start()
    {
        scenManager = GameObject.Find("Game Manager").GetComponent<ScenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch (place)
            {
                case Place.Gothicvania: scenManager.SetPositionToGothicvania(); scenManager.LoadGothicvania(); break;
                case Place.Greenland: GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(372.25f, 2.47f); scenManager.LoadGreenLand(); break;
            }
        }
    }
}
