using UnityEngine;

public class InitializationCanvas : MonoBehaviour
{
    private float load_time = 1.0f;
    private float load_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (load_timer < load_time)
            load_timer += Time.deltaTime;
        else
            gameObject.SetActive(false);
    }
}
