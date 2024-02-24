using Ming;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float MoveSpeed = 10;

    public MingSpriteAnimationFrames_IdleRun PlayerAnimationFrames;

    public MingProjectileManager ProjectileManager;
    public MingProjectileBlueprint PlayerBulletBlueprint;

    private IMingSimpleInput<MingSimpleInputType> _input = new MingDefaultSimpleInputMapper();
    private SpriteRenderer _playerSpriteRenderer;
    private Rigidbody2D _rigidbody;

    void ProjectileUpdater(ref MingProjectile projectile)
    {
        projectile.Position += projectile.Velocity * Time.deltaTime;
    }

    private void Awake()
    {
        _playerSpriteRenderer = MingGameObjects.FindByName(transform, "Sprite").GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveVec = Vector2.zero;

        if (_input.IsActive(MingSimpleInputType.Left))
            moveVec += Vector2.left;

        if (_input.IsActive(MingSimpleInputType.Right))
            moveVec += Vector2.right;

        if (_input.IsActive(MingSimpleInputType.Up))
            moveVec += Vector2.up;

        if (_input.IsActive(MingSimpleInputType.Down))
            moveVec += Vector2.down;

        moveVec.Normalize();
        Vector2 newPosition = _rigidbody.position + moveVec * MoveSpeed * Time.deltaTime;

        // https://docs.unity3d.com/2023.1/Documentation/ScriptReference/Rigidbody2D.Slide.html
        // unity 2023 and use slide?
        _rigidbody.MovePosition(newPosition);

        bool isMovingHorizontally = moveVec.x != 0;
        if (isMovingHorizontally)
        {
            _playerSpriteRenderer.flipX = moveVec.x < 0;
        }

        bool isMoving = moveVec != Vector2.zero;
        Sprite[] activeAnimation = isMoving ? PlayerAnimationFrames.Run : PlayerAnimationFrames.Idle;
        _playerSpriteRenderer.sprite = MingSimpleSpriteAnimator.GetAnimationSprite(activeAnimation, PlayerAnimationFrames.AnimationFramesPerSecond);

        if (Input.GetKeyDown(KeyCode.E))
        {
            //var projectile = new MingProjectile();
            //projectile.ApplyBlueprint(PlayerBulletBlueprint);
            //ProjectileManager.SpawnProjectile(ref projectile);

            ProjectileSpawners.SpawnCircle(
                PlayerBulletBlueprint,
                transform.position,
                radius: 1.0f,
                count: 200,
                speed: 3,
                ProjectileManager,
                ProjectileUpdaters.BasicMove);
        }
    }
}
