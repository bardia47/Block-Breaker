using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float nextVelocity;
    public float velocityIncreasePerPaddleTouch;
    public float initialVelocity = 270;

    private float maxVelocity;
    private bool correctVelocity = false;

    public int bouncesOnWall;
    public int maxBouncesBeforePush;

    private Rigidbody2D rb;

    private bool isYellow;

    private void Start()
    {
        isYellow = true;
        rb = GetComponent<Rigidbody2D>();
        FindObjectOfType<Paddle>().activeBall = this.transform;

        GetComponentInChildren<TrailRenderer>().emitting = false;
        maxVelocity = rb.velocity.magnitude;
    }

    private void Update()
    {
        if (this.transform.IsChildOf(FindObjectOfType<Paddle>().transform))
        {
            Vector2 pos = this.transform.position;
            pos.x = FindObjectOfType<Paddle>().transform.position.x;
            this.transform.position = pos;
        }
                
                }

    private void FixedUpdate()
    {
        if (!correctVelocity)
            return;
        if (rb.velocity.magnitude != maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }

    }

    public void Launch() {
        transform.SetParent(null);
         rb.AddForce(new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(0.7f, 1f)).normalized * initialVelocity);
        maxVelocity = rb.velocity.magnitude;
        GetComponentInChildren<TrailRenderer>().emitting = true;
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Brick"))
        {

            if (!correctVelocity)
                correctVelocity = true;

            rb.AddForce(rb.velocity.normalized);
            maxVelocity = rb.velocity.magnitude;
            bouncesOnWall = 0;
            other.collider.GetComponent<Brick>().Break();
            AudioManager.PlaySound(SoundClip.BRICKHIT);
        }
        if (other.collider.CompareTag("Bounds"))
        {
            bouncesOnWall++;
            AudioManager.PlaySound(SoundClip.PADDLEHIT,0.8f);

            if (bouncesOnWall >= maxBouncesBeforePush)
            {

                Vector2 force = rb.velocity;
                rb.velocity = Vector2.zero;
                float minX = -0.05f;
                float maxX = 0.05f;
                float minY = -0.05f;
                float maxY = 0.05f;
                Vector3 otherTransForm = other.transform.position;
                Vector3 ballTransForm = transform.position;
                if (otherTransForm.x > ballTransForm.x)
                    maxX = 0f;
                else
                    minX = 0f;
                if (otherTransForm.y > ballTransForm.y)
                    maxY = 0f;
                else
                    minY = 0f;


                rb.AddForce(new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)).normalized * initialVelocity);
                rb.AddForce(force * nextVelocity);
                GetComponentInChildren<TrailRenderer>().startColor = ChangeColor();

                bouncesOnWall = 0;
            }
        }
        if (other.collider.CompareTag("Player"))
        {
            nextVelocity += velocityIncreasePerPaddleTouch;
            rb.AddForce(rb.velocity.normalized * nextVelocity);
            maxVelocity = rb.velocity.magnitude;
            AudioManager.PlaySound(SoundClip.PADDLEHIT, 0.8f);

        }
    }

    private Color ChangeColor()
    {
        if (isYellow) {
            isYellow = false;
            return new Color(0.871f, 0.314f, 0.314f,0f);

        }
        isYellow = true;
        return new Color(0.871f, 0.871f, 0.314f, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("KillPlane"))
        {
            AudioManager.PlaySound(SoundClip.LIFELOST, 0.5f);

            FindObjectOfType<Paddle>().Health--;
            UIManager.instance.LoseLife();
            Destroy(this.gameObject);
        }
    }
}
