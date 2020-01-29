using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float espeed;
    public float timeDestroy;
    public int damage;



    // Start is called before the first frame update
    void Start(){
        Destroy(gameObject, timeDestroy);
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * espeed * Time. deltaTime);
        
        
    }

    private void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Enimy"))
        {
            
            Debug.Log("Acertou");
        }
    }

    

   
}
