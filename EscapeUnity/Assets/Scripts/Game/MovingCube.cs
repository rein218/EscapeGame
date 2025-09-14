using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingCube : AbstractPausable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _pausable;
    private Vector2 savedVelosity;
    public override void Start()
    {
        base.Start();
    }

    public override void Pause()
    {
        base.Pause();
        if (_pausable)
        {
            _sprite.color = new Color(0.33f, 0.45f, 0.55f);
            savedVelosity = _rb.linearVelocity;
            _rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            _sprite.color = new Color(0.33f, 0.45f, 0.55f);
        }
        
    }
    public override void Resume()
    {
        base.Resume();
        _sprite.color = new Color(1,1,1,1);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = savedVelosity;
    }
}
