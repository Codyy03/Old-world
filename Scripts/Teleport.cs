using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform placeToTeleport,player;
    bool canBeTeleport;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeTeleport && Input.GetKeyDown(KeyCode.E))
            player.transform.position = placeToTeleport.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canBeTeleport = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeTeleport = false;
    }
}
