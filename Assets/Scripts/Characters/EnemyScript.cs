using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : Entity
{
    private bool playerDetected;
    [Header("Movement Details")]
    public float playerSpeed=3.5f;
    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }
    protected override void HandleAttack()
    {
        if(playerDetected)
            anim.SetTrigger("attack");
    }
    protected override void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity=new Vector2(facingDir*playerSpeed,rb.linearVelocity.y);
        else
            rb.linearVelocity=new Vector2(0,rb.linearVelocity.y);
    }
    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected=Physics2D.OverlapCircle(attackPoint.position,attackRadius,whatIsTarget);
    }
    protected override void Die()
    {
        base.Die();
        UI.instance.AddKillCount();
    }
}
