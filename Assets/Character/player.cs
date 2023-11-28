using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;

    // Specify the layer name in the Unity Inspector
    public string groundLayerName = "Ground";

    // Layer mask variable to be used in raycasting
    private LayerMask groundLayer;

    void Awake()
    {
        // Set up the layer mask in Awake instead of the constructor
        groundLayer = LayerMask.GetMask(groundLayerName);
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
        }
        AlignWithSurface();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
            //steps1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walkback");
            playerAnim.ResetTrigger("idle");
            //steps1.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
            //steps1.SetActive(false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }
        if (walking == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                w_speed = w_speed + rn_speed;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                w_speed = olw_speed;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
            }
        }
        AlignWithSurface();
    }

    void AlignWithSurface()
    {
        // Perform raycast to detect the ground on the specified layer
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f, groundLayer))
        {
            // Rotate the player to match the ground normal
            Vector3 groundNormal = hit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
            playerTrans.rotation = Quaternion.Slerp(playerTrans.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

}