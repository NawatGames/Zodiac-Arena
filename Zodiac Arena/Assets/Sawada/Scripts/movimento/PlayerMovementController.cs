using System;
using System.Collections;
// using MovementCommands;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    
    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Collider2D lastCollision;
    private Animator _animator;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    
    [Header("Run Variables")]
    // [SerializeField]
    // [Range(0, 1)]
    // public float horizontalDamping = 0.5f;
    [SerializeField] public float speed = 7;
    
    [Header("Jump Variables")]
    [SerializeField] public float jumpVelocity = 15;
    [SerializeField] [Range(1, 5)] public float fallGravityMultiplier = 4;
    [SerializeField] [Range(0, 1)] public float jumpCutMultiplier = 0.3f;
    [SerializeField] [Range(0.01f, 10)] public float jumpCut = 3f;
    [SerializeField] [Range(1, 5)] public float airLinearDrag = 3f;
    [SerializeField] private bool doubleJump = true;
    private bool _doubleJumpAvailable;
    
    [Header("Wall Variables")]
    [SerializeField] [Range(0, 5)] public float walledVelocity = 3;

    [Header("Buffer Variables")]
    private float _jumpBufferCounter = 0;
    [SerializeField] public float jumpBufferTime = 0.1f;
    private float _hangCounter = 0;
    [SerializeField] public float hangTime = 0.1f;
    private float _walledJumpCounter = 0;
    [SerializeField] public float walledJumpTime = 0.1f;

    [Header("Player Variables")]
    [SerializeField] [Range(0, 1)] public float crouchSizeMultiplier = 0.5f;

    [SerializeField] public float dodgeDelay = 1.5f;
    [SerializeField] public float dodgeDuration = 0.7f;

    [Header("Crouching Variables")] 
    [SerializeField] private int dodgeCounter = 0;
    private Vector2 crouchSize;
    private Vector2 standingSize;

    [Header("Knockback Variables")]
    [SerializeField] private float kbEffectDuration; //1
    [SerializeField] private int kbForce; //600
    [SerializeField] private GameObject KnockbackRam;
    [SerializeField] private SpriteRenderer Warning;
    private bool knockbacked = false;

    [Header("Death Variables")]
    [SerializeField] private Death_Effect deathScript;
    private bool dead = false;

    private bool _canJump => _jumpBufferCounter > 0f && (_hangCounter > 0f || (_doubleJumpAvailable && doubleJump) || IsWalled());
    private bool canDodge = true;
    private bool isFacingLeft;
    private bool isDodging;
    private bool isJumping;
    private bool isRunning;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        // standingSize = _boxCollider2D.size;
        standingSize = gameObject.transform.localScale;
        crouchSize = new Vector2(gameObject.transform.localScale.x,standingSize.y * crouchSizeMultiplier);
    }
    private void Update()
    {
        //jumping
        if (!dead && !PauseMenu.paused)
            if(Input.GetButtonDown("Jump"))
                _jumpBufferCounter = jumpBufferTime;
            else _jumpBufferCounter -= Time.deltaTime;

        if (IsWalled())
        {
            if (!isDodging) _animator.Play(isFacingLeft ? "Slide_Left" : "Slide_Right");
            _walledJumpCounter = walledJumpTime;
        }
        else _walledJumpCounter -= Time.deltaTime;
        
        if (IsGrounded() || IsWalled())
        {
            if (_jumpBufferCounter < 0 && _rigidbody2D.velocity.y == 0)
            {
                isJumping = false;
                if (!isDodging && !isRunning)
                    _animator.Play(isFacingLeft? "Player_Idle_Left" : "Player_Idle_Right");
            }
            _hangCounter = hangTime;
            _doubleJumpAvailable = true;
        }
        else
        {
            if(!isDodging)
                if(_walledJumpCounter > 0)
                    _animator.Play(isFacingLeft ? "Walled_Jump_Left":"Walled_Jump_Right");
                else    
                    _animator.Play(isFacingLeft ? "Jump_Left":"Jump_Right");
            _hangCounter -= Time.deltaTime;
            _rigidbody2D.drag = airLinearDrag;
        }
        if(_canJump && !knockbacked) Jump();
        FallMultiplier();
        //dodging
        if (!dead && !PauseMenu.paused && Input.GetKey(KeyCode.LeftShift) && canDodge)
        {
            canDodge = false;
            StartCoroutine(Dodge());
            StartCoroutine(ResetDodge());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Danger") && !dead)
        {
            if (gameObject.CompareTag("dodging"))
            {
                if (col.collider != lastCollision)
                {
                    lastCollision = col.collider;
                    dodgeCounter++;
                }
            }
            else
            {
                //Debug.Log("Hit");
                Die();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (transform.gameObject.CompareTag("dodging") && other.gameObject.CompareTag("Danger") && other != lastCollision)
        {
            lastCollision = other;
            dodgeCounter++;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Danger") && !dead)
        {
            if (gameObject.CompareTag("dodging"))
            {
                if (col != lastCollision)
                {
                    lastCollision = col;
                    dodgeCounter++;
                }
            }
            else
            {
                //Debug.Log("Hit");
                Die();
            }
        }

        if (col.CompareTag("Warning"))
            Warning.enabled = true;
        else if (col.CompareTag("Knockback"))
        {
            Vector2 kbOrigin = col.transform.position;
            if (kbOrigin.x == 0)
            {
                kbOrigin.x = transform.position.x <= 0 ? 2 : -2;
            }
            else
            {
                kbOrigin.x += 0.3f * kbOrigin.x;
                kbOrigin.y = transform.position.y; // se y > altura do chao, kb.y pode ser um pouco mais baixo p mandar mais p cima
            }
            GameObject obj = Instantiate(KnockbackRam, kbOrigin, transform.rotation);
            obj.GetComponent<AriesKnockbackRam>().player = transform;
            obj.GetComponent<AriesKnockbackRam>().playerScript = this;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Warning"))
            Warning.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!knockbacked)
        {
            Run();
        }
        //crouching
        gameObject.transform.localScale = !dead && !PauseMenu.paused && Input.GetKey(KeyCode.LeftControl) ? crouchSize : standingSize;
    }
    
    private IEnumerator Dodge()
    {
        _animator.Play("Dodge");
        isDodging = true;
        // gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        transform.gameObject.tag = "dodging";
        yield return new WaitForSeconds(dodgeDuration);
        transform.gameObject.tag = "Player";
        // gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        isDodging = false;
    }
    
    private IEnumerator ResetDodge()
    {
        yield return new WaitForSeconds(dodgeDelay);
        canDodge = true;
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
        // Debug.Log(raycastHitLeft.collider);
        // Debug.Log(raycastHitRight.collider);
        return raycastHitRight.collider != null || raycastHitLeft.collider != null;
    }

    private void Jump()
    {
        isJumping = true;
        if (_jumpBufferCounter < 0f || _hangCounter < 0f) _doubleJumpAvailable = false;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        _hangCounter = 0f;
        _jumpBufferCounter = 0f;
    }

    private void FallMultiplier()
    {
        if (_rigidbody2D.velocity.y < jumpCut)
        {
            _rigidbody2D.gravityScale = fallGravityMultiplier;
            if (IsWalled()) _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -walledVelocity);
        }
        else if (_rigidbody2D.velocity.y > 0.01 && !Input.GetButton("Jump") && !knockbacked)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * jumpCutMultiplier);
        else
            _rigidbody2D.gravityScale = 1;
    }
    
    private void Run()
    {
        if (!dead && !PauseMenu.paused && _rigidbody2D.velocity.x != 0 && Input.GetAxisRaw("Horizontal") < 0)
            isFacingLeft = true;
        else if (!dead && !PauseMenu.paused && _rigidbody2D.velocity.x != 0 && Input.GetAxisRaw("Horizontal") > 0)
            isFacingLeft = false;
        isRunning = _rigidbody2D.velocity.x != 0;
        if (isRunning && !isJumping && !isDodging)
        {
            _animator.Play(isFacingLeft? "Player_Running_Left":"Player_Running_Right");    
        }
        float fHorizontalVelocity = _rigidbody2D.velocity.x;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity = 0;
        else
            fHorizontalVelocity = Input.GetAxisRaw("Horizontal") * speed;
        if(!dead)
            _rigidbody2D.velocity = new Vector2(fHorizontalVelocity, _rigidbody2D.velocity.y);
    }
    public void ApplyKnockBack(Vector2 direction) // o script da KbRam chama essa função
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(direction * kbForce);
        knockbacked = true;
        StartCoroutine(Unknockback(kbEffectDuration));
    }
    private IEnumerator Unknockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        knockbacked = false;
    }
    private void Die()
    {
        dead = true;
        deathScript.DeathEffects();
    }
}

