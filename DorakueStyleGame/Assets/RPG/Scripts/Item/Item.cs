using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        HPRecovery,
        MPRecovery,
        PoisonRecovery,
        NumbnessRecovery,
        WeaponAll,
        WeaponUnityChan,
        WeaponYuji,
        ArmorAll,
        ArmorUnityChan,
        ArmorYuji,
        Valuables
    }

    //アイテムの種類
    [SerializeField]
    public Type itemType = Type.HPRecovery;
    //アイテムの漢字名
    [SerializeField]
    private string m_kanjiName = "";
    //アイテムの平仮名名
    [SerializeField]
    private string m_hiraganaName = "";
    //アイテム情報
    [SerializeField]
    private string m_information = "";
    //アイテムのパラメータ
    [SerializeField]
    private int m_amount = 0;

    //アイテムの種類を返す
    public Type GetItemType()
    {
        return itemType;
    }
    //アイテムの名前を返す
    public string GetKanjiName()
    {
        return m_kanjiName;
    }
    //アイテムの平仮名の名前を返す
    public string GetHiraganaName()
    {
        return m_hiraganaName;
    }
    //アイテム情報を返す
    public string GetInformation()
    {
        return m_information;
    }
    //アイテムの強さを返す
    public int GetAmount()
    {
        return m_amount;
    }
}