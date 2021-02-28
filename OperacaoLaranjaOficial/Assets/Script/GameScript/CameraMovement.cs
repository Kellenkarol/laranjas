using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform[] transformPositions;
    [SerializeField] GameObject camReference;
    [SerializeField]float currentDistance;
    [SerializeField]Transform destiny;
    [SerializeField]string _cenaEmTela;
    GameControllerScript gm;
    public string CenaEmTela
    {
        get { return _cenaEmTela; }
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destiny != null)
        {
            currentDistance += Time.deltaTime;
            currentDistance = Mathf.Clamp(currentDistance, 0, 1);
            camReference.transform.position = Vector3.MoveTowards(camReference.transform.position, destiny.position, currentDistance);
            if (currentDistance >= 1)
            {
                destiny = null;
            }
        }
    }

    public void SetDestiny(int valueDestiny)
    {
        currentDistance = 0;
        destiny = transformPositions[valueDestiny];
        switch (valueDestiny)
        {
            case 2:
                _cenaEmTela = "Fase1";
                gm.inicializarFase(0);
                break;
            case 3:
                _cenaEmTela = "Fase2";
                gm.inicializarFase(1);
                break;
            case 4:
                _cenaEmTela = "Fase3";
                gm.inicializarFase(2);
                break;
            case 5:
                _cenaEmTela = "Fase4";
                gm.inicializarFase(3);
                break;
            default:
                _cenaEmTela = "ForaJogo";
                gm.inicializarFase(-1);
                break;
        }
    }
}
