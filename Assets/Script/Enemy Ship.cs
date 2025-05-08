using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*1.) Ememy will follow player but not hit them
 * If aggro, will shoot at player if in sight
 * if hits asteroid, go boom
 * Need cool vfx explosion
 * Needs to shoot lazers
 * Needs to make noise
 * Make him less of an aimbot
 * Stoped shooting after a while
 * Still hits player and zooms behind player
 */

public class EnemyShip : MonoBehaviour
{
    private bool inRangeShoot, inRangeMove, canSeePlayer, canShoot = false, isAggro = false;
    [SerializeField]
    private float maxRangeShoot, minRangeMove = 25f, currentRangeToPlayer, FireRate, speed = 15, rotationRate = 10f, LazerSpeed = 50f;
    private Vector3 dirToPlayer, randomFlightDir, randomCasualDirection;
    private GameObject player;
    private Rigidbody rb;
    [SerializeField]
    private AudioSource engineSoundSource, lasersoundsource;
    private AudioClip laserSoundclip;
    private GameObject enemyLaser;
    [SerializeField]
    private GameObject rightLazerSpawnPoint, leftLaserSpawnPoint;
    [SerializeField]
    private ParticleSystem Explosion;
    private List<Vector3> StrafeDirectons = new List<Vector3>();
    void Start()
    {
        StrafeDirectons.Add(Vector3.up);
        StrafeDirectons.Add(Vector3.down);
        StrafeDirectons.Add(Vector3.right);
        StrafeDirectons.Add(Vector3.left);
        StrafeDirectons.Add(new Vector3(1, 1, 0));
        StrafeDirectons.Add(new Vector3(1, -1, 0));
        StrafeDirectons.Add(new Vector3(-1, 1, 0));
        StrafeDirectons.Add(new Vector3(-1, -1, 0));
        randomCasualDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        randomFlightDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2f), 0f);
        FireRate = Random.Range(0.15f, 1f);
        player = GameObject.Find("Player Ship");
        rb = GetComponent<Rigidbody>();
        enemyLaser = Resources.Load<GameObject>("Enemy Ship Lazer");
        laserSoundclip = Resources.Load<AudioClip>("Sounds/Lazer");
        StartCoroutine(EnemyLazerFireing());
        StartCoroutine(ChangeFlightDirection());
    }

    // Update is called once per frame
    void Update()
    {
        currentRangeToPlayer = Vector3.Distance(transform.position, player.transform.position);
        dirToPlayer = player.transform.position - transform.position;

        //Movment Section
        if (currentRangeToPlayer > minRangeMove && isAggro) //moves the enemy tword the player when outside of minimum range
        {
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);

            randomFlightDir = StrafeDirectons[Random.Range(0, StrafeDirectons.Count)]; //Generates a random strafe directon for the enemy if it is within minimum range
        }
        else if (currentRangeToPlayer <= minRangeMove && isAggro)// moves enemy 30% of full speed in random direction in order to strafe around the player
        {
            rb.AddRelativeForce(randomFlightDir * 0.3f * speed * Time.deltaTime, ForceMode.VelocityChange);
        } else if (!isAggro)//enemy is not aggro, should fly casul
        {
            //transform.rotation = Quaternion.LookRotation(randomCasualDirection, transform.up);
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);

        }



        if (currentRangeToPlayer < maxRangeShoot)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
        //looks at the player, but keeps enemy's own'up value
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, transform.up);

    }



    IEnumerator EnemyLazerFireing()
    {

        yield return new WaitForSeconds(FireRate);//waits before doing anything 


        if (canShoot)//checks whether we are in range to shoot based on the range-check in update
        {
            GameObject laser1, laser2;//creates two vareibles to store our created lasers so that we can force them to move once we instantiate them

            laser1 = Instantiate(enemyLaser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);//creates a laser and places it on the left turret
            laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * LazerSpeed;//shoves laser foward 
            laser2 = Instantiate(enemyLaser, rightLazerSpawnPoint.transform.position, rightLazerSpawnPoint.transform.rotation);//creates a laser and places it on the right turret
            laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLazerSpawnPoint.transform.forward * LazerSpeed;//shoves laser foward 
            lasersoundsource.PlayOneShot(laserSoundclip);// plays the pew pew this stinks
        }
        StartCoroutine(EnemyLazerFireing());//starting a new co-routine that starts this proces again wich alowws the enemy to keep shooting.
    }

    IEnumerator ChangeFlightDirection()
    {
        yield return new WaitForSeconds(Random.Range(5f, 120f));
        randomCasualDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        StartCoroutine(ChangeFlightDirection());
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player Laser")) ;
        {
            Explosion.Play();
            



        }

    }
}
