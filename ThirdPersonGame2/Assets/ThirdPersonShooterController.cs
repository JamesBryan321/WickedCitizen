using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimCamera;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private LayerMask aimColliderMask = new LayerMask();
    [SerializeField]
    private Transform debugTransform;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;

    public Animator animator;
    public Animator animatorGun;
    public float angle;
    public GameObject HandIks;
    private Rig gunRig;

    public GameObject LeftHand;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs StarterAssetsInputs;

    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private float reloadTime;
    private bool isReloading = false;

    public Text ammoText;

    public GameObject sword;
    private MeshCollider swordCol;


    private void Start()
    {
        swordCol = sword.gameObject.GetComponent<MeshCollider>();
        gunRig = HandIks.gameObject.GetComponent<Rig>();
        gunRig.weight = 0f;

    }
    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        
       // swordCol = sword.gameObject.GetComponent<MeshCollider>();
      
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        ammoText.text = currentAmmo.ToString();
        if (isReloading)
            return;
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycasyHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycasyHit.point;
            mouseWorldPosition = raycasyHit.point;
        }
        if (StarterAssetsInputs.shoot & StarterAssetsInputs.aim == false)
        {
           // HandIks.SetActive(false);
            // Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;

            StartCoroutine(swordOn());
            animator.SetTrigger("SwordAttack");
            StarterAssetsInputs.shoot = false;

            // angle = Mathf.Atan2(raycasyHit.normal.x, raycasyHit.normal.z) * Mathf.Rad2Deg + 180;

            StartCoroutine(swordWait());


        }
        if (StarterAssetsInputs.aim)
        {
            LookAtConstraint lookCon = LeftHand.gameObject.GetComponent<LookAtConstraint>();
            gunRig.weight = 1f;
            lookCon.enabled = true;
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0.9f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            if (StarterAssetsInputs.shoot)
            {
                Vector3 aimDir = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
                Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
                animator.SetTrigger("Shoot");
                StarterAssetsInputs.shoot = false;
                animatorGun.SetTrigger("Shoot");

                angle = Mathf.Atan2(raycasyHit.normal.x, raycasyHit.normal.z) * Mathf.Rad2Deg + 180;

                currentAmmo--;
            }
        }
        else
        {
            LookAtConstraint lookCon = LeftHand.gameObject.GetComponent<LookAtConstraint>();
            lookCon.enabled = false;
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            gunRig.weight = 0f;

        }
       

        if(StarterAssetsInputs.aim && StarterAssetsInputs.sprint)
        {
            animator.SetTrigger("AimRun");
        }
        
        
        if (StarterAssetsInputs.aim && StarterAssetsInputs.walk)
        {
            animator.SetTrigger("WalkAim");
        }
    }
     IEnumerator Reload()
    {
        StarterAssetsInputs.aim = false;
        animator.ResetTrigger("WalkAim");
        animator.ResetTrigger("Shoot");
        animator.SetTrigger("Shoot");
        isReloading = true;
      //  animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;

    }
    IEnumerator swordWait()
    {
        yield return new WaitForSeconds(1f);
        HandIks.SetActive(true);
        animator.ResetTrigger("SwordAttack");
        swordCol.enabled = false;
    }
    IEnumerator swordOn()
    {
        yield return new WaitForSeconds(0.3f);
        swordCol.enabled = true;

    }
}
