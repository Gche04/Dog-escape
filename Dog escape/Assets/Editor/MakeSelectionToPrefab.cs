using UnityEditor;
using UnityEngine;

public class MakeSelectionToPrefab : ScriptableWizard {
    public GameObject Prefab;

    [MenuItem("Tools/Replace Selection With Prefab")]
    static void CreateWizard() {
        ScriptableWizard.DisplayWizard<MakeSelectionToPrefab>("Replace Selection", "Replace");
    }

    void OnWizardCreate() {
        if (Prefab == null) return;
        
        GameObject[] selected = Selection.gameObjects;
        foreach (GameObject go in selected) {
            // Instantiate the prefab
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
            Undo.RegisterCreatedObjectUndo(instance, "Replace With Prefab");
            
            // Match transform properties
            instance.transform.SetParent(go.transform.parent);
            instance.transform.position = go.transform.position;
            instance.transform.rotation = go.transform.rotation;
            instance.transform.localScale = go.transform.localScale;
            
            // Remove the old object
            Undo.DestroyObjectImmediate(go);
        }
    }
}