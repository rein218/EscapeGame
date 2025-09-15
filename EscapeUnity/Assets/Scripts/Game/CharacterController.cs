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

    [SerializeField] private float _rotationForse = 500f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private float initialJumpVelocity;
    private float jumpTimer = 0f;
    private bool isJumping = false;

    private Vector2 startPosition;
    [Header("Fade Settings")]
    [SerializeField] private float _fadeSpeed = 0.5f;
    [SerializeField] private GameObject fadeMask;

    [Header("unlocks")]
    [SerializeField] private bool _isJumpUnlocked = false;

    public override void Start()
    {
        base.Start();
        initialJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * _jumpHeight);
        _basePos = transform.position;

        startPosition = transform.position;
        FadeOut();
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && _isJumpUnlocked)
        {
            isJumping = true;
            jumpTimer = 0f;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, initialJumpVelocity);
        }

        // Если прыжок активен и клавиша все еще нажата
        if (isJumping && Input.GetKey(KeyCode.Space))
        {
            jumpTimer += Time.deltaTime;

            // Если время прыжка истекло, прекращаем прыжок
            if (jumpTimer >= _jumpDuration)
            {
                isJumping = false;
            }
        }

        // Если клавиша прыжка отпущена, прекращаем прыжок
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
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
        if (isJumping)
        {
            // Рассчитываем силу, которую нужно приложить для поддержания прыжка
            // Сила уменьшается по мере приближения к концу прыжка
            float jumpProgress = jumpTimer / _jumpDuration;
            float jumpForceMultiplier = 1f - jumpProgress;

            // Применяем дополнительную силу для поддержания прыжка
            _rb.AddForce(Vector2.up * jumpForceMultiplier * initialJumpVelocity * 0.1f, ForceMode2D.Impulse);


            _rb.MoveRotation(_rb.rotation + _rotationForse * Time.fixedDeltaTime);
        }
    }
    // Визуализация области проверки земли в редакторе
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
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
