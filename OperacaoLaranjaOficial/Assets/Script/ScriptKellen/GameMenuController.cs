using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{

    public GameObject gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        HideMenu ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (gameMenu.activeSelf)
                HideMenu();
            else
                ShowMenu();
        }
    }

    public void ShowMenu()
    {
        gameMenu.SetActive(true);
    }

    public void HideMenu()
    {
        gameMenu.SetActive(false);
    }
}
