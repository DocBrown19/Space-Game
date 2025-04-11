using UnityEngine;

public class LazerPointer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Debug.DrawRay(transform.position, transform.forward, Color.red); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
