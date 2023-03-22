using UnityEngine;

[CreateAssetMenu(fileName = "New Set Piece", menuName = "Level/Set Piece")]
public class SetPieceSO : ScriptableObject
{
    public GameObject Prefab;
    public float ZScale;
    public int MinLevel;
    public int MaxLevel;
}
