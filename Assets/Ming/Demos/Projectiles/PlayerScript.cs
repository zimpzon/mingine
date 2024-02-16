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

    void ProjectileUpdater(ref MingProjectile projectile)
    {
        projectile.Position += projectile.Velocity * Time.deltaTime;
    }

    private void Awake()
    {
        _playerSpriteRenderer = MingGameObjects.FindByName(transform, "Body").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 moveVec = Vector3.zero;

        if (_input.IsActive(MingSimpleInputType.Left))
            moveVec += Vector3.left;

        if (_input.IsActive(MingSimpleInputType.Right))
            moveVec += Vector3.right;

        if (_input.IsActive(MingSimpleInputType.Up))
            moveVec += Vector3.up;

        if (_input.IsActive(MingSimpleInputType.Down))
            moveVec += Vector3.down;

        moveVec.Normalize();
        transform.position += moveVec * MingTime.DeltaTime * MoveSpeed;

        bool isMoving = moveVec.sqrMagnitude > 0;
        if (isMoving)
        {
            _playerSpriteRenderer.flipX = moveVec.x < 0;
        }

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
                count: 32,
                speed: 3,
                ProjectileManager,
                ProjectileUpdaters.BasicMove);
        }
    }
}
