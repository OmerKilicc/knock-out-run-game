using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data Holder", menuName = "Level/Holder", order = 0)]
public class LevelsDataSO : ScriptableObject
{
	public List<PremadeLevel> PremadeLevels;

    [Tooltip("Set pieces for generated levels.")]
	public List<SetPieceSO> SetPieces;
}

[System.Serializable]
public struct PremadeLevel
{
	public int Level;
	public GameObject LevelPrefab;
	public float ZEnd;
}
