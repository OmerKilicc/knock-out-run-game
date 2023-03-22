using Euphrates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeReference] IntSO _currentLevel;

    [Header("Validation"), Space]
    [SerializeReference] TriggerChannelSO _validated;

    [Header("Triggers and Channels"), Space]
    [SerializeReference] SaveChannelSO _save;
    [SerializeReference] TriggerChannelSO _setLevel;
    [SerializeReference] TriggerChannelSO _levelGenerated;
    [SerializeReference] TriggerChannelSO _restartLevel;
    [SerializeReference] TriggerChannelSO _nextLevel;

    [Header("UI"), Space]
    [SerializeReference] GameObject _loadingScreen;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        _validated.AddListener(VersionValidated);

        _levelGenerated.AddListener(LevelReady);
        _restartLevel.AddListener(LoadCurrentLevel);
        _nextLevel.AddListener(LoadNexLevel);
    }

    private void OnDisable()
    {
        _validated.RemoveListener(VersionValidated);

        _levelGenerated.RemoveListener(LevelReady);
        _restartLevel.RemoveListener(LoadCurrentLevel);
        _nextLevel.RemoveListener(LoadNexLevel);
    }

    void VersionValidated()
    {
        _save.Load();
        LoadLevel(null);
    }


    #region Load / Unload Cycle
    void LoadLevel(AsyncOperation _)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        load.completed += SceneLoaded;
    }

    void SceneLoaded(AsyncOperation op)
    {
        // Trigger event that sets the level enviroment
        _setLevel.Invoke();
    }

    void LevelReady()
    {
        // Disable loading screen
        _loadingScreen.SetActive(false);
    }

    void LoadNexLevel()
    {
        _currentLevel.Value++;
        _save.Save();
        LoadCurrentLevel();
    }

    void LoadCurrentLevel()
    {
        // Enable loading screen
        _loadingScreen.SetActive(true);

        // Increment current level and start loading processs
        AsyncOperation op = SceneManager.UnloadSceneAsync(1);
        op.completed += LoadLevel;
    }
    #endregion
}
