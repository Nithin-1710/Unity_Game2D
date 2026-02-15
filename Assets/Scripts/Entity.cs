using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Collider2D col;
    protected SpriteRenderer sr;
    protected Animator anim;
    protected Rigidbody2D rb;
    [Header("Health")]
    [SerializeField]private int maxHealth=1;
    [SerializeField]private int currentHealth;
    [SerializeField]private Material damageMaterial;
    [SerializeField]private float damageDuration=.2f;
    private Coroutine damageFeedbackCoroutine;
    protected bool isDead=false;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    protected bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    protected int facingDir=1;
    protected bool facingRight=true;
    protected bool canMove=true;    
    protected virtual void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponentInChildren<Animator>();
        col=GetComponent<Collider2D>();
        sr=GetComponentInChildren<SpriteRenderer>();
        currentHealth=maxHealth;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimation();
        HandleFlip();

    }
    public virtual void enableJumpAndMove(bool enabel)
    {
        canMove=enabel;
    }
    protected virtual void HandleMovement()
    {
        
    }
    protected void HandleAnimation()
    {
        anim.SetFloat("xVelocity",rb.linearVelocity.x);
        anim.SetFloat("yVelocity",rb.linearVelocity.y);
        anim.SetBool("isGrounded",isGrounded);
    }
    protected virtual void HandleFlip()
    {
        if(rb.linearVelocity.x>0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocity.x<0 && facingRight)
        {
            Flip();
        }
    }
    protected virtual void HandleCollision()
    {
        isGrounded=Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
    }
    protected virtual void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
        }
    }
    public void damageTarget()
    {
        Collider2D[] enemyColliders=Physics2D.OverlapCircleAll(attackPoint.position,attackRadius,whatIsTarget);
        foreach(Collider2D enemy in enemyColliders)
        {
            Entity entityTarget=enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if(isDead) return;
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        anim.SetTrigger("onHit");
    }
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        anim.SetTrigger("Dead");
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject,3);
    }
    public void Flip()
    {
        transform.Rotate(0,180,0);
        facingRight=!facingRight;
        facingDir*=-1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,transform.position+new Vector3(0,-groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
