using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
  
    Ragdoll ragdoll;

    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer;
    public float dieForce = 10f;
    SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if(currentHealth <= 0f)
        {
            Die(direction);
            
        }
        blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction)
    {
        ragdoll.ActivateRagdoll();
        direction.y = 1;
        ragdoll.ApplyForce(direction * dieForce);
    }
}
