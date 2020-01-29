using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlayEnemy :  EnemyController
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

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerAlvo.position, Mathf.Abs(speed * Time.deltaTime));
        }
        

    }

    private void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Attack"))
        {
            DamageEnemy();
            Debug.Log("Acertou");
        }
    }

     IEnumerator DamageEffect(){

        float actualSpeed = speed;
        sprite.color = Color.red;
        speed = speed * -1;
        rb2d.AddForce (new Vector2(0f, 200f));
        yield return new WaitForSeconds(0.2f);
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
