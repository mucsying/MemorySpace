using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using Inventory;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Plot;

public class EventsManager : MonoBehaviour
{
    public bool canBack = true;
    public string savePath;
    public List<string> savePaths;
    public TextMeshProUGUI save_auto_data;
    public TextMeshProUGUI save_1_date;
    public TextMeshProUGUI save_2_date;
    public TextMeshProUGUI save_3_date;
    private DialogManager _dialogManager;
    private BagManager _bagManager;
    private NoteBook.NoteBook _noteBook;

    private void Start()
    {
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_1.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_2.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_3.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_Auto.json");
        _dialogManager = DialogManager.Instance;
        _bagManager = BagManager.Instance;
        _noteBook = NoteBook.NoteBook.Instance;
    }

    public void BackGame()
    {
        if(canBack)
            GameObject.Destroy(gameObject);
    }

    public void BackMenu()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("SceneInfo"));
        StartCoroutine(nameof(Loading));
    }

    public void SetCanBack()
    {
        canBack = !canBack;
    }

    IEnumerator Loading()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("HomePage");
        while (!operation.isDone)
        {
            yield return null;
        }
    }


    PlayerSave CreateSave()
    {
        Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerSave playerSave = new PlayerSave
        {
            //SaveInfo
            year = System.DateTime.Now.Year,
            month = System.DateTime.Now.Month,
            day = System.DateTime.Now.Day,
            hour = System.DateTime.Now.Hour,
            min = System.DateTime.Now.Minute,
            sec = System.DateTime.Now.Second,

            //Transform
            object_name = trans.transform.name,
            pos_x = trans.position.x,
            pos_y = trans.position.y,
            pos_z = trans.position.z,

            rot_x = trans.eulerAngles.x,
            rot_y = trans.eulerAngles.y,
            rot_z = trans.eulerAngles.z,

            scl_x = trans.localScale.x,
            scl_y = trans.localScale.y,
            scl_z = trans.localScale.z,
            
            //NPC
            
            //Scene
            sceneName = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>().sceneName
         };
        if (GameObject.FindWithTag("Water"))
            playerSave.water = false;
        else
        {
            playerSave.water = true;
        }

        if (GameObject.Find("char_model_bullyA"))
            playerSave.bullyA = false;
        else
            playerSave.bullyA = true;
        
        if (GameObject.Find("char_model_bullyB"))
            playerSave.bullyB = false;
        else
            playerSave.bullyB = true;
        
        if (GameObject.Find("char_model_bullyC"))
            playerSave.bullyC = false;
        else
            playerSave.bullyC = true;
        
        return playerSave;
    }

    public void SetSavePath(int slot)
    {
        if (slot == 1)
            savePath = savePaths[0];
        else if (slot == 2)
            savePath = savePaths[1];
        else if (slot == 3)
            savePath = savePaths[2];
        else if (slot == 4)
            savePath = savePaths[3];
        _dialogManager.SaveDialog(slot);
        _bagManager.SaveBag(slot);
        _noteBook.SaveNoteBook(slot);
    }

    public void WriteSave()
    {
        PlayerSave playerSave = CreateSave();
        string saveStr = JsonMapper.ToJson(playerSave);
        StreamWriter sw = new StreamWriter(savePath);
        sw.Write(saveStr);
        sw.Close();
    }

    public void SetSaveInfo()
    {
        for(int i =0;i<savePaths.Count;i++)
        {
            if (File.Exists(savePaths[i]))
            {
                StreamReader sr = new StreamReader(savePaths[i]);
                string saveStr = sr.ReadToEnd();
                sr.Close();
                PlayerSave playerSave = JsonMapper.ToObject<PlayerSave>(saveStr);
                if(i==0)
                    save_1_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if(i==1)
                    save_2_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if(i==2)
                    save_3_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if(i==3)
                    save_auto_data.text = "上次游玩时间  " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;

            }
            else
            {
                if (i == 0)
                    save_1_date.text = "未建立存档";
                else if (i == 1)
                    save_2_date.text = "未建立存档";
                else if (i == 2)
                    save_3_date.text = "未建立存档";
                else if (i == 3)
                    save_auto_data.text = "未开始游戏";

            }
        }
    }

}
