using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{
    [SerializeField]
   private Rigidbody[] rigidbodies;
    Animator animator;

    private NavMeshAgent agent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        DeactiveRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactiveRagdoll()
    {
        foreach (var rigidBody in rigidbodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }
    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidbodies)
       {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
        agent.enabled = false;

    }
    public void ApplyForce(Vector3 force)
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
