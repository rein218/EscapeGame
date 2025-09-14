using UnityEngine;
public class StaticObject : AbstractPausable
{
    [SerializeField] private SpriteRenderer _sprite;
    public override void Start()
    {
        base.Start();

    }

    public override void Pause()
    {
        if (_sprite == null) return;
       _sprite.color = new Color(0.33f, 0.45f, 0.55f);
    }
    public override void Resume()
    {
        if (_sprite == null) return;
        _sprite.color = new Color(1,1,1);
    }
}
