using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoading : MonoBehaviour
{
    public Text cur_index;
    public Image process;
    public GameObject PrologueImg;
    public GameObject Chapter_1_Img;
    public GameObject Chapter_2_Img;
    private string sceneName;
    private SceneSetting sceneSetting;
    // Start is called before the first frame update
    void Start()
    {
        sceneSetting =GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();
        sceneName = sceneSetting.sceneName;
        LoadBgImg();
    }

    private void Update()
    {
        if (sceneSetting.loadingSF)
        {
            StartCoroutine(nameof(Loading));
            sceneSetting.loadingSF = false;
        }
    }

    IEnumerator Loading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        int index = 0;
        while (!operation.isDone)
        {
            if (index < operation.progress * 100)
                index +=2;
            if (operation.progress > 0.9)
                index = 100;
            cur_index.text = index.ToString() + "%";
            process.fillAmount = 1 - index / 10;
            yield return null;
        }
    }

    private void LoadBgImg()
    {
        if (sceneName == "Continue")
        {

        }
        else if (sceneName == "Prologue")
        {
            PrologueImg.SetActive(true);
        }
        else if (sceneName == "Chapter_1")
        {
            Chapter_1_Img.SetActive(true);
        }
        else if (sceneName == "Chapter_2")
        {
            Chapter_2_Img.SetActive(true);
        }
    }
}
