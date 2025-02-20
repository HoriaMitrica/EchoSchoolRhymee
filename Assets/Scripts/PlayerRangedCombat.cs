using System.Security.Cryptography;
using UnityEngine;

public class PlayerRangedCombat : MonoBehaviour
{
    public ProjectileBehavior ProjectilePrefab;
    public Transform LaunchOffset;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire3"))
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }
}
