﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float espeed;
    public float timeDestroy;



    // Start is called before the first frame update
    void Start(){
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * espeed * Time. deltaTime);
        

    }
}
