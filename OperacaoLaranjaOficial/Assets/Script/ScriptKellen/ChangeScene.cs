using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nomeDaCena;


    public void changeS()
    {
        SceneManager.LoadScene(nomeDaCena);

    }

    public void Sair()
    {
        Application.Quit();

    }
}
