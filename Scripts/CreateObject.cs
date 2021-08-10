using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [Header("Player")]
    public List<GameObject> m_players;
    [Header("NPC")]
    public List<GameObject> m_NPCs;
    [Header("Object")]
    public List<GameObject> m_objects;

    private PlayerSave playerSave;
    private SceneSetting sceneSetting;
    private GameObject camerControl;
    private Vector3 relative_pos = new Vector3(0, 1.6f, 0.15f);

    // Start is called before the first frame update
    void Start()
    {
        sceneSetting = GameObject.FindGameObjectWithTag("SceneInfo").GetComponent<SceneSetting>();
        playerSave = sceneSetting.playerSave;
        if (!sceneSetting.load_saved)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
            return;
        }
        GameObject.FindGameObjectWithTag("Player").tag = "TransNPC";
        foreach (GameObject m_player in m_players)
        {
            if (m_player.name == playerSave.object_name)
            {
                m_player.tag = "Player";
                GameObject.FindGameObjectWithTag("CameraContainer").transform.parent = m_player.transform;
                GameObject.FindGameObjectWithTag("CameraContainer").transform.localPosition = relative_pos;
                m_player.GetComponent<PlayerController>().enabled = true;
                if (m_player.name != "InitialState")
                {
                    foreach (GameObject m_play in m_players)
                    {
                        if (m_play.GetComponent<InitPlayerControl>())
                            Destroy(m_play);
                    }
                }
                break;
            }
        }

        foreach(GameObject m_NPC in m_NPCs)
        {
            if (m_NPC.name == "char_model_bullyA")
            {
                if(playerSave.bullyA)
                    Destroy(GameObject.Find("char_model_bullyA"));
            }
            if (m_NPC.name == "char_model_bullyB")
            {
                if(playerSave.bullyB)
                    Destroy(GameObject.Find("char_model_bullyB"));
            }
            if (m_NPC.name == "char_model_bullyC")
            {
                if(playerSave.bullyC)
                    Destroy(GameObject.Find("char_model_bullyC"));
            }
        }

        foreach(GameObject m_object in m_objects)
        {
            if (m_object == null)
            {
                continue;
            }
            if(m_object.name=="Water")
            {
                if (playerSave.water)
                    GameObject.Destroy(m_object);
            }
            if (m_object.name == "Notebook")
            {
                if (playerSave.notebook)
                    GameObject.Destroy(m_object);
            }
            if (m_object.name == "Tissue")
            {
                if (playerSave.tissue)
                    GameObject.Destroy(m_object);
            }

        }
    } 
}
