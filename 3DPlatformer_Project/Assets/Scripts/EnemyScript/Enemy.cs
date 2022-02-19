using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    patrol, detect , chase
}
public class Enemy : MonoBehaviour
{   [Header("Enemy Properties")]
    public float enemySpeed = 10f;
    private Rigidbody rb;
    [HideInInspector]
    public int moveDirection = 1;
    
    public int isDestroy = 0;
    [Header("Enemy Health Management")]
    public int maxHealth = 4;
    public int currentHealth;
    public Healthbar healthBar;
    public GameObject healthUI;
    // checkground
    public LayerMask ground;
    public Transform checkSphere;
    public float checkSphereRadius = 0.1f;
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody>();
    }
    public IEnumerator SetHealth(int health)
    {
        yield return new WaitForSeconds(0.5f);
        currentHealth = health;
    }
    private void Update()
    {
        if (isDestroy == 1)
        {
            Destroy(this.gameObject);
        }
        if (currentHealth < maxHealth)
        {
            healthBar.SetHealth(currentHealth);
            healthUI.SetActive(true);
        }
        if(!Physics.CheckSphere(checkSphere.position,checkSphereRadius,ground))
        {
            moveDirection = SwitchDirect(moveDirection);
            transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, moveDirection));
        }

    }
    void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        rb.velocity = new Vector3(moveDirection * enemySpeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);
    }
    private int SwitchDirect(int currentdirect)
    {
        currentdirect *= -1;
        return currentdirect;
    }
    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;
        
        if(currentHealth<=0)
        {
            isDestroy = 1;
            GameManagement.instance.SaveProgress();
        }
        
    }

}
