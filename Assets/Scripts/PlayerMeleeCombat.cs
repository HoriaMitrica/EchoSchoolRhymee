using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    private float Cooldown = 2f;
    [SerializeField] private float _startCooldown;
    public int damage = 1;
    public int _lives = 3;
    private Animator _animator;
    [SerializeField] private Animator Player;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int isDead = Animator.StringToHash("isDead");





    void Start()
    {
        damage = 1;

        _animator = GetComponent<Animator>();
        _animator.SetBool(Attack, false);
        _animator.SetBool(isHit, false);

    }
    void Update()
    {
        if (Cooldown <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    _animator.SetBool(Attack,true);

                }
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);

                    enemiesToDamage[i].GetComponent<Box>().BoxTakeDamage(damage);
                }

            }
            _animator.SetBool(Attack, false);



            Cooldown = _startCooldown;
        }
        else
        {
            Cooldown -= Time.deltaTime;
        }
    }

    public void PlayerTakeDamage(int damageToPlayer)
    {
        _lives -= 1;
        //Debug.Log("takendamage");
        _animator.SetBool(isHit,true);
        if (_lives <= 0)
        {
            
            _animator.SetBool(isDead, true);

        }

    }

}



