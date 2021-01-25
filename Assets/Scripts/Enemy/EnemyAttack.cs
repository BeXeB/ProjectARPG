using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectile;
    public float damage = 10f;
    private float attackCooldown = 0f;
    public float attackSpeed = 3f;
    
    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack()
    {
        if (attackCooldown <= 0f)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z + 0.5f);
            var createdObject = Instantiate(projectile, pos, transform.rotation);
            createdObject.GetComponent<ProjectileBase>().SetDamage(damage);
            attackCooldown = 1f / attackSpeed;
            Destroy(createdObject, 5f);
        }
    }
}
