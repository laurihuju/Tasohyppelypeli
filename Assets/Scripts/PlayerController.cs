using UnityEngine;

public class PlayerController : ObjectController
{
    [SerializeField] private GameObject pausePanel;

    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode attack;
    public KeyCode pause;
    public float jumpForce;
    public int lowJumpMultiplier;

    private bool wasGroundedInPreviousTime;

    private void Start()
    {
        wasGroundedInPreviousTime = false;
    }

    public override void Update()
    {
        GroundCheck();

        //Pelin pysäyttäminen ja pysäytyksen poistaminen
        if (Input.GetKeyDown(pause))
        {
            if (GameController.isGamePaused)
            {
                GameController.isGamePaused = false;
                pausePanel.SetActive(false);
            } else
            {
                GameController.isGamePaused = true;
                pausePanel.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        if (!GameController.isGamePaused)
        {
            MovePlayer();
        } else
        {
            StopMovement();

            if(GetAnimator().GetFloat("Speed") != 0)
            {
                GetAnimator().SetFloat("Speed", 0);
            }
        }
    }

    private void MovePlayer()
    {
        //Vaakatasossa liikkuminen
        GetAnimator().SetFloat("Speed", Mathf.Abs(GetRigidBody().velocity.x));

        if (Input.GetKey(moveLeft) && !IsMovementFrozen())
        {
            TurnObject(-1);

            Move(-1);
        }
        else if (Input.GetKey(moveRight) && !IsMovementFrozen())
        {
            TurnObject(1);

            Move(1);
        }
        else
        {
            StopMovement();
        }

        if(IsGrounded() && !wasGroundedInPreviousTime)
        {
            AudioManager.instance.Play("Jumpland");
        }

        if (!IsGrounded())
        {
            wasGroundedInPreviousTime = false;
        } else
        {
            wasGroundedInPreviousTime = true;
        }

        //Hyppy
        if (Input.GetKey(jump) && IsGrounded())
        {
            AudioManager.instance.Play("Jump");

            GetAnimator().SetBool("isJumping", true);

            GetRigidBody().AddForce(new Vector2(0, jumpForce));

            SetGrounded(false);

        } else if (!Input.GetKey(jump) && GetRigidBody().velocity.y > 0)
        {
            GetRigidBody().AddForce(new Vector2(0, -lowJumpMultiplier));
        }

        if(IsGrounded())
        {
            GetAnimator().SetBool("isJumping", false);
        }

        //Hyökkäys
        if (Input.GetKey(attack) && !GetCombatSystem().IsAttacking())
        {
            StartCoroutine(GetCombatSystem().Attack());
        }
    }

    public override bool IsPlayer()
    {
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            GetComponent<HealthManager>().TakeDamage(0.2f);
        }
    }
}
