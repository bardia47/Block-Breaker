using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Transform activeBall;
    public GameObject ballPrefab;

    public float moveSpeed;
    private float horizontal = 0f;
    private Rigidbody2D rb;
    [SerializeField]
    private int health;

    public int Health
    {
        get { return health; }
        set { 
            health = value;
            HandleHealth();
        }
    }

    private void HandleHealth()
    {
        if (health <= 0)
        {
            health = 0;
            UIManager.instance.TriggerGameOver();
            //trigger game over
        }
        else
            SpawnBall();

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = 3;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space))
            if (activeBall.IsChildOf(this.transform))
                activeBall.GetComponent<Ball>().Launch();

    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, 0f);
    }

    public void SpawnBall() {
        GameObject g = Instantiate(ballPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f),Quaternion.identity);
        g.transform.SetParent(this.transform);

    }
}
