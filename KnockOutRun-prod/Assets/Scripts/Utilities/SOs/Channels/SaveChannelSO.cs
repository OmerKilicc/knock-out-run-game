using Euphrates;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Channel", menuName = "SO Channels/Save")]
public class SaveChannelSO : ScriptableObject
{
	public SaveData DefaultValues;

	[Header("Saved data fields")]
	[SerializeReference] IntSO _level;
	[SerializeReference] IntSO _coin;

	public void Save()
    {
		var data = new SaveData(_level, _coin);
		var dataStr = JsonUtility.ToJson(data);

		PlayerPrefs.SetString("SaveData", dataStr);
		PlayerPrefs.Save();
    }

	public void Load()
    {
		bool firstPlay = !PlayerPrefs.HasKey("SaveData");

		if (firstPlay)
        {
			LoadData(DefaultValues);
			return;
        }

        try
        {
			var dataStr = PlayerPrefs.GetString("SaveData");
			var data = JsonUtility.FromJson<SaveData>(dataStr);

			LoadData(data);
		}
        catch (System.Exception)
        {
			LoadData(DefaultValues);
		}
    }

	void LoadData(SaveData data)
    {
		_level.Value = data.Level;
		_coin.Value = data.Coin;
    }
}

[System.Serializable]
public struct SaveData
{
	public int Level;
	public int Coin;

    public SaveData(int level, int coin)
    {
		Level = level;
		Coin = coin;
    }
}
