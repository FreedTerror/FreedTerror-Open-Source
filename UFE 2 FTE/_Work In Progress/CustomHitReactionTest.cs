using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHitReactionTest : MonoBehaviour
{
    public SpriteRenderer mainSpriteRenderer;
    public Sprite hitReactionSprite;

    public Transform customTransform;
    public SpriteRenderer customSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainSpriteRenderer.sprite == hitReactionSprite)
        {
            mainSpriteRenderer.enabled = false;
            customSpriteRenderer.enabled = true;
            customSpriteRenderer.flipX = mainSpriteRenderer.flipX;
            //customSpriteRenderer.sprite = mainSpriteRenderer.sprite;
        }
        else
        {
            mainSpriteRenderer.enabled = true;
            customSpriteRenderer.enabled = false;
        }      
    }
}
