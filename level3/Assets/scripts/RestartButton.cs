using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
