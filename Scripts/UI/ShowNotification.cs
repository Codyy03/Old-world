using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNotification : MonoBehaviour
{
    [SerializeField] GameObject notification;
    [HideInInspector] public float distance;

    public float distanceToShow;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
   
    }

    // Update is called once per fram
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (distance <= distanceToShow)
            notification.SetActive(true);
        else notification.SetActive(false);
    }
}
