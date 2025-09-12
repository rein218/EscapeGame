using UnityEngine;

public class CharacterController : AbstractPausable
{
   [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private bool isTryingToMove;
    [SerializeField] private float _movementSpeed;

     private bool moveLeft = false;
    private bool moveRight = false;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
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
}
