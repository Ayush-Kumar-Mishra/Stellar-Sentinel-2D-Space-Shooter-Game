using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    private Player _player;
    private AudioSource _audioSource;

    void Start()
    {
        transform.position = new Vector3(Random.Range(-10,10), 8, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source of blast is NULL.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float posY = transform.position.y;

        if (posY <= -6)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject);
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }

            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,0.3f);
        }
    }
}
