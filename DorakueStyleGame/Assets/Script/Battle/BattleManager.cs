using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public Slider m_HpSlider;
    public Slider m_MpSlider;


    private void Start()
    {
        InitialValue();
    }

    private void Update()
    {
        if(m_playerStatus.GetHp()<=0)
        {
            m_playerStatus.SetHp(0);
        }
    }

    public void PushAttackButton()
    {
        Debug.Log("押された");
    }

    //初期値
    public void InitialValue()
    {
        //プレイヤーのステータス
        m_HpSlider.value = m_playerStatus.GetMaxHp();
        m_MpSlider.value = m_playerStatus.GetMaxMp();

        Debug.Log(m_playerStatus.GetMaxHp());
        Debug.Log(m_playerStatus.GetMaxMp());
    }
}
