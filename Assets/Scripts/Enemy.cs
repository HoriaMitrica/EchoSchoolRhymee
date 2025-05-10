using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 10;
    public int damageToPlayer = 1;
    public Transform enemyattackPos;
    public float enemyattackRange = 1f;
    private float Cooldown=3f;
    [SerializeField] private float _startCooldown;

    public LayerMask playerLayer;

    public float speed = 1;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    [SerializeField] private Animator boss;

    private static readonly int BossAttack = Animator.StringToHash("BossAttack");
    public static readonly int BossisHit = Animator.StringToHash("BossisHit");
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;
        _animator = GetComponent<Animator>();
        _animator.SetBool(BossAttack, false);
        _animator.SetBool(BossisHit, false);



    }

    void Update()
    {
        if (Cooldown <= 0)
        {
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(enemyattackPos.position, enemyattackRange, playerLayer);
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].GetComponent<PlayerCombat>().PlayerTakeDamage(damageToPlayer);
                _animator.SetTrigger("BossAttack");



            }

            Cooldown = _startCooldown;

        }
        else
        {
            Cooldown -= Time.deltaTime;
                                        //_animator.SetBool(BossAttack, false);

        }

    }
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        _animator.SetBool(BossisHit, true);

        if (enemyHealth <= 0)
        {

            Destroy(gameObject);
        }
    }
}
