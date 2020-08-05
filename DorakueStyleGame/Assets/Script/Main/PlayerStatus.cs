using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    [SerializeField]
    private Vector3    m_position;//場所
    [SerializeField]
    private Quaternion m_rotation;//回転

    [SerializeField]
    private int        m_playerLevel;//レベル
    [SerializeField]
    private string     m_playerName;//名前
    [SerializeField]
    private int        m_playerMaxHp;//最大体力
    [SerializeField]
    private int        m_playerMaxMp;//最大魔力
    [SerializeField]
    private int        m_playerHp;//体力
    [SerializeField]
    private int        m_playerMp;//魔力

    //Get
    public int GetPlayerLevel()
    {
        return m_playerLevel;
    }
    public string GetPlayerName()
    {
        return m_playerName;
    }
    public int GetMaxHp()
    {
        return m_playerMaxHp;
    }
    public int GetMaxMp()
    {
        return m_playerMaxMp;
    }
    public int GetHp()
    {
        return m_playerHp;
    }
    public int GetMp()
    {
        return m_playerMp;
    }

    //Set
    public void SetPlayerLevel(int playerLevel)
    {
        this.m_playerLevel = playerLevel;
    }
    public void SetPlayerName(string playerName)
    {
        this.m_playerName = playerName;
    }
    public void SetMaxHp(int maxhp)
    {
        this.m_playerMaxHp = maxhp;
    }
    public void SetMaxMp(int maxmp)
    {
        this.m_playerMaxMp = maxmp;
    }
    public void SetHp(int hp)
    {
        this.m_playerHp = hp;
    }
    public void SetMp(int mp)
    {
        this.m_playerMp = mp;
    }

}
