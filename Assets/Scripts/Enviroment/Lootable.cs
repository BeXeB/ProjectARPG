using UnityEngine;

[RequireComponent(typeof(DropTable))]
public class Lootable : Interactable
{
    bool looted = false;
    public override void OnInteract()
    {
        if (!looted)
        {
            looted = true;
            GetComponent<DropTable>().Drop();
        }
    }
}
