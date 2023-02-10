using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    float xMovement;
    float yMovement;
    void Start()
    {
        
    }
    void Update()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");


       transform.position += new Vector3(xMovement * speed * Time.deltaTime, yMovement * speed * Time.deltaTime);
    }
}
