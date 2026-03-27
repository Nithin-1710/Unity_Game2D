using UnityEngine;

public class PlayerScript : Entity
{
    [Header("Movement Details")]
    public float playerSpeed=3.5f;
    [SerializeField] private float jumpForce=8;
    private float xinput;
    public bool canJump=true;

    protected override void Update()
    {
        base.Update();
        HandleInput();
    }
        private void HandleInput()
    {
        xinput=Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if(Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }
    protected override void HandleMovement()
    {
        if(canMove)
            rb.linearVelocity=new Vector2(xinput*playerSpeed,rb.linearVelocity.y);
        else
            rb.linearVelocity=new Vector2(0,rb.linearVelocity.y);
    }
        private void Jump()
    {
        if (isGrounded && canJump )
        {
            rb.linearVelocity=new Vector2(rb.linearVelocity.x,jumpForce);
            SoundManager.Instance.PlaySound2D("Jump");
        }
    }
    public override void enableJumpAndMove(bool enabel)
    {
        base.enableJumpAndMove(enabel);
        canJump=enabel; 
    }
    protected override void Die()
    {
        base.Die();
        UI.instance.enableGameOverUI();
    }
}
