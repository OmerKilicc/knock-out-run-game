using UnityEngine;

[CreateAssetMenu(fileName = "New Level Object", menuName = "Level/Object")]
public class LevelObject : ScriptableObject
{
	public uint ID;
	public string Name;
	public GameObject Prefab;
}
