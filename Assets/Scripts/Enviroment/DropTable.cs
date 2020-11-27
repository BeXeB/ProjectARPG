using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<Drop> dropTable;

    public void Drop()
    {
        foreach (var item in dropTable)
        {
            item.TryDrop(transform.position, transform.rotation);
        }
    }
}
