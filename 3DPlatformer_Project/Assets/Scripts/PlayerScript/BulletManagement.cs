using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagement : MonoBehaviour
{
    public ParticleSystem bulletParticle;
    public int maxCollide = 1;
    private int currentCollide;
    public int bulletDamage=1;
    private void Start()
    {
        currentCollide = maxCollide;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitpoint = collision.GetContact(0).normal;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            DestroyBullet(hitpoint);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }
        else
        {
            currentCollide--;
        }
        if(currentCollide<= 0)
        {
            DestroyBullet(hitpoint);
        }
    }
    private void DestroyBullet(Vector3 hitpoint)
    {
        ParticleSystem bullet_Particle = Instantiate(bulletParticle, transform.position, Quaternion.LookRotation(hitpoint));
        Destroy(bullet_Particle.gameObject, 2f);
        Destroy(this.gameObject,0.02f);
    }
}
