using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    private PlayerController playerController;
    private BagManager bagManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bagManager = BagManager.Instance;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        playerController._show = !playerController._show;
        bagManager.ShowBag(playerController._show);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
