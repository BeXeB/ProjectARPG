using UnityEngine;

public class ConvertToRegularMesh : MonoBehaviour
{
    [ContextMenu("Convert To Regular Mesh")]
    void Convert()
    {
        SkinnedMeshRenderer skinned = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinned.sharedMesh;
        meshRenderer.sharedMaterials = skinned.sharedMaterials;

        DestroyImmediate(skinned);
        DestroyImmediate(this);
    }
}
