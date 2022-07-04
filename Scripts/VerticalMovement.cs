using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
   public enum Movement { 
    Vertival,
    Horizontal

    }
    [SerializeField] float speed;
    [SerializeField] float maxTime;

    public Movement move;
    int directory=-1;
    float time;
    void Start()
    {
        time = maxTime;
    }
    private void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            directory *= -1;
            time = maxTime;
        }
        switch (move)
        {
            case Movement.Vertival: transform.Translate(Vector2.up * directory * speed * Time.deltaTime); break;
            case Movement.Horizontal: transform.Translate(Vector2.right * directory * speed * Time.deltaTime); break;
        }

     
    }

    // Update is called once per fram
    void Update()
    {
       
    }
}
