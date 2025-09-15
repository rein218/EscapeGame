using UnityEngine;
using DG.Tweening;
using System.Collections;


public class CharacterController : AbstractPausable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool _isTryingToMove;
    [SerializeField] private Vector3 _basePos;
    [Header("Horizontal Movement")]
    [SerializeField] private float _movementSpeed;
    private bool moveLeft = false;
    private bool moveRight = false;

    [Header("Jump Settings")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpspeed; 
    [SerializeField] private LayerMask whatisground; 
    [SerializeField] private float groundcheckradius = 0.2f;
    [SerializeField] private bool isGrounded;

    private Vector2 startPosition;

    [Header("Coyote Time")]
    public float coyotetime = .2f; 
    public float coyotecounter; 

    [Header("Minimum Distance of Jump Detection")]
    public float jumpbufferlength = .1f; 
    private float jumpbuffercount; 

    [Header("Fade Settings")]
    [SerializeField] private float _fadeSpeed = 0.5f;
    [SerializeField] private GameObject fadeMask;

    [Header("unlocks")]
    [SerializeField] private bool _isJumpUnlocked = false;

    public override void Start()
    {
        base.Start();
        _basePos = transform.position;

        startPosition = transform.position;
        FadeOut();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundcheckradius, whatisground); 

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft = true;
            moveRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight = true;
            moveLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            moveLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            moveRight = false;
        }
        if (!_isJumpUnlocked) return;
  
        if (jumpbuffercount >= 0f && coyotecounter > 0f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpspeed);
            jumpbuffercount = 0f;
        }
        
        if (isGrounded)
        {
            coyotecounter = coyotetime;
        }
        else
        {
            coyotecounter -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpbuffercount = jumpbufferlength;
        }
        else
        {
            jumpbuffercount -= Time.deltaTime;
        }

        

        if (Input.GetKeyUp(KeyCode.Space) && _rb.linearVelocity.y > 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * .5f); 
        }
    }

    void FixedUpdate()
    {
        if (moveLeft)
        {
            _rb.linearVelocity = new Vector2(-_movementSpeed, _rb.linearVelocity.y);
        }
        else if (moveRight)
        {
            _rb.linearVelocity = new Vector2(_movementSpeed, _rb.linearVelocity.y);
        }
        else
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        }
    }

    // Визуализация области проверки земли в редакторе
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundcheckradius);
        }
    }

    public void ReturnToStart()
    {
        transform.position = _basePos;
    }

    public void PauseInstance()
    {
        PauseManager.Instance.Pause();
    }
    public void Death()
    {
        transform.position = startPosition;
    }

    public void FadeOut()
    {
        fadeMask.transform.localScale = new Vector3(0, 0, 0);
        fadeMask.transform.DOScale(new Vector3(20f, 20f, 1f), _fadeSpeed);
    }

    public void FadeIn()
    {
        fadeMask.transform.DOScale(new Vector3(0f, 0f, 1f), _fadeSpeed);
    }
    
      public void EnableJump()
    {
        _isJumpUnlocked = true;
    }
}
