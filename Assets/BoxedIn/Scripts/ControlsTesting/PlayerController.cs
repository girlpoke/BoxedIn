using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoxedIn.testing
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rb;

        private void Start() => rb = GetComponent<Rigidbody>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W)) AngularVelocity(-speed);
            if (Input.GetKeyDown(KeyCode.A)) AngularVelocity(-speed, true);
            if (Input.GetKeyDown(KeyCode.S)) AngularVelocity(speed);
            if (Input.GetKeyDown(KeyCode.D)) AngularVelocity(speed, true);
        }

        private void AngularVelocity(float _force, bool _sideControls = false)
        {
            var aV = rb.angularVelocity;
            
            if(!_sideControls) aV.x += (_force * Time.deltaTime);
            else aV.z += (_force * Time.deltaTime);
            
            rb.angularVelocity = aV;
        }
    }
}