using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ValidationManager : MonoBehaviour
{
    [SerializeReference] TriggerChannelSO _validated;
    [SerializeReference] TriggerChannelSO _validationFailed;

//#if UNITY_EDITOR

    private void Start()
    {
        _validated.Invoke();
    }

//#else
//    private void Awake()
//    {
//        if (!Debug.isDebugBuild)
//        {
//            _validated.Invoke();
//            return;
//        }

//        string url = "http://firatyildirim.com.s3-website.eu-central-1.amazonaws.com/demo-data.json";
//        StartCoroutine(GetRequest(url));
//    }

//    IEnumerator GetRequest(string uri)
//    {
//        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
//        {
//            yield return webRequest.SendWebRequest();

//            string[] pages = uri.Split('/');
//            int page = pages.Length - 1;

//            switch (webRequest.result)
//            {
//                case UnityWebRequest.Result.ConnectionError:
//                case UnityWebRequest.Result.DataProcessingError:
//                case UnityWebRequest.Result.ProtocolError:
//                    _validationFailed.Invoke();
//                    break;
//                case UnityWebRequest.Result.Success:
//                    HandleValidation(webRequest.downloadHandler.text);
//                    break;
//            }
//        }
//    }

//    void HandleValidation(string jsonStr)
//    {
//        try
//        {
//            string appVer = Application.version.ToString();
//            var data = JsonUtility.FromJson<Root>(jsonStr);
//            foreach (var game in data.games)
//            {
//                if (game.name == Application.identifier)
//                {
//                    foreach (var ver in game.enabledVersions)
//                    {
//                        if (ver != appVer)
//                            continue;

//                        _validated.Invoke();
//                        return;
//                    }
//                }
//            }
            
//        }
//        catch
//        {
//        }

//        _validationFailed.Invoke();
//    }
//#endif
}

[Serializable]
public class Game
{
    public string name;
    public List<string> enabledVersions;
}

[Serializable]
public class Root
{
    public string enabled;
    public List<Game> games;
}
