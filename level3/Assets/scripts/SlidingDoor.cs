using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float openY = 2f;    
    public float closeY = 0f; 
    public float speed = 2f;     
    public float waitTime = 1f; 
    
    private bool isOpening = false;
    private bool isClosing = false;


    void Start()
    {
        StartCoroutine(DoorRoutine());
    }

    IEnumerator DoorRoutine()
    {
        while (true)
        {
            isOpening = true;
            isClosing = false;
            yield return new WaitForSeconds(waitTime);
 
            isOpening = false;
            isClosing = true;
            yield return new WaitForSeconds(waitTime);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, openY, transform.position.z),
                speed * Time.deltaTime);
        }

        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, closeY, transform.position.z),
                speed * Time.deltaTime);
        }
    }
}