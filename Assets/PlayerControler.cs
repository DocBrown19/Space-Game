using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10000f, forwardControl, verticalControl, horizontalControl, mouseSensitivity = 2.0f;
    public Vector2 mouseChange, mouseDiretion, shipDiretion;

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
        rb.AddTorque(-mouseup, mouseside, 0);







    }


}


