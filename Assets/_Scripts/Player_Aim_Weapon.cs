using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Aim_Weapon : MonoBehaviour
{
    private Transform aimTransform;


    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private GameObject explosionPrefab;

    private GameObject muzzleInst;

    public float timer = 0f;
    public float reloadTimer = 0f;

    public bool shot = false;

    public int dmg;
    public int ammo;
    public int maxAmmo;

    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float weaponRange = 10f;
    [SerializeField] private Animator muzzleFlashAnimator;
    [SerializeField] private Animator reloadAnimator;

    [SerializeField] private Logic_Script logicScript;

    bool reloadAnim = true;


    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        ammo = maxAmmo;
        logicScript.DisplayAmmo(ammo, maxAmmo);
        
    }


    // Update is called once per frame
    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {

            timer += Time.deltaTime;

            if (timer >= 0.1f && ammo > 0)
            {
                ammo--;

                logicScript.DisplayAmmo(ammo, maxAmmo);

                float randNum = Random.Range(-2f, 2f);
                Vector3 spread = new Vector3(0, randNum, 0);

                muzzleFlashAnimator.SetTrigger("Shoot");

                Vector3 forwardVector = Vector3.up;
                float deviation = Random.Range(10f, -10f);
                //float angle = Random.Range(0f, 360f);
                forwardVector = Quaternion.AngleAxis(deviation, Vector3.forward) * forwardVector;
                // forwardVector = Quaternion.AngleAxis(angle, Vector3.forward) * forwardVector;
                forwardVector = transform.rotation * forwardVector;

                var hit = Physics2D.Raycast(
                    bulletSpawnPoint.position,
                    forwardVector,
                    weaponRange
                    );

                var trail = Instantiate(
                    bulletTrail,
                    bulletSpawnPoint.position,
                    transform.rotation
                    );

                var trailScript = trail.GetComponent<Bullet_Trail>();


                if (hit.collider != null)
                {
                    trailScript.SetTargetPosition(hit.point);

                    var explosion = Instantiate(explosionPrefab, hit.point, Quaternion.identity);
                    var hittable = hit.collider.GetComponent<IsHittable>();
                    hittable?.Hit(dmg);
                    Destroy(explosion, 0.5f);
                }
                else
                {
                    var endPosition = bulletSpawnPoint.position + forwardVector * weaponRange;
                    trailScript.SetTargetPosition(endPosition);

                }
                timer = 0;
            }

            
        }
        if (ammo <= 0)
        {
            reloadTimer += Time.deltaTime;

            if (reloadAnim)
            {
                reloadAnimator.SetTrigger("Reload");
                reloadAnim = false;
            }
            
        }

        if (reloadTimer > 1f)
        {
            ammo = maxAmmo;
            reloadTimer = 0;
            reloadAnim = true;
            logicScript.DisplayAmmo(ammo, maxAmmo);
        }
    }

}
