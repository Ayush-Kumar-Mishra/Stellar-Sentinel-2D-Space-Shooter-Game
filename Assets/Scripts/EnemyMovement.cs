using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public GameObject enemyExplosionSound;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + 0,transform.position.y + speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            ScoreSystem.score++;
            var Clone = Instantiate(enemyExplosionSound, transform.position, transform.rotation);
            Destroy(Clone, 2f);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
