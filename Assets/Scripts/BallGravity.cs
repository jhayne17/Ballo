using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravity : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rig;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.useGravity = true; // Enables gravity
    }
}
