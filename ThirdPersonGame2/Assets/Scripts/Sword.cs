using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Transform vfxRed;
    [SerializeField]
    private Transform vfxGreen;
    [SerializeField]
    private List<GameObject> BloodFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            MeshCollider swordcollider = this.gameObject.GetComponent<MeshCollider>();
            swordcollider.enabled = false;
            GameObject thePlayer = GameObject.Find("ThePlayer");
            ThirdPersonShooterController playerScript = thePlayer.GetComponent<ThirdPersonShooterController>();
            //Instantiate(vfxGreen, transform.position, Quaternion.identity);

            int randomblood = Random.Range(0, BloodFX.Count);
            var instance = (Instantiate(BloodFX[randomblood], transform.position, Quaternion.Euler(0, playerScript.angle + 90f, 0)));
            var settings = instance.GetComponent<BFX_BloodSettings>();
            settings.FreezeDecalDisappearance = true;

            other.GetComponent<BulletTarget>().TakeDamage2();
            
    
           

        }
        else
        {
            GameObject thePlayer = GameObject.Find("ThePlayer");
            ThirdPersonShooterController playerScript = thePlayer.GetComponent<ThirdPersonShooterController>();
            Instantiate(vfxRed, transform.position, Quaternion.Euler(0, playerScript.angle +90f , 0));

        }
        // Destroy(gameObject);
    }

}
