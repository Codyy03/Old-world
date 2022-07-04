using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] bool leftSide;
    [SerializeField] float maxTime, speed;

    private float time;
    int counter = 0, direction=1;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        time = maxTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        time -= Time.deltaTime;
        transform.Translate(Vector2.right*direction * speed * Time.deltaTime);
        if (time <= 0)
        {
            //jezeli czas sie skonczy zmien kierunek
            counter++;
            time = maxTime;
            if(counter==1)
            { 
                direction = -1;
            spriteRenderer.flipX = !leftSide;

            }
            else if(counter==2)
            {
                direction = 1;
                spriteRenderer.flipX = leftSide;
                counter = 0;
            }
        }

    }
}
