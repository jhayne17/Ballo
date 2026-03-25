using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounceLogic : MonoBehaviour
{
   public float bounceForce = 10f;
   private Rigidbody rb;
   void Start()
   {
       rb = GetComponent<Rigidbody>();
   }
   
   void OnCollisionEnter(Collision collision)
   {
       rb.velocity = Vector3.up * bounceForce;
   }

}
