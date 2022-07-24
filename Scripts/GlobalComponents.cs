using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalComponents : MonoBehaviour
{
    public static GameObject inventory, fastAccess;
    [SerializeField] GameObject inv, fast;
   
    // Start is called before the first frame upda
    private void Awake()
    {
      
        fastAccess = fast;
        inventory = inv;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
