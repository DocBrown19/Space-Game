using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*TODO
 * Projectiles
 * Sound (made progres)
 * Enemies/Stuff to shoot
 * Moveing astroids in random diretion
 * Spawn asteroids randomly 
 * Spawn planets randomly
 * Add hyperspace
 * Add fleetcariers
 * Add cockpit for ship and fleet carier
 * Add more ships
 * Add stars and stations
 * Add upgrades for your ship
 * Add a human player
 * Add srvs
 * make it sothe astriods dont spawn in the space staion
 * make a customization menu
 * Needs a game over screen
 */











public class PlayerControler : MonoBehaviour
{
    private Rigidbody rb;
    private float forwardControl, verticalControl, horizontalControl;
    [SerializeField]
    private float speed = 10000f, mouseSensitivity = 2.0f, rollControl, rollSensitivity = 100f, boost = 30f, maxBoostSpeed = 300f,minBoostSpeed = 30f,boostAcceleration = 120f,LazerSpeed = 50f;
    private Vector2 mouseChange, mouseDiretion, shipDiretion;
    [SerializeField]
    private AudioSource engineSoundSource, lasersoundsource;
    private AudioClip laserSoundclip;
    private GameObject laser;
    [SerializeField]
    private GameObject rightLazerSpawnPoint, leftLaserSpawnPoint;
    private Camera playerCam;
    [SerializeField]
    private ParticleSystem Explosion;
 
 

    private void Start()
    {
        playerCam = GetComponent<Camera>();
        rb = GetComponent <Rigidbody>();
        laser = Resources.Load<GameObject>("Red Lazer");
        laserSoundclip = Resources.Load<AudioClip>("Sounds/Lazer");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        forwardControl = Input.GetAxis("Forward");
        verticalControl = Input.GetAxis("Vertical");
        horizontalControl = Input.GetAxis("Horizontal");
        rollControl = Input.GetAxis("Roll");
        rb.AddRelativeForce(Vector3.forward * boost * Time.deltaTime * forwardControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime * verticalControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime * horizontalControl, ForceMode.VelocityChange);

        float mouseup = mouseSensitivity * Input.GetAxis("Mouse Y");
        float mouseside = mouseSensitivity * Input.GetAxis("Mouse X");
  
        rb.AddRelativeTorque(-mouseup, mouseside, 0, ForceMode.VelocityChange);
        rb.AddRelativeTorque(Vector3.forward*rollControl* rollSensitivity* Time.deltaTime,ForceMode.Acceleration);

         engineSoundSource.volume =Mathf.Clamp ((Mathf.Abs(forwardControl)+ Mathf.Abs(verticalControl) + Mathf.Abs(horizontalControl)),0f,1f);
        if (forwardControl > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            
            boost = Mathf.Lerp(minBoostSpeed, maxBoostSpeed, Time.deltaTime * boostAcceleration);

        }
        else
        {
            boost = Mathf.Lerp(maxBoostSpeed, minBoostSpeed, Time.deltaTime * boostAcceleration);
        }
        //fire my lazers
        if (Input.GetMouseButtonDown(0))
        {
           GameObject laser1, laser2;
            laser1 = Instantiate(laser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);
            laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * LazerSpeed;
            laser2 = Instantiate(laser, rightLazerSpawnPoint.transform.position, rightLazerSpawnPoint.transform.rotation);
            laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLazerSpawnPoint.transform.forward * LazerSpeed;
            lasersoundsource.PlayOneShot(laserSoundclip);
            
        }


        







    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy Laser"));
        {
            Explosion.Play();
           


        }

    }


}


