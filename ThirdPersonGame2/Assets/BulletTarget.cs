using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletTarget : MonoBehaviour
{
    public AiHealth health;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void TakeDamage2()
    {
        health.TakeDamage(20f,transform.right);
        agent.speed = 0f;
    }
}
