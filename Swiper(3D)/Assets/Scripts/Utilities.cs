using System;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    /*
     * Bu Class a gerek kalmadi.
     * 
     */

    private Func<bool> timerCallBack;
    private float timer;

    public void RunBoolFunctionAfterSeconds(float countdown, Func<bool> function)
    {
        timer = countdown;
        timerCallBack = function;
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                timerCallBack();
            }
        }
    }
}
