using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    PlayerStatus m_playerStatus = new PlayerStatus();

    public Slider m_HpSlider;
    public Slider m_MpSlider;

    private int m_playerHp;

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
        m_playerHp -= 10;
        m_playerStatus.SetHp(m_playerHp);
        m_HpSlider.value = m_playerHp;
        Debug.Log("押された");
    }

    //初期値
    public void InitialValue()
    {
        string datastr = "";

        //選んだデータの読み込み
        StreamReader reader = new StreamReader(Application.dataPath + "/Save/" + DisplaySaveData.m_selectDataName+ ".json");
        datastr = reader.ReadToEnd();
        reader.Close();

        //読み込んだデータをPlayerStatusに変換
        m_playerStatus = JsonUtility.FromJson<PlayerStatus>(datastr);

        //表示用UIに代入
        m_HpSlider.maxValue = m_playerStatus.GetMaxHp();
        m_HpSlider.value = m_playerStatus.GetHp();
        m_MpSlider.maxValue = m_playerStatus.GetMaxMp();
        m_MpSlider.value = m_playerStatus.GetMp();

        //用意した変数に代入
        m_playerHp = m_playerStatus.GetHp();
    }
}
