using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSetting : MonoBehaviour
{
    [HideInInspector]
    public bool loadingSF=false;
    public PlayerSave playerSave;
    public bool load_saved=false;
    public bool player_load = false;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetContinue()
    {
        sceneName = "Continue";
    }

    public void SetPrologue()
    {
        sceneName = "Prologue";
    }

    public void SetChapter1()
    {
        sceneName = "Chapter_1";
    }

    public void SetChapter2()
    {
        sceneName = "Chapter_2";
    }

    public void SetScene()
    {
        if(sceneName!=null)
            StartCoroutine(nameof(Loading));
    }

    IEnumerator Loading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
        while (!operation.isDone)
        {
            yield return null;
        }
        loadingSF = true;
    }
}

