using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
      public GameObject CurrentCheckpoint;
    public Transform player;
    void Start()
    {
     CurrentCheckpoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer(){
        FindObjectOfType<sparky>().transform.position = CurrentCheckpoint.transform.position;
    }
}
