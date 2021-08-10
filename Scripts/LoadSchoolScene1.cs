using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSchoolScene1 : MonoBehaviour
{
    private SceneSetting _sceneSetting;

    private void Start()
    {
        _sceneSetting = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();
    }
    public void SchoolScene1()
    {
        _sceneSetting = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();
        _sceneSetting.SetChapter1();
        _sceneSetting.SetScene();
    }
}
