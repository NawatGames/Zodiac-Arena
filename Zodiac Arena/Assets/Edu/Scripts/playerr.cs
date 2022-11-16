using System;
using System.Collections;
// using MovementCommands;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class playerr : MonoBehaviour
{
    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Collider2D lastCollision;

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

    [Header("Player Variables")]
    [SerializeField] [Range(0, 1)] public float crouchSizeMultiplier = 0.5f;

    [SerializeField] public float dodgeDelay = 1.5f;
    [SerializeField] public float dodgeDuration = 0.7f;

    [Header("Crouching Variables")]
    [SerializeField] private int dodgeCounter = 0;
    private Vector2 crouchSize;
    private Vector2 standingSize;

    [Header("Knockback Variables")]
    [SerializeField] private int kbIntensity;
    [SerializeField] private float kbEffectDuration;
    [SerializeField] private GameObject KnockbackRam;
    [SerializeField] private SpriteRenderer Warning;
    private bool knockbacked = false;

    private bool _canJump => _jumpBufferCounter > 0f && (_hangCounter > 0f || (_doubleJumpAvailable && doubleJump) || IsWalled());
    private bool canDodge = true;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        // standingSize = _boxCollider2D.size;
        standingSize = gameObject.transform.localScale;
        crouchSize = new Vector2(gameObject.transform.localScale.x, standingSize.y * crouchSizeMultiplier);
    }
    private void Update()
    {
        //jumping
        if (Input.GetButtonDown("Jump")) _jumpBufferCounter = jumpBufferTime;
        else _jumpBufferCounter -= Time.deltaTime;

        if (IsGrounded() || IsWalled())
        {
            _hangCounter = hangTime;
            _doubleJumpAvailable = true;
        }
        else
        {
            _hangCounter -= Time.deltaTime;
            _rigidbody2D.drag = airLinearDrag;
        }
        if (_canJump && !knockbacked) Jump();
        FallMultiplier();
        //dodging
        if (Input.GetKey(KeyCode.Q) && canDodge)
        {
            canDodge = false;
            StartCoroutine(Dodge());
            StartCoroutine(ResetDodge());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Danger"))
        {
            Debug.Log("Hit");
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
        if (col.CompareTag("Danger"))
        {
            Debug.Log("Hit");
        }
        if (col.CompareTag("Warning"))
            Warning.enabled = true;
        else if(col.CompareTag("Knockback"))
        {
            Vector2 kbOrigin = col.transform.position;
            if (kbOrigin.x == 0)
            {
                kbOrigin.x = 2 * (transform.position.x <= 0 ? 1 : -1);
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
        gameObject.transform.localScale = Input.GetKey(KeyCode.LeftControl) ? crouchSize : standingSize;
    }

    private IEnumerator Dodge()
    {
        transform.gameObject.tag = "dodging";
        yield return new WaitForSeconds(dodgeDuration);
        transform.gameObject.tag = "Player";
    }

    private IEnumerator ResetDodge()
    {
        yield return new WaitForSeconds(dodgeDelay);
        canDodge = true;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        // Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private bool IsWalled()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.left, 0.1f, wallLayerMask);
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.right, 0.1f, wallLayerMask);
        // Debug.Log(raycastHitLeft.collider);
        // Debug.Log(raycastHitRight.collider);
        return raycastHitRight.collider != null || raycastHitLeft.collider != null;
    }

    private void Jump()
    {
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
        float fHorizontalVelocity = _rigidbody2D.velocity.x;

        // fHorizontalVelocity += Input.GetAxisRaw("Horizontal");
        // if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
        //     fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        // else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
        //     fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDamping, Time.deltaTime * 10f);
        // else
        // fHorizontalVelocity *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity = 0;
        else
            fHorizontalVelocity = Input.GetAxisRaw("Horizontal") * speed;

        _rigidbody2D.velocity = new Vector2(fHorizontalVelocity, _rigidbody2D.velocity.y);
    }
    public void ApplyKnockBack(Vector2 direction, int intensity)
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(direction * intensity);
        knockbacked = true;
        StartCoroutine(Unknockback(kbEffectDuration));
    }
    private IEnumerator Unknockback (float duration)
    {
        yield return new WaitForSeconds(duration);
        knockbacked = false;
    }
}