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
            _sprite.color = new Color(0, 0, 0);
            savedVelosity = _rb.linearVelocity;
            _rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            _sprite.color = new Color(255, 255, 255);
        }
        
    }
    public override void Resume()
    {
        base.Resume();
        _sprite.color = new Color(255, 255, 255);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.linearVelocity = savedVelosity;
    }
}
