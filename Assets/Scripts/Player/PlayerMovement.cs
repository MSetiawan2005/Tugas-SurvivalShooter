using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");


    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Turning();
    }

    public void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);

        movement = movement.normalized * speed * Time.deltaTime;
        
        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0f;
        animator.SetBool(IsWalking, walking);
    }
}
