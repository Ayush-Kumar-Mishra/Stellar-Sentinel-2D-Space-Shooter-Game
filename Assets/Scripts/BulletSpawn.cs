using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public AudioSource shootAudio;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            var Clone = Instantiate(bullet, transform.position, Quaternion.identity);
            shootAudio.Play();
            Destroy(Clone, 3f);
        }
    }
    
}
