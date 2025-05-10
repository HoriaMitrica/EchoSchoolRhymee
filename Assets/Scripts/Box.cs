using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxhealth = 2;
    

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
    //         for (int i = 0; i < enemiesToDamage.Length; i++)
    //         {
    //             //Debug.Log("damage"+damage);
    //             enemiesToDamage[i].GetComponent<Box>().BoxTakeDamage(damage);
    //         }
    //     }
    // }
public void BoxTakeDamage(int damage)
{
    boxhealth -= damage;

    if (boxhealth <= 0)
    {
        Destroy(gameObject);
    }
}

}
