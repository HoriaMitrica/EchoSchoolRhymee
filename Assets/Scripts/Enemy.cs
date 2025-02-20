using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 3;
    public int damageToPlayer = 1;
    public Transform enemyattackPos;
    public float enemyattackRange = 2f;
    private float Cooldown;
   [SerializeField] private float _startCooldown;

    public LayerMask playerLayer;

    public float speed = 1;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.freezeRotation = true;
    }

    void Update()
    {
        if (Cooldown <= 0)
        {
                Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(enemyattackPos.position, enemyattackRange, playerLayer);
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    playerToDamage[i].GetComponent<PlayerCombat>().PlayerTakeDamage(damageToPlayer);

                }

                Cooldown = _startCooldown;
            }
            else
            {
                Cooldown -= Time.deltaTime;
            }

        }
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        //Debug.Log("Damage Taken");
        //Debug.Log(damage);
        if (enemyHealth <= 0)
        {

            Destroy(gameObject);
            //Debug.Log("Enemy Died");
        }
    }
}
