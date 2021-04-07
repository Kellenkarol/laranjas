using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MartaPollaroid : MonoBehaviour
{
    [Tooltip("Influencia Marta Não editar")] [SerializeField] int _quantInfluencia;
    TextMeshPro textValueInfluence;

    [HideInInspector]
    public AudioSource mySound;


    public int InfluenciaMarta
    {
        get
        {
            return _quantInfluencia;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        textValueInfluence = GetComponentInChildren<TextMeshPro>();
        mySound = GameObject.Find("SFX/MartaPollaroid").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlterarInfluenciaMarta(int newInfluencia)
    {
        _quantInfluencia += newInfluencia;
        AtualizarUIInfluencia();
    }
    void AtualizarUIInfluencia()
    {
        if (_quantInfluencia > 0)
        {
            textValueInfluence.text = "" + _quantInfluencia;
        }
        else
        {
            textValueInfluence.text = "" + 0;
        }

    }
}
