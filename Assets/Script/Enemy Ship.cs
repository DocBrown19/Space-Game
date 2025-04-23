using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
/*1.) Ememy will follow player but not hit them
 * If aggro, will shoot at player if in sight
 * if hits asteroid, go boom
 * Need cool vfx explosion
 * Needs to shoot lazers
 * Needs to make noise
 */

public class EnemyShip : MonoBehaviour
{
    private bool inRange, canSeePlayer, canShoot = true, isAggro = true;
    [SerializeField]
    private float maxRange, currentRangeToPlayer, FireRate = 1f, speed = 15, rotationRate = 10f, LazerSpeed = 50f;
    private Vector3 dirToPlayer;
    private GameObject player;
    private Rigidbody rb;
    [SerializeField]
    private AudioSource engineSoundSource, lasersoundsource;
    private AudioClip laserSoundclip;
    private GameObject enemyLaser;
    [SerializeField]
    private GameObject rightLazerSpawnPoint, leftLaserSpawnPoint;
    void Start()
    {
        player = GameObject.Find("Player Ship");
        rb = GetComponent<Rigidbody>();
        enemyLaser = Resources.Load<GameObject>("Enemy Ship Lazer");
        laserSoundclip = Resources.Load<AudioClip>("Sounds/Lazer");
        StartCoroutine(EnemyLazerFireing());
    }

    // Update is called once per frame
    void Update()
    {
        dirToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dirToPlayer, Color.red);
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
       
    }

    IEnumerator EnemyLazerFireing()
    {
        
     yield return new WaitForSeconds(2f);
     GameObject laser1, laser2;
    laser1 = Instantiate(enemyLaser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);
    laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward* LazerSpeed;
    laser2 = Instantiate(enemyLaser, rightLazerSpawnPoint.transform.position, rightLazerSpawnPoint.transform.rotation);
    laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLazerSpawnPoint.transform.forward* LazerSpeed;
    lasersoundsource.PlayOneShot(laserSoundclip);
    StartCoroutine(EnemyLazerFireing());
    }
}
