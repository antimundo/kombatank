using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting settings")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float force = 5f;
    [SerializeField] float lifespan = 1f;

    // Local variables
    bool canShoot;
    float nextShot;
    Coroutine myCoroutine;
    public float isShooting { get; private set; }

    // Input Events
    public void OnShoot(InputAction.CallbackContext ctx) => isShooting = ctx.ReadValue<float>();

    void Update()
    {
        canShoot = Time.time > nextShot;

        if (isShooting == 1 && canShoot == true)
        {
            // bullet instantiate
            GameObject thisBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            thisBullet.GetComponent<Bullet>().DestroyBullet(GetComponent<PlayerId>().id, lifespan);

            // impulse
            Rigidbody2D rb = thisBullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);

            // cooldon
            nextShot = Time.time + cooldown;
            canShoot = false;
        }
    }

    

}
