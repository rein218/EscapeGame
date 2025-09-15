using UnityEngine;

public class MovingPlatform : AbstractPausable
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _waitTime = 1f;
    
    private Vector3 targetPosition;
    private bool isWaiting = false;
    private bool isOnPause = false;
    private float waitTimer = 0f;

    public override void Start()
    {
        base.Start();
        targetPosition = _pointB.position;
    }

    public override void Pause()
    {
        base.Pause();
        isOnPause = true;
    }
    public override void Resume()
    {
        base.Resume();
        isOnPause = false;
    }

    public void Update()
    {
        if (isOnPause) return;
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= _waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;

                targetPosition = (targetPosition == (Vector3)_pointA.position) ?
                                 _pointB.position : _pointA.position;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position,
                                                targetPosition,
                                                _speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isWaiting = true;
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (_pointA != null && _pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_pointA.position, 0.5f);
            Gizmos.DrawWireSphere(_pointB.position, 0.5f);
            Gizmos.DrawLine(_pointA.position, _pointB.position);
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterController>(out var player))
        {
          //collision.transform.SetParent(transform);
            
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterController>(out var player))
        {
            //collision.transform.SetParent(null);
        }
    }
}
