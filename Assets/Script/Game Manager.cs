using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject[] astroids;
    public GameObject player;
    public int minAstroids = 100, maxAstroids = 500, minSpawnTime = 30, maxSpawnTime = 60, minAstroidFieldSize = 500, maxAstroidFieldSize = 1000;
    private int randomNumOfastroids, randomAsteroid, randSpawnTime;
    private Vector3 randomSpawnLocation, randomLocationOffset;
    private float randX, randY, randZ;
    void Start()
    {
        randomNumOfastroids = Random.Range(minAstroids, maxAstroids);
        randSpawnTime = Random.Range(minSpawnTime, maxSpawnTime + 1);
        for (int i = 0; i < randomNumOfastroids; i++) 
        {
         randomAsteroid = Random.Range(0, astroids.Length);
         randX = player.transform.position.x + Random.Range(-500f,500f);
         randY = player.transform.position.x + Random.Range(-500f, 500f);
         randZ = player.transform.position.x + Random.Range(-500f, 500f);
         randomSpawnLocation = new Vector3(randX, randY, randZ); 
         Instantiate(astroids[randomAsteroid], randomSpawnLocation, transform.rotation);

        }
        StartCoroutine(SpawnAstroidField());
    }

    IEnumerator SpawnAstroidField()
    {
       yield return new WaitForSeconds(randSpawnTime);
        int randomNumOfAsteroidField = Random.Range(minAstroidFieldSize, maxAstroidFieldSize);
        randX = player.transform.position.x + Random.Range(-200f, 200f);
        randY = player.transform.position.x + Random.Range(-200f, 200f);
        randZ = player.transform.position.x + Random.Range(100f, 200f);
        randomSpawnLocation = new Vector3(randX, randY, randZ);



        for (int i = 0; i < randomNumOfastroids; i++)
        {
            randomAsteroid = Random.Range(0, astroids.Length);
            randX = randomSpawnLocation.x + Random.Range(100f, 200f);
            randY = randomSpawnLocation.x + Random.Range(100f, 200f);
            randZ = randomSpawnLocation.x + Random.Range(100f, 200f);
            randomLocationOffset = new Vector3(randX, randY, randZ);
            Instantiate(astroids[randomAsteroid], randomLocationOffset, transform.rotation);

        }

        StartCoroutine(SpawnAstroidField());
    }
   
    void Update()
    {
        
    }
}
