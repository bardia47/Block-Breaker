using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public static BrickManager instance;
    [Header("Refs")]
    public GameObject brickPrefab;
    public Sprite yellowBrick;
    public Sprite redBrick;
    [Header("Level Size")]
    public int width;
    public int height;
    public float widthInterval = 1f;
    public float heightInterval = 0.5f;

    private static int MAX_LEVEL = 2;
    public int bricksLeft = 0;

    public int level = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        StartCoroutine(SetUpLevel(0f, level));
    }


    public void CheckForNewLevel() {

        if (bricksLeft <= 0)
        {
            if (level <= MAX_LEVEL)
            {
                level++;
                height += 1;
                width += 1;
            }
            StartCoroutine(SetUpLevel(1f, level));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private float getWidthOffset() {
        return (width / 2) + (0.2f) * level;
    }
    private void SpawnBrick(int xPos, int yPos) {
        GameObject g = Instantiate(brickPrefab, new Vector2((xPos- getWidthOffset()) * widthInterval,
            yPos * heightInterval), Quaternion.identity);
        if (yPos % 2 == 0)
        {
            g.GetComponent<Brick>().sr.sprite = yellowBrick;
            g.GetComponent<Brick>().Health = 2;
        
        }
        bricksLeft++;
    
    }

    private IEnumerator SetUpLevel(float delay,int level) {
        yield return new WaitForSeconds(delay);
        int xcounter =1;
        int ycounter =1 ;
        switch (level%3)
        {
                case 1:
                    xcounter = 2;
                    ycounter = 1;
                    break;
                case 2:
                    xcounter = 2;
                    ycounter = 2;
                    break;
                case 0:
                    xcounter = 1;
                    ycounter = 1;
                    break;
            }
        for (int x = 0; x <= width; x+= xcounter)
        {
            for (int y = MAX_LEVEL-level; y < MAX_LEVEL - level + height; y+= ycounter)
            {
                SpawnBrick(x, y);
            }
        }
    }
}
