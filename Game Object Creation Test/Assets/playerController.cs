using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class playerController : MonoBehaviour
{

    public float speed;
    public bool isOrange;
    private Rigidbody rb;
    //public float yRot;

    void Start()
    {
        isOrange = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    { 
        Quaternion rotations = Camera.main.gameObject.transform.rotation;
        float yRot = rotations.y / (1 - rotations.w * rotations.w);
        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal*Mathf.Cos(yRot)-moveVertical*Mathf.Sin(yRot), 0.0f, -(moveHorizontal*Mathf.Sin(yRot) + moveVertical*Mathf.Cos(yRot)));

        rb.AddForce(movement * speed);
    }
}
