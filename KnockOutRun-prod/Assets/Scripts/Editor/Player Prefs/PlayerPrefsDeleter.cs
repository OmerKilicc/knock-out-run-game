using UnityEditor;
using UnityEngine;

public class PlayerPrefsDeleter : EditorWindow
{
    [MenuItem("Tools/Edit Prefs/Delete All")]
	public static void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All Player Prefs Deleted!");
    }
    
    [MenuItem("Tools/Edit Prefs/Name")]
	public static void Init()
    {
        PlayerPrefsDeleter window = ScriptableObject.CreateInstance<PlayerPrefsDeleter>();

        window.titleContent.text = "Delete Player Pref";
        window.titleContent.tooltip = "Delete a spesific Player Pref by it's key.";
        window.position = new Rect(200, 50, 250, 100);
        window.maxSize = new Vector2(250, 50);
        window.minSize = new Vector2(250, 50);

        _input = string.Empty;
        _error = string.Empty;
        _failed = false;

        window.Show();
    }

    static string _input = string.Empty;
    static string _error = string.Empty;
    static bool _failed = false;

    void OnGUI()
    {
        _input = GUILayout.TextField(_input);

        if (GUILayout.Button("Delete"))
        {
            if (!PlayerPrefs.HasKey(_input))
            {
                _failed = true;
                _error = "No such key";
                this.maxSize = new Vector2(250, 60);
                this.minSize = new Vector2(250, 60);
                return;
            }

            this.maxSize = new Vector2(250, 50);
            this.minSize = new Vector2(250, 50);
            _failed = false;
            _error = string.Empty;

            PlayerPrefs.DeleteKey(_input);

            Debug.Log($"Player Pref with key {_input} has been deleted!");
        }

        if(_failed)
        {
            GUILayout.Label(_error);
        }
    }
}
