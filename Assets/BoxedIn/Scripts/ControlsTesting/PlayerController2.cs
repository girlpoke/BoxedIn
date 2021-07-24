using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
      [SerializeField] private float speed;
      
      private Rigidbody rb;
      private void Start() => rb = GetComponent<Rigidbody>();

      private void Update()
      {
            if (Input.GetKey(KeyCode.W)) rb.velocity = transform.forward * ((speed * 2) * Time.deltaTime);
            if (Input.GetKey(KeyCode.S)) rb.velocity = transform.forward * -((speed * 2) * Time.deltaTime);
      }
}