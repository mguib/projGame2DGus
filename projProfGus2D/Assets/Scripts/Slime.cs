using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyController
{
    public int vida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
         
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
         
    }

     private void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Attack"))
        {
            DamageEnemy();
        }
    }

    IEnumerator DamageEffect(){

        float actualSpeed = speed;
        sprite.color = Color.red;
        speed = speed * -1;
        rb2d.AddForce (new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.1f);
        speed = actualSpeed;
        sprite.color = Color.white;
    }

    void DamageEnemy()
    {
        vida--;
        StartCoroutine(DamageEffect());


        if (vida < 1)
        {
            Destroy(gameObject);
        }
    }
    
}
