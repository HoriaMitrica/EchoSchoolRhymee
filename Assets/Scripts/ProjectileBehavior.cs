using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
  public float Speed = 4.5f;
   private void Update()
    {
        transform.position += -transform.right*Time.deltaTime * Speed;
void OnCollisionEnter2D (Collision2D collision)
{
    Destroy(gameObject);
}
    }
}
