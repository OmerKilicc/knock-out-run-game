using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudSpawner))]
public class CloudSpawnerInspector : Editor
{
    CloudSpawner _target;

    private void OnEnable()
    {
        _target = (CloudSpawner)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn"))
            _target.Spawn();

        if (GUILayout.Button("Delete All"))
            _target.DeleteAll();
    }
}
