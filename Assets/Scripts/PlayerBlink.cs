using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlink : Singleton<PlayerBlink>
{
    [SerializeField] bool isBlinking = false;
    [SerializeField] GameObject blinkPanel;
    public Slider blinkMeter;

    public event EventHandler OnBlink;

    public float eyeStrainTimer = 0;
    public float maxEyeStrainTimer = 5f;

    IEnumerator ForceBlinkCoRoutine;

    public GameObject eyeOpen;
    public GameObject eyeHalf;
    public GameObject eyeShut;

    void Start()
    {
        blinkMeter.minValue = 0;
        blinkMeter.maxValue = maxEyeStrainTimer;
        blinkMeter.value = maxEyeStrainTimer;
    }


    private void Update()
    {
        BlinkTimer();

        if (isBlinking)
        {
            eyeOpen.SetActive(false);
            eyeHalf.SetActive(false);
            eyeShut.SetActive(true);
        }
        else
        {

            if (eyeStrainTimer < 2)
            {
                eyeOpen.SetActive(false);
                eyeHalf.SetActive(true);
                eyeShut.SetActive(false);
            }
            else
            {
                eyeOpen.SetActive(true);
                eyeHalf.SetActive(false);
                eyeShut.SetActive(false);
            }
        }

    }

    void BlinkTimer()
    {
        if (!isBlinking && !PlayerDeathManager.Instance.isDead)
        {
            if (eyeStrainTimer >= 0)
            {
                eyeStrainTimer -= Time.deltaTime;

            }
            else
            {
                if (ForceBlinkCoRoutine == null)
                {
                    ForceBlinkCoRoutine = ForceBlink();
                    StartCoroutine(ForceBlinkCoRoutine);
                }
                else
                {
                    StopCoroutine(ForceBlinkCoRoutine);
                    ForceBlinkCoRoutine = ForceBlink();
                    StartCoroutine(ForceBlinkCoRoutine);
                }
            }
            // Set UI
            blinkMeter.value = eyeStrainTimer;
        }
    }

    IEnumerator ForceBlink()
    {
        BlinkDown();
        yield return new WaitForSeconds(0.2f);
        BlinkUp();
    }

    public void ResetEyeStrainMeter()
    {
        eyeStrainTimer = maxEyeStrainTimer;
    }

    public void BlinkDown()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            blinkPanel.SetActive(true);

            // call blink event
            if (OnBlink != null)
            {
                OnBlink(null, EventArgs.Empty);
            }

            ResetEyeStrainMeter();
        }
    }

    public void BlinkUp()
    {
        if (isBlinking)
        {
            isBlinking = false;
            blinkPanel.SetActive(false);
        }
    }






}
