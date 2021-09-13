using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] public LayerMask whatIsWall;
    public float wallrunForce, maxWallrunTime, maxWallSpeed;
    public bool isWallRight, isWallLeft;
    bool isWallRunning;
    public float maxWallRunCameraTilt, wallRunCameraTilt;
    public Transform orientation;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWallLeft)
            StartWallrun();


        if (isWallRight)
            StartWallrun();

        WallRunInput();
        CheckForWall();
    }
    private void StartWallrun()
    {
        playerRigidbody.useGravity = false;
        isWallRunning = true;

        playerRigidbody.AddForce(new Vector3(5, 0));
        // playerRigidbody.AddForce(orientation.forward * 50 * Time.deltaTime);
        if (playerRigidbody.velocity.magnitude <= maxWallSpeed)
        {
            playerRigidbody.AddForce(orientation.forward * wallrunForce * Time.deltaTime);


            if (isWallRight)
            {
                playerRigidbody.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
                playerRigidbody.AddForce(orientation.forward * 20000 * Time.deltaTime);
            }

            else
                playerRigidbody.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
            playerRigidbody.AddForce(orientation.forward * 20000 * Time.deltaTime);
        }

        if (playerRigidbody.velocity.magnitude <= 1)
        {
            playerRigidbody.useGravity = true;
        }
    }
    private void StopWallRun()
    {
        isWallRunning = false;
        playerRigidbody.useGravity = true;
    }
    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);

        //leave wall run
        if (!isWallLeft && !isWallRight) StopWallRun();


        //rset jump
       // if (isWallLeft || isWallRight) secondJumpAvailable = true;
    }

    private void WallRunInput()
    {
        //Wallrun
        //if (Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        // if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();
        // if (Input.GetKey(KeyCode.W) && isWallLeft) StartWallrun();
    }

}
