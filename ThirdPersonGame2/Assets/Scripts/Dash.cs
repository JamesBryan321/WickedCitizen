using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Dash : MonoBehaviour
{
    //   ThirdPersonController shooterController;

   //StarterAssets.ThirdPersonController Pcontroller;
    public float dashSpeed;
    public float dashTime;
    public GameObject dashEffect;
    CharacterController controller;
    private StarterAssetsInputs StarterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
       // Pcontroller = GetComponent<StarterAssets.ThirdPersonController>();
        controller = GetComponent<CharacterController>();
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StarterAssetsInputs.dash)
        {
            StartCoroutine(Dashplayer());
        }
    }

    public void StartDash()
    {
        StartCoroutine(Dashplayer());
    }
    public IEnumerator Dashplayer()
    {
        float startTime = Time.time;
        Instantiate(dashEffect, this.transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        while (Time.time < startTime + dashTime)
        {

            controller.Move(controller.transform.forward * dashSpeed * Time.deltaTime);
          
            yield return null;
        }
        StarterAssetsInputs.dash = false;
    }
}
