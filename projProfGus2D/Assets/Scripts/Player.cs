﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public float    speed; //Variavel de velocidade
    public int      jumpForce; //Variavel de controle da força do pulo
    public int      health;
    public Transform groundCheck;

    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;
    //private Transform trans;

    private SpriteRenderer sprite;
    private Rigidbody2D     rb2d;
    private Animator       anim;

    public float attackRate;
    public Transform spawnAttack;
    public GameObject attackPrefab;
    
    private float nextAttack;





    // Start is called before the first frame update
    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //trans = GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update(){

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded){
            jumping = true;
        }

        SetAnimations();

        if (Input.GetButton("Fire1") && Time.time > nextAttack)
        {
            
            Attack();

        }
    }

    private void FixedUpdate(){
         
        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);

        if ((move < 0f && facingRight) || (move >0f && !facingRight)){
            Flip();

        }

        if (jumping){
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;


        }

        

    }

    public void Flip(){
        facingRight = !facingRight;
        //trans.localScale = new Vector3()
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
    }


    void SetAnimations(){

        anim.SetFloat("VelY", rb2d.velocity.y);
        anim.SetBool("JumpFall", !grounded);
        anim.SetBool("Walk", rb2d.velocity.x != 0f && grounded);


    }

    void Attack() {
        anim.SetTrigger("Punch");

        nextAttack = Time.time + attackRate;

        GameObject cloneAttack = Instantiate(attackPrefab, spawnAttack.position, spawnAttack.rotation);

        if (!facingRight)
        {
            cloneAttack.transform.eulerAngles = new Vector3(180, 0, 180);
        }

    }

    IEnumerator DamageEffect()
    {
        //criar efeito de camera;

        for (float i = 0; i < 1; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);

        }

        invunerable = false;

    }

    public void DamagePlayer()
    {

        if (!invunerable)
        {
            invunerable = true;
            health--;
            StartCoroutine(DamageEffect());

            if (health < 1)
            {
                Debug.Log("Morreu");
            }
        }

        
    }

    

}
