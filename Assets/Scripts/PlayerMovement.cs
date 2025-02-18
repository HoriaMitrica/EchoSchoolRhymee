using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Test = 1;
    public float Speed = 2;
    public float jumpForce = 5;
    private Rigidbody2D _rigidbody2D;

    private float _horizontal;
    private Collider2D _collision;
    
    private bool _isGrounded=true;
    


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;
    }

    void FixedUpdate()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        
        if (_horizontal != 0)
        {
            _rigidbody2D.linearVelocity = new Vector2(_horizontal * Speed, _rigidbody2D.linearVelocity.y);

        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            Jump();

        }

       
    }
     void Jump()
        {

            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
            _isGrounded=false;
        }
    private void OnTriggerEnter2D(Collider2D _collision)
    {
       
    if (_collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D _collision)
    {
        
    if (_collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
        
    }
}
