using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 4.5f;
    public float lifetime = 3f; // Destroy after 3 seconds
    private Vector2 direction;

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        direction=new Vector2(-1,0);
        //Destroy(gameObject, lifetime); // Auto-destroy after set time
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
