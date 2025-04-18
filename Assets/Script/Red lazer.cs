using UnityEngine;

public class Redlazer : MonoBehaviour
{

    private float despawnTime = 0.5f, timer=0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destorys the lazers
        timer += Time.deltaTime;
        if (timer > despawnTime) {
            Destroy(gameObject);
        }
         
    }

    private void OnTriggerEnter(Collider other)
    {
        //Makes the astriod go boom
        if (other.CompareTag("Asteroid"))
        {
            other.GetComponent<Fracture>().FractureObject();
            Destroy(gameObject); //this desroys the lazer
            

        }
      
    }
}
