using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using Inventory;
using UnityEngine.SceneManagement;
using TMPro;
using Plot;

public class HomePageManager : MonoBehaviour
{
    public string savePath;
    public List<string> savePaths;
    public TextMeshProUGUI save_auto_data;
    public TextMeshProUGUI save_1_date;
    public TextMeshProUGUI save_2_date;
    public TextMeshProUGUI save_3_date;
    public string sceneName;
    private SceneSetting sceneSetting;
    private DialogManager dialogManager;
    private BagManager _bagManager;
    private NoteBook.NoteBook _noteBook; 

    private void Start()
    {
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_1.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_2.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_3.json");
        savePaths.Add(Application.dataPath + "/SaveFile" + "/Save_Auto.json");
        sceneSetting = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();
        dialogManager = DialogManager.Instance;
        _bagManager = BagManager.Instance;
        _noteBook = NoteBook.NoteBook.Instance;
    }

    public void SetSaveInfo()
    {
        for (int i = 0; i < savePaths.Count; i++)
        {
            if (File.Exists(savePaths[i]))
            {
                StreamReader sr = new StreamReader(savePaths[i]);
                string saveStr = sr.ReadToEnd();
                sr.Close();
                PlayerSave playerSave = JsonMapper.ToObject<PlayerSave>(saveStr);
                if (i == 0)
                    save_1_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if (i == 1)
                    save_2_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if (i == 2)
                    save_3_date.text = "存档建立于 " + playerSave.year + "-" + playerSave.month + "-" + playerSave.day + " " + playerSave.hour + ":" + playerSave.min + ":" + playerSave.sec;
                else if (i == 3)
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

    public void ReadSave(int slot)
    {
        if (slot == 1)
            savePath = savePaths[0];
        else if (slot == 2)
            savePath = savePaths[1];
        else if (slot == 3)
            savePath = savePaths[2];
        else if (slot == 4)
            savePath = savePaths[3];
        

        if (File.Exists(savePath))
        {
            StreamReader sr = new StreamReader(savePath);
            string saveStr = sr.ReadToEnd();
            sr.Close();
            PlayerSave playerSave = JsonMapper.ToObject<PlayerSave>(saveStr);
            sceneSetting.playerSave = playerSave;
            sceneSetting.sceneName = playerSave.sceneName;
            sceneSetting.load_saved = true;
        }
        else
            sceneSetting.sceneName = null;
        dialogManager.LoadDialog(slot);
        _bagManager.LoadBag(slot);
        _noteBook.LoadNoteBook(slot);
    }


}
