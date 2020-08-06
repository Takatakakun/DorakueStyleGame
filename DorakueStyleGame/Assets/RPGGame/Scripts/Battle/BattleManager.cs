using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

    public Slider m_HpSlider;
    public Slider m_MpSlider;

    private int m_playerHp;

    private void Start()
    {
        InitialValue();
    }

    private void Update()
    {
    }

    public void PushAttackButton()
    {
    }

    //初期値
    public void InitialValue()
    {
    }
}
