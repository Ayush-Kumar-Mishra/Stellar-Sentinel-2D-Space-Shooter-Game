using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + 0, transform.position.y + speed);
    }
}
