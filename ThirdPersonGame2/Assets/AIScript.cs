using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public Transform playerPos;
    NavMeshAgent agent;
    public Animator animator;
    public float maxTime;
    public float maxDistance;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerPos.position - agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {


                agent.destination = playerPos.transform.position;
            }
            timer = maxTime;
           
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
