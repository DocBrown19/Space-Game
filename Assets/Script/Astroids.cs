using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private Vector3 randomSpin;
    private float speed;
    void Start()
    {
        randomSpin = new Vector3(Random.Range(-359f, 359f), Random.Range(-359f, 359f), Random.Range(-359f, 359f));
        speed = Random.Range(0, .25f);
    }

    
    void Update()
    {
      transform.Rotate(randomSpin* Time.deltaTime*speed);  
    }
}
