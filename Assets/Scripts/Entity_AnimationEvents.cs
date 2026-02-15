using UnityEngine;

public class Enitiy_AnimationEvents : MonoBehaviour
{
    private Entity entity;
    void Awake()
    {
        entity=GetComponentInParent<Entity>();
    }
    public void damageTargets()=>entity.damageTarget();
    private void DisableJumpAndMovement()=> entity.enableJumpAndMove(false);
    private void EnableJumpAndMovement()=>entity.enableJumpAndMove(true);
}
