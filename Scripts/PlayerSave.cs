using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    //SaveInfo
    public string save_name;
    public int year, month, day, hour, min, sec;

    //Transform
    public string object_name;
    public float pos_x, pos_y, pos_z;
    public float rot_x, rot_y, rot_z;
    public float scl_x, scl_y, scl_z;

    //Object
    public bool notebook;
    public bool tissue;
    public bool water;

    //NPC
    public bool bullyA;
    public bool bullyB;
    public bool bullyC;
    
    //Scene
    public string sceneName;
}
