using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] SavableVector startingPosition = default;

    private float walkSpeed;
    private Vector2 moveInput;

    #region Properties
    public bool CanMove { get; set; } = true;
    #endregion

    #region Cached references
    private PlayerController myController;
    private Rigidbody2D myRigidbody;
    private PlayerAttack myAttack;
    private Animator animator;
    #endregion


    private void Awake()
    {
        SetReferences();
        SetValues();
        SetAnimation();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        if (CanMove && !myAttack.IsAttacking
            && moveInput != Vector2.zero && !myController.IsDead)
        {
            Move();
            UpdateAnimation();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void Move()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveInput * walkSpeed * Time.fixedDeltaTime);
    }

    private void SetAnimation()
    {
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
        animator.SetBool("isWalking", true);
    }

    private void SetReferences()
    {
        myController = GetComponent<PlayerController>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAttack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
    }

    private void SetValues()
    {
        transform.position = startingPosition.currentValue;
        walkSpeed = myController.myStats.GetSpeed;
    }

}
