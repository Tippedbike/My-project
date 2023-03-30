
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
     bool broken = true;

    Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

     void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        if(!broken)
        {
            return;
        }
        
     
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
             animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);

            if(!broken)
            {
                return;
            }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);
        }
       
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Dance");
    }
   
}