using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame Instance { get; private set; }
    [SerializeField]bool _lockPlayerActive;

    public bool LockPlayerActive
    {
        get { return _lockPlayerActive; }
        set { _lockPlayerActive = value; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
