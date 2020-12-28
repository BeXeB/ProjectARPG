using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    public void SetDamage(float value)
    {
        damage = value;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        CharacterStats stats = other.gameObject.GetComponent<CharacterStats>();
        if (stats)
        {
            stats.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
