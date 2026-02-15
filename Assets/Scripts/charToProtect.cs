using UnityEngine;

public class charToProtect : Entity
{
    private Transform player;
    protected override void Awake()
    {
        base.Awake();
        player=FindFirstObjectByType<PlayerScript>().transform;
    }
    protected override void Update()
    {
        HandleFlip();
    }
    protected override void HandleFlip()
    {
        if(player==null){
            return;
        }
        if(player.transform.position.x>transform.position.x && !facingRight)
        {
            Flip();
        }
        else if(player.transform.position.x<transform.position.x && facingRight)
        {
            Flip();
        }
    }
    protected override void Die()
    {
        base.Die();
        UI.instance.enableGameOverUI();
    }
}
