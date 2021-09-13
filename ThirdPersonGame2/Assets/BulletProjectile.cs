using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletProjectile : MonoBehaviour
{

    private Rigidbody bulletRigidbody;
    public float bulletSpeed;
    [SerializeField]
    private Transform vfxRed;
    [SerializeField]
    private Transform vfxGreen;
    [SerializeField]
    private List<GameObject> BloodFX;



    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    
      
    }
    // Start is called before the first frame update
    void Start()
    {

        bulletRigidbody.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>() != null)
        {
            GameObject thePlayer = GameObject.Find("ThePlayer");
            ThirdPersonShooterController playerScript = thePlayer.GetComponent<ThirdPersonShooterController>();
            //Instantiate(vfxGreen, transform.position, Quaternion.identity);
            
            int randomblood = Random.Range(0, BloodFX.Count);
            var instance = (Instantiate(BloodFX[randomblood], transform.position, Quaternion.Euler(0,playerScript.angle + 90f, 0)));
            var settings = instance.GetComponent<BFX_BloodSettings>();
            settings.FreezeDecalDisappearance = true;

            other.GetComponent<BulletTarget>().TakeDamage2();
          
        }
        else
        {
            Instantiate(vfxRed, transform.position, Quaternion.identity);

        }
       // Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
