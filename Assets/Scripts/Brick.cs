using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Brick : MonoBehaviour
{
    private int health;
    private ParticleSystem p;


    private void Start()
    {
        p = GetComponentInChildren<ParticleSystem>();
    }
    public int Health
    {
        get { return health; }
        set { health = value; HandleHealth(); }
    }

    

    public int points = 100;
    public SpriteRenderer sr;
    public SpriteRenderer shadow;

    private bool isBroken = false;
    private void HandleHealth()
    {
        if (isBroken)
            return;
        if (health == 1)
            this.sr.sprite = BrickManager.instance.redBrick;

        if (health <= 0)
        {
            isBroken = true;
            GetComponent<Collider2D>().enabled = false;
            health = 0;
            DestructBrick();
        }
    }
    public void Break()
    {
        var main = p.main;
        main.startColor = health == 2 ? Color.yellow : Color.red;
        p.Play();
        Health--;
        UIManager.instance.IncreaseScore(points);
    }

    private void DestructBrick()
    {
        BrickManager.instance.bricksLeft--;
        BrickManager.instance.CheckForNewLevel();
        shadow.DOFade(0f,0.5f);
        sr.DOFade(0f, 0.5f).OnComplete(()=>
        {
            Destroy(this.gameObject);
        
        });
    }
}
