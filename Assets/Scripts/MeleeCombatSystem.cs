using System.Collections;
using UnityEngine;

public class MeleeCombatSystem : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float whenGiveDamage;
    [SerializeField] private float whenUnfreezeMovement;
    [SerializeField] private LayerMask whatLayerToAttack;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float amountOfDamage;
    [SerializeField] private float knockbackAmount;
    [SerializeField] private float knockbackAmountY;

    private bool isAttacking;
    private Animator animator;
    ObjectController myController;

    private void Start()
    {
        isAttacking = false;
        animator = GetComponent<Animator>();
        myController = GetObjectController(this.gameObject);

        if (myController.IsPlayer())
        {
            amountOfDamage = GameController.playerDamage;
        }

        if(knockbackAmountY == 0)
        {
            knockbackAmountY = 100f;
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        myController.FreezeMovement();

        yield return new WaitForSeconds(whenGiveDamage);

        if (Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatLayerToAttack))
        {
            Collider2D[] attackTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatLayerToAttack);
            foreach(Collider2D targetCollider in attackTargets)
            {
                targetCollider.gameObject.GetComponent<HealthManager>().TakeDamage(amountOfDamage);
                StartCoroutine(KnockBack(targetCollider.gameObject));
            }
        }

        yield return new WaitForSeconds(whenUnfreezeMovement);

        myController.UnfreezeMovement();

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        animator.ResetTrigger("Attack");

    }

    private IEnumerator KnockBack(GameObject targetObject)
    {
        Rigidbody2D targetBody = targetObject.GetComponent<Rigidbody2D>();
        ObjectController targetController = GetObjectController(targetObject);

        if (targetBody != null && targetController != null)
        {
            float orgDeceleration = targetController.GetDecelerationX();
            targetController.SetDecelerationX(0.08f);

            targetController.FreezeMovement();

            targetBody.velocity = new Vector2(0, targetBody.velocity.y);

            targetBody.AddForce(new Vector2(myController.GetFacingDirection() * knockbackAmount, knockbackAmountY));

            yield return new WaitForFixedUpdate();

            int i = 0;
            while (targetObject.GetComponent<Rigidbody2D>() != null)
            {
                if ((targetBody.velocity.x > -0.1 && targetBody.velocity.x < 0.1) || i > 1000)
                {
                    targetController.UnfreezeMovement();
                    targetController.SetDecelerationX(orgDeceleration);
                    break;
                }
                i++;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private ObjectController GetObjectController(GameObject gameObject)
    {
        ObjectController controller = null;

        if (gameObject.GetComponent<EnemyController>() != null)
        {
            controller = gameObject.GetComponent<EnemyController>();
        }
        else if (gameObject.GetComponent<PlayerController>() != null)
        {
            controller = gameObject.GetComponent<PlayerController>();
        }

        return controller;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public Transform GetAttackPoint()
    {
        return attackPoint;
    }

    public float GetAttackRadius()
    {
        return attackRadius;
    }

    public LayerMask WhatLayerToAttack()
    {
        return whatLayerToAttack;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
