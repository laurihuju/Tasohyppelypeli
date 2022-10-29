using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public LayerMask groundLayers;
    [SerializeField] private Transform groundCheckPointLeft;
    [SerializeField] private Transform groundCheckPointRight;

    private bool isGrounded;

    //Muutetaan lapsiobjektin kautta
    private sbyte facingDirection;
    private bool isMovementFrozen;

    [SerializeField] private float accelerationX;
    [SerializeField] private float decelerationX;
    [SerializeField] private float maxVelocityX;

    private Rigidbody2D rb2d;
    private Transform trans;
    private Animator animator;
    private MeleeCombatSystem combatSystem;

    void Awake()
    {
        isGrounded = false;
        isMovementFrozen = false;

        facingDirection = 1;

        rb2d = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        combatSystem = GetComponent<MeleeCombatSystem>();
    }

    public virtual void Update()
    {
        GroundCheck();
    }

    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckPointLeft.position, groundCheckPointRight.position, groundLayers);
    }

    public void Move(sbyte direction)
    {
        if ((rb2d.velocity.x > -maxVelocityX && direction == -1) || (rb2d.velocity.x < maxVelocityX && direction == 1))
        {
            rb2d.AddForce(new Vector2(accelerationX * direction, 0));
        }
        else
        {
            rb2d.velocity = new Vector2(maxVelocityX * direction, rb2d.velocity.y);
        }
    }

    public void StopMovement()
    {
        if (rb2d.velocity.x < 0)
        {
            if (rb2d.velocity.x + decelerationX > 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x + decelerationX, rb2d.velocity.y);
            }
        }
        else if (rb2d.velocity.x > 0)
        {
            if (rb2d.velocity.x - decelerationX < 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x - decelerationX, rb2d.velocity.y);
            }
        }
    }

    public void TurnObject(sbyte direction)
    {
        trans.localScale = new Vector3(Mathf.Abs(trans.localScale.x) * direction, trans.localScale.y, trans.localScale.z);
        facingDirection = direction;
    }

    public void TurnObject()
    {
        trans.localScale = new Vector3(trans.localScale.x * -1, trans.localScale.y, trans.localScale.z);
        facingDirection *= -1;
    }

    public void FreezeMovement()
    {
        isMovementFrozen = true;
    }

    public void UnfreezeMovement()
    {
        isMovementFrozen = false;
    }

    public bool IsMovementFrozen()
    {
        return isMovementFrozen;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public void SetGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    public sbyte GetFacingDirection()
    {
        return facingDirection;
    }

    public Rigidbody2D GetRigidBody()
    {
        return rb2d;
    }

    public Transform GetTransform()
    {
        return trans;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public MeleeCombatSystem GetCombatSystem()
    {
        return combatSystem;
    }

    public float GetDecelerationX()
    {
        return decelerationX;
    }

    public void SetDecelerationX(float deceleration)
    {
        this.decelerationX = deceleration;
    }

    public virtual bool IsPlayer()
    {
        return false;
    }
}
