using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject generalMenu;
    public GameObject optionsMenu;
   

    // Start is called before the first frame update
    void Start()
    {
        ActiveMenu  (generalMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void HideMenus()
    {
        generalMenu.SetActive   (false);
        optionsMenu.SetActive    (false);
    }

    public void ActiveMenu(GameObject menu)
    {
        HideMenus   ();
        menu.SetActive  (true);
    }
}
