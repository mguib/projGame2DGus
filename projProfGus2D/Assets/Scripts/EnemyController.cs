using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float distanceAttack;
    public float speed;
    public int heat;
    protected bool isMoving = false;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Transform playerAlvo;
    protected SpriteRenderer sprite;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAlvo = GameObject.Find("Player").GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();

    }

    protected virtual void Update()
    {
        float distance = PlayerDistance();

        isMoving = (distance <= distanceAttack);

        if (isMoving)
        {
            if ((playerAlvo.position.x > transform.position.x && sprite.flipX) ||
                (playerAlvo.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();

            }
        }
    }


    protected float PlayerDistance()
    {
        return Vector2.Distance(playerAlvo.position, transform.position);
    }

    protected void Flip()
    {
        sprite.flipX = !sprite.flipX;
        speed *= -1;

    }


    
    private void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Attack"))
        {
            DamageEnemy();
            Debug.Log("Acertou essa desgraçaa");
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
        heat--;
        StartCoroutine(DamageEffect());

        if (heat < 1)
        {
            Destroy(gameObject);
        }
    }
}
