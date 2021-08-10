using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOpenPrelude : MonoBehaviour
{
    // Start is called before the first frame update
    public void Prelude()
    {
        SceneManager.LoadScene("DoctorOffice");
    }
}
