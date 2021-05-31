using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame Instance { get; private set; }
    [SerializeField]bool _lockPlayerActive;
    [SerializeField] int _faseGame;
    public bool LockPlayerActive
    {
        get { return _lockPlayerActive; }
        set { _lockPlayerActive = value; }
    }

    public int FaseGame
    {
        get { return _faseGame; }
        set { _faseGame = value; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (PlayerPrefs.GetInt("FaseConcluida") == 0)
            {
                _faseGame = 1;
                PlayerPrefs.SetInt("FaseConcluida", _faseGame);
            }
            else
            {
                _faseGame = PlayerPrefs.GetInt("FaseConcluida");
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("FaseConcluida", 1);
            _faseGame = 1;
        }
    }
}
