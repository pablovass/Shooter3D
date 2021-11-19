using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private bool walking;
    private Vector3 playerToMouse; //almasena el eje xyz
    private int floorMask;
    private float camRayLength=100f;
    private float speed = 2f;
    private Rigidbody _rigidbody;
    private Vector3 move;
    
    

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Animation(h,v);
        MyRotation();
        Moving(h,v);
    }

    void Animation(float h, float v)
    {
        walking = h != 0f || v != 0f;
        _animator.SetBool("isWalking",walking);
    }

    void Moving(float h, float v)
    {
     move.Set(h,0f,v);
     move = move.normalized * speed * Time.deltaTime;
     
     if (_rigidbody)_rigidbody.MovePosition(transform.position+move);

    }
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    void MyRotation()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _rigidbody.MoveRotation(newRotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
