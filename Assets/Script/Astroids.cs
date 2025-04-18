using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour
{
    // private Vector3 randomSpin;
    // private float speed;
    [SerializeField]
    private int checkPlayerDistanceTime = 5;
    [SerializeField]
    private Transform playerTransform;
    private float maxDistenceFromPlayer = 1000f;
    void Start()
    {
        // randomSpin = new Vector3(Random.Range(-359f, 359f), Random.Range(-359f, 359f), Random.Range(-359f, 359f));
        // speed = Random.Range(0, .25f);
        playerTransform = GameObject.Find("Player Ship").transform;
        StartCoroutine(CheckPlayerDistance());
    }
    IEnumerator CheckPlayerDistance()
    {
      yield return new WaitForSeconds(checkPlayerDistanceTime);
        if (Vector3.Distance(playerTransform.position, transform.position) > maxDistenceFromPlayer)
        {
           Destroy(gameObject); 
        }
        StartCoroutine(CheckPlayerDistance());
    }


    void Update()
    {
     // transform.Rotate(randomSpin* Time.deltaTime*speed);  
    }
}
