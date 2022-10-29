using UnityEngine;

public class EnemyController : ObjectController
{
    private float myWidth;

    void Start()
    {
        myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        if (!GameController.isGamePaused)
        {
            //Liikkuminen
            if (!IsMovementFrozen())
            {
                Vector2 lineCastPos = new Vector2(GetTransform().position.x + myWidth * GetFacingDirection(), GetTransform().position.y);

                if (Physics2D.Linecast(lineCastPos, new Vector2(lineCastPos.x + 0.1f * GetFacingDirection(), lineCastPos.y), groundLayers) || !Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, groundLayers))
                {
                    TurnObject();
                }

                Move(GetFacingDirection());

                if (GetAnimator().GetBool("Idle"))
                {
                    GetAnimator().SetBool("Idle", false);
                }
            }

            //Hyökkäys
            if (CanAttack())
            {
                StartCoroutine(GetCombatSystem().Attack());
            }
        } else
        {
            if (!GetAnimator().GetBool("Idle"))
            {
                GetAnimator().SetBool("Idle", true);
            }
        }
    }

    private bool CanAttack()
    {
        if(Physics2D.OverlapCircle(GetCombatSystem().GetAttackPoint().position, GetCombatSystem().GetAttackRadius(), GetCombatSystem().WhatLayerToAttack()) && !GetCombatSystem().IsAttacking())
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            
        }
    }
}
