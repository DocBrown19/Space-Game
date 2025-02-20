using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*TODO
 * Projectiles
 * Sound
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
 */











public class PlayerControler : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10000f, forwardControl, verticalControl, horizontalControl, mouseSensitivity = 2.0f;
    public Vector2 mouseChange, mouseDiretion, shipDiretion;
    public AudioSource engineSoundSource;
    public AudioClip engineSoundClip;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        forwardControl = Input.GetAxis("Forward");
        verticalControl = Input.GetAxis("Vertical");
        horizontalControl = Input.GetAxis("Horizontal");
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime * forwardControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime * verticalControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime * horizontalControl, ForceMode.VelocityChange);
        //mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // mouseDiretion += mouseChange;
        float mouseup = mouseSensitivity * Input.GetAxis("Mouse Y");
        float mouseside = mouseSensitivity * Input.GetAxis("Mouse X");
        //transform.Rotate(-mouseup, mouseside, 0);
        rb.AddRelativeTorque(-mouseup, mouseside, 0, ForceMode.VelocityChange);

         engineSoundSource.volume =Mathf.Clamp ((Mathf.Abs(forwardControl)+ Mathf.Abs(verticalControl) + Mathf.Abs(horizontalControl)),0f,1f);

        //if (forwardControl != 0 || verticalControl != 0 || horizontalControl != 0)
        //{ 
        //  engineSoundSource.PlayOneShot(engineSoundClip);
        //}







    }


}


