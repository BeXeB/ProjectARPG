using UnityEngine;

[System.Serializable]
public class Drop
{
    public GameObject drop;
    public float dropChance = 0;

    public void TryDrop(Vector3 position, Quaternion rotation)
    {
        var rngNo = Random.Range(1, 100);
        if (rngNo <= dropChance)
        {
            Debug.Log(position);
            var dropPosition = position + new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f));
            Debug.Log(dropPosition);
            var dropedObject = GameObject.Instantiate(drop, dropPosition, rotation);
            dropedObject.layer = 10;
            var rigidBody = dropedObject.GetComponent<Rigidbody>();
            rigidBody.AddExplosionForce(Random.Range(500, 1000), position, 2f, 50);
        }
    }
}
