using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
public class ShootingProjectile : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float fireRate = 0.5f;
    public Shaker shaker;
    public ShakePreset preset;
    public float fireForce = 100f;
    private float nextTimeToFire = 0f;
    private void Start()
    {
        shaker = Camera.main.GetComponent<Shaker>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {

            nextTimeToFire = Time.time + fireRate;
            Shooting();
            shaker.Shake(preset);
        }
    }


    private void Shooting()
    {   
        GameObject Bullet_clone = Instantiate(bulletPrefabs, attackPoint.position, Quaternion.identity);
        Rigidbody Bullet_rb = Bullet_clone.GetComponent<Rigidbody>();
        Bullet_rb.AddForce(transform.right * fireForce, ForceMode.Impulse);
        FindObjectOfType<AudioManager>().PLay("Lazer");

    }
}
