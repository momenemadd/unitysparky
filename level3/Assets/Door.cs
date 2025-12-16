using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
   
    public int requiredScore = 3;
    public int sceneBuildIndex = 3;

    public float openDelay = 0.5f;

    private Animator animator;
    private bool opened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("Door: Animator not found on door object.");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;
        
            if (PlayerStats.score >= requiredScore)
            {
                opened = true;
                animator.SetBool("playerDetected", true);
                StartCoroutine(LoadNextSceneAfterDelay());
                PlayerStats.score=0;
            }
        
    }
 

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(openDelay);
        int current = SceneManager.GetActiveScene().buildIndex;
        int next = current + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            Debug.LogWarning($"Door: No next scene in Build Settings (current index {current}). Add scenes or set up a fallback.");
        }
    }
}
