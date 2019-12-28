﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlayEnemy :  EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerAlvo.position, Mathf.Abs(speed * Time.deltaTime));
        }
        

    }
}
