using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class CharacterStatus : ScriptableObject
{

    //キャラクターの名前
    [SerializeField]
    private string m_characterName = "";
    //毒状態かどうか
    [SerializeField]
    private bool m_isPoisonState = false;
    //痺れ状態かどうか
    [SerializeField]
    private bool m_isNumbnessState = false;
    //キャラクターのレベル
    [SerializeField]
    private int m_level = 1;
    //最大HP
    [SerializeField]
    private int m_maxHp = 100;
    //HP
    [SerializeField]
    private int m_hp = 100;
    //最大MP
    [SerializeField]
    private int m_maxMp = 50;
    //MP
    [SerializeField]
    private int m_mp = 50;
    //素早さ
    [SerializeField]
    private int m_agility = 5;
    //力
    [SerializeField]
    private int m_power = 10;
    //打たれ強さ
    [SerializeField]
    private int m_strikingStrength = 10;
    //魔法力
    [SerializeField]
    private int m_magicPower = 10;

    public void SetCharacterName(string characterName)
    {
        this.m_characterName = characterName;
    }

    public string GetCharacterName()
    {
        return m_characterName;
    }

    public void SetPoisonState(bool poisonFlag)
    {
        m_isPoisonState = poisonFlag;
    }

    public bool IsPoisonState()
    {
        return m_isPoisonState;
    }

    public void SetNumbness(bool numbnessFlag)
    {
        m_isNumbnessState = numbnessFlag;
    }

    public bool IsNumbnessState()
    {
        return m_isNumbnessState;
    }

    public void SetLevel(int level)
    {
        this.m_level = level;
    }

    public int GetLevel()
    {
        return m_level;
    }

    public void SetMaxHp(int hp)
    {
        this.m_maxHp = hp;
    }

    public int GetMaxHp()
    {
        return m_maxHp;
    }

    public void SetHp(int hp)
    {
        this.m_hp = Mathf.Max(0, Mathf.Min(GetMaxHp(), hp));
    }

    public int GetHp()
    {
        return m_hp;
    }

    public void SetMaxMp(int mp)
    {
        this.m_maxMp = mp;
    }

    public int GetMaxMp()
    {
        return m_maxMp;
    }

    public void SetMp(int mp)
    {
        this.m_mp = Mathf.Max(0, Mathf.Min(GetMaxMp(), mp));
    }

    public int GetMp()
    {
        return m_mp;
    }

    public void SetAgility(int agility)
    {
        this.m_agility = agility;
    }

    public int GetAgility()
    {
        return m_agility;
    }

    public void SetPower(int power)
    {
        this.m_power = power;
    }

    public int GetPower()
    {
        return m_power;
    }

    public void SetStrikingStrength(int strikingStrength)
    {
        this.m_strikingStrength = strikingStrength;
    }

    public int GetStrikingStrength()
    {
        return m_strikingStrength;
    }

    public void SetMagicPower(int magicPower)
    {
        this.m_magicPower = magicPower;
    }

    public int GetMagicPower()
    {
        return m_magicPower;
    }
}