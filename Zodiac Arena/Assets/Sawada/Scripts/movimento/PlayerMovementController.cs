using System;
// using MovementCommands;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    
    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    

    [SerializeField]
    float fJumpVelocity = 5;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDamping = 0.5f;
    [SerializeField]
    [Range(1, 5)]
    float fallGravity = 3f;
    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;
    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;
    [SerializeField]
    float fHorizontalAcceleration = 1;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f,groundLayerMask);
        // Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
    private bool IsWalled()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.left, 0.1f,wallLayerMask);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.right, 0.1f,wallLayerMask);
        Debug.Log(raycastHitLeft.collider);
        Debug.Log(raycastHitRight.collider);
        return raycastHitLeft.collider != null && raycastHitRight.collider != null && !IsGrounded();
    }

    private void Update()
    {

        fGroundedRemember -= Time.deltaTime;
        fJumpPressedRemember -= Time.deltaTime;
        
        // if (Input.GetButtonDown("Jump"))
        // {
        //     fJumpPressedRemember = fJumpPressedRememberTime;
        // }
        
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.gravityScale = fallGravity;
        }
        else
        {
            _rigidbody2D.gravityScale = 1;
        }
        
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            // Debug.Log("jump");
            // _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, fJumpVelocity);
            _rigidbody2D.AddForce(Vector2.up * fJumpVelocity, ForceMode2D.Impulse);
        }

        if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
        }
    }

    private void FixedUpdate()
    {
        float fHorizontalVelocity = _rigidbody2D.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        
        _rigidbody2D.velocity = new Vector2(fHorizontalVelocity, _rigidbody2D.velocity.y);
    }

}

