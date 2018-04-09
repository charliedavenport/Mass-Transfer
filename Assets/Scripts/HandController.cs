using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    [SerializeField]
    private Transform boxCollider;
    public VRPlayer player;
    public Vector3 controllerVelocity;
    public Vector3 controllerAngularVelocity;

    private void FixedUpdate() {
        
    }

    private void OnTriggerEnter(Collider other) {
        
    }


}
