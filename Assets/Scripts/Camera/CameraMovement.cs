using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    void Start()
    {

    }


    void Update()
    {
        var position = player.transform.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
