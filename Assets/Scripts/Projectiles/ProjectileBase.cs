using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float speed;
    public float damage;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        print("Hit: " + other.transform.name);
        CharacterStats stats = other.gameObject.GetComponent<CharacterStats>();
        if (stats)
        {
            stats.TakeDamage(damage);   
        }
        Destroy(this.gameObject);
    }
}
