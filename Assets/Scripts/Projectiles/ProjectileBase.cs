using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float ttl;
    protected float destroyTime = 0;
    protected Weapon weaponsShotFrom;

    public void SetDamage(float value)
    {
        damage = value;
    }

    public void SetWeaponShotFrom(Weapon value)
    {
        weaponsShotFrom = value;
    }

    private void OnEnable()
    {
        destroyTime = Time.time + ttl;
    }

    protected virtual void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (destroyTime <= Time.time)
        {
            weaponsShotFrom.ReturnToPool(this);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        CharacterStats stats = other.gameObject.GetComponent<CharacterStats>();
        if (stats)
        {
            stats.TakeDamage(damage);
        }
        weaponsShotFrom.ReturnToPool(this);
    }
}
