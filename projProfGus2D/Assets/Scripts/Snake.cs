using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    public bool facingRight = true;

    public int health;
    public float speed;
   
    public Transform wallCheck;
    private bool tochedWall = false;
 

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {

        tochedWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (tochedWall)
        {
            Flip();
        }
        
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;

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
        yield return new WaitForSeconds(0.2f);
        speed = actualSpeed;
        sprite.color = Color.white;
    }

    void DamageEnemy()
    {
        health--;
        StartCoroutine(DamageEffect());


        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}
