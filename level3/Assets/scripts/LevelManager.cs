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
        if (CurrentCheckpoint == null)
        {
            Debug.LogWarning("LevelManager: RespawnPlayer called but CurrentCheckpoint is null");
            return;
        }

        var playerObj = FindObjectOfType<sparky>();
        if (playerObj == null)
        {
            Debug.LogWarning("LevelManager: RespawnPlayer could not find object of type 'sparky' to move");
            return;
        }

        Debug.Log($"LevelManager: Respawning player '{playerObj.name}' to checkpoint '{CurrentCheckpoint.name}' at {CurrentCheckpoint.transform.position}");
        playerObj.transform.position = CurrentCheckpoint.transform.position;
    }
}
