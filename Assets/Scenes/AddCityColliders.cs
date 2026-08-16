using UnityEngine;
using UnityEditor;

public static class AddCityColliders
{
    [MenuItem("Tools/City/Add Mesh Colliders")]
    public static void AddColliders()
    {
        GameObject root = Selection.activeGameObject;

        if (root == null)
        {
            Debug.LogError("Sélectionne la ville dans la Hierarchy.");
            return;
        }

        Transform[] allChildren = root.GetComponentsInChildren<Transform>(true);

        int added = 0;
        int skipped = 0;

        foreach (Transform t in allChildren)
        {
            GameObject obj = t.gameObject;
            string n = obj.name.ToLowerInvariant();

            // Petits détails inutiles pour les collisions
            if (n.Contains("glass") ||
                n.Contains("handle") ||
                n.Contains("hinge") ||
                n.Contains("mullion") ||
                n.Contains("sash"))
            {
                skipped++;
                continue;
            }

            MeshFilter filter = obj.GetComponent<MeshFilter>();

            if (filter == null || filter.sharedMesh == null)
                continue;

            MeshCollider collider = obj.GetComponent<MeshCollider>();

            if (collider == null)
                collider = Undo.AddComponent<MeshCollider>(obj);

            collider.sharedMesh = filter.sharedMesh;
            collider.convex = false;

            added++;
        }

        Debug.Log($"City colliders configurés : {added} | Ignorés : {skipped}");
    }
}