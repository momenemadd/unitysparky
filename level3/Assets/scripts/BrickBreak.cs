using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite explodBlock;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            
            if (other.GetContact(0).point.y > transform.position.y)
            {
                BreakBrick();
            }
        }
    }

    void BreakBrick()
    {
        sr.sprite = explodBlock;
        Destroy(gameObject, 1f);
    }
}
