using System;
// using MovementCommands;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{

    public ControlMap control;
    private Vector2 _direction;
    public float speed;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    

    [SerializeField]
    LayerMask lmWalls;
    [SerializeField]
    float fJumpVelocity = 7;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDamping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float fCutJumpHeight = 0.5f;
    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;
    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;
    [SerializeField]
    float fHorizontalAcceleration = 1;

    [SerializeField] private LayerMask platformLayerMask;
    
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f,platformLayerMask);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void Update()
    {
        // Debug.Log(IsGrounded());
        Vector2 v2GroundedBoxCheckPosition = (Vector2)transform.position + new Vector2(0, -0.01f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);
        // bool bGrounded = Physics2D.OverlapBox(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, lmWalls);


        fGroundedRemember -= Time.deltaTime;

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }
        
        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            if (_rigidbody2D.velocity.y > 0)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * fCutJumpHeight);
            }
            // Debug.Log("jump");
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, fJumpVelocity);
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

    private void Awake()
    {
        // control.MovimentMap.Direction.performed += context => UpdateDirection(context.ReadValue<Vector2>());
        
        // InputManager.Instance.DirectionChangedEvent += OnDirectionChanged;
    }

    // private void UpdateDirection(Vector2 direction)
    // {
    //     _direction = direction;
    // }
    private void OnDisable()
    {
        // InputManager.Instance.DirectionChangedEvent -= OnDirectionChanged;
    }

    // private void OnDirectionChanged(Vector2 direction)
    // {
    //     _direction = direction;
    // }

}

