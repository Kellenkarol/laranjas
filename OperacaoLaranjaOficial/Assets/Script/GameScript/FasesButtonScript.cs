using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FasesButtonScript : MonoBehaviour
{
    [SerializeField] int numFase;
    public bool iteractableFaseButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerGame.Instance.FaseGame >= numFase)
        {
            iteractableFaseButton = true;

        }
        else
        {
            iteractableFaseButton = false;
        }
    }
}
