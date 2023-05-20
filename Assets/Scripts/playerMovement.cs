using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float speed;
    float xInput, yInput;
    public GameObject imageGameOver;
    public TMP_Text scoreTextDes;

    void Start()
    {
        transform.position = new Vector2(0,-4);
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + speed*xInput, -2.4f, 2.4f), Mathf.Clamp(transform.position.y + speed*yInput, -4.5f, 2f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Time.timeScale = 0;
            Destroy(scoreTextDes);
            imageGameOver.SetActive(true);
        }
    }

    public void StartPlay()
    {
        SceneManager.LoadScene(1);
    }
}
