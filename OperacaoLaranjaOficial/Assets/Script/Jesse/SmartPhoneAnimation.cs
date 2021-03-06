﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhoneAnimation : MonoBehaviour
{
    public  Animator animPhone, animBlur, animScreen;
    private bool animFinished=true, animInOut;

    // Start is called before the first frame update
    void Start()
    {
        // StartAnim();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void StartAnim()
    {
        print("SmartPhoneWithoutAnim");
        if(animFinished)
        {
            animFinished=false;
            animInOut = !animInOut;
            animPhone.SetBool("InOut", animInOut);
            animBlur.SetBool("InOut", animInOut);
            if(animInOut)
            {
                animScreen.SetBool("OnOff", animInOut);
            }
            StartCoroutine("WaitFinishAnim");

        }
    }

    private IEnumerator WaitFinishAnim()
    {
        yield return new WaitForSeconds(1);
        animFinished = true;
    }

    public bool GetIfIsShowing()
    {
        return animInOut;
    }

    public void ScreenOnOff()
    {
        // print("DEBUG");
        animScreen.SetBool("OnOff", animInOut);
    }

}
