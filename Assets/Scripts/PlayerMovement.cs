using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float jumpForce = 8f;
    private Rigidbody2D _rigidbody2D;
    private float _horizontal;
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private bool _isGrounded = true;
    private static readonly int isMoving = Animator.StringToHash("isMoving");
    private static readonly int isJumping = Animator.StringToHash("isJumping");



    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;
        _animator = GetComponent<Animator>();
        _animator.SetBool(isJumping, false);
    }

    void Update()
    {
        // Handle movement input
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Jump input handled here to prevent missed inputs
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
            _animator.SetBool(isJumping, true);
        } 
         _animator.SetBool(isJumping, !_isGrounded);
    }

    void FixedUpdate()
    {
        // Apply movement
        _rigidbody2D.linearVelocity = new Vector2(_horizontal * Speed, _rigidbody2D.linearVelocity.y);
        if (_horizontal != 0)
        {
            _animator.SetBool(isMoving, true);
        }
        else
        {
            _animator.SetBool(isMoving, false);

        }
        if (_horizontal < 0)
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = playerScale.x*1;
        }

    }

    void Jump()
    {
        _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
        _isGrounded = false; 
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}