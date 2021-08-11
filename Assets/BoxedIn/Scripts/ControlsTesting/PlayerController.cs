using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoxedIn.testing
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform cam;
        private Rigidbody rb;
        
        private void Start() => rb = GetComponent<Rigidbody>();
        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var direction = new Vector3(v, 0f, h);

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, -direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
               
                rb.angularVelocity = moveDir * (speed * Time.deltaTime);
            }
        }
    }
}