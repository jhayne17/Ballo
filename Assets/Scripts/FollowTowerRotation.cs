using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTowerRotation : MonoBehaviour
{
    public Transform tower;

    void LateUpdate()
    {
        transform.rotation = tower.rotation;
    }
}
