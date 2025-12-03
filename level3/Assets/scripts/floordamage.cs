using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floordamage : MonoBehaviour
{
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other != null && other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(1);
            }
            else
            {
                // Fallback: if PlayerStats isn't attached, respawn to avoid leaving player stuck
                FindObjectOfType<LevelManager>().RespawnPlayer();
            }
        }
    }
}
