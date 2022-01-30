using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Light : MonoBehaviour
{
    public GameObject lightObj;

    void Start()
    {
        // Subscribe to lights on
        GameStateManager.Instance.OnLightsOn += delegate (object sender, EventArgs e)
        {
            TurnOnLight();
        };
    }

    void TurnOnLight()
    {
        lightObj.SetActive(true);
    }
}
