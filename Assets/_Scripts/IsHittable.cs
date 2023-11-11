using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IsHittable : MonoBehaviour
{
    public bool isInvincible;
    public int health;

    private CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isInvincible)
            Destroy(gameObject);
    }

    public void Hit(int dmg)
    {
        //Camera_Shake_Manager.instance.CameraShake(impulseSource);

        if (isInvincible)
            return;
        else
            health -= dmg;
        
    }
}
