﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BigBall : MonoBehaviour
{
    public TextMeshProUGUI textNumber;

    public Image imageBall;
    public Sprite bgBall;
    public Sprite bgBallLogo;
    public Animator animBall;
    public string numberBall;
    public GameObject trackBall;

    [SerializeField] private Color initialColor;
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    [SerializeField] private Image bgLineBall;

    public void Start()
    {
        SetBgBallWithLogo();
    }
    public void SetInfoInBigBall(int _numberBall, bool isAnim = true)
    {
        if (isAnim)
        {
            animBall.SetTrigger("isShow");
        }
        imageBall.sprite = bgBall;
        numberBall = _numberBall.ToString("D2");
        textNumber.text = _numberBall.ToString("D2");
        imageBall.enabled = true;
        ChangeColorBgLine();
        
        //GameManager.instance.globeScriptable.indexBalls++;

    }

    public void SetBgBallWithLogo()
    {
        imageBall.sprite = bgBallLogo;
        textNumber.text = string.Empty;
        imageBall.enabled = true;
        bgLineBall.color = initialColor;
    }

    private void ChangeColorBgLine()
    {
        int number = int.Parse(numberBall);
        if (number <= 30)
        {
            bgLineBall.color = firstColor;
            textNumber.color = firstColor;
        }
        else
        {
            bgLineBall.color = secondColor;
            textNumber.color = secondColor;
        }
    }

    public void SetBallWinner()
    {
        imageBall.color = Color.yellow;
    }

}