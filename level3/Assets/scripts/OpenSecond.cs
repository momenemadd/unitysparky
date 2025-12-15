using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSecond : MonoBehaviour
{
    public void onClickStart(){
        SceneManager.LoadScene(2);
    }
}
