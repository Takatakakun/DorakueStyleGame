using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public enum MenuMode
    {
        MenuPanel,      //メニューパネル
        StatusPanel,    //ステータスパネル
        ItemSelectPanel,      //アイテム選択パネル
        ToolPanel,      //道具パネル
        UseItemPanel,   //アイテムの使用や渡す、捨てる等を表示するパネルの状態
        UseItemPanelToUseItemPanel,//アイテムを捨てるを選択した後にまだそのアイテムがある状態
        UseItemSelectCharacterPanelToUseItemPanel,//アイテムを使用、渡すを選択した後に使用する、渡す相手を選択した後の状態
        NoItemPassed//アイテムを使用、渡す、捨てるを選択した後にそのアイテムが0になり他のアイテムも一つも持っていない状態
    }
    [SerializeField]
    [Header("PlayerStayusを入れる")]
    private AllyStatus m_playerStatus;
    private MenuMode m_menuMode;
    private PlayerMenu m_playerMenu;//プレイヤーのメニュースクリプト

    //最初に選択するButtonのTransform
    private GameObject m_firstSelectButton;
    //メニューパネル
    private GameObject m_menuPanel;
    //ステータス表示パネル
    private GameObject m_statusPanel;
    //アイテム選択パネル
    private GameObject m_itemSelectPanel;
    //道具パネル
    private GameObject m_toolPanel;
    //道具パネルボタンを表示する場所
    private GameObject m_content;
    //道具を使う選択パネル
    private GameObject m_useToolPanel;
    //情報表示パネル
    private GameObject m_itemInformationPanel;
    //アイテム使用後の情報表示パネル
    private GameObject m_useItemInformationPanel;

    //メニューパネルのCanvasGroup
    private CanvasGroup m_menuPanelCanvasGroup;
    //アイテム選択パネルのCanvasGroup
    private CanvasGroup m_itemSelectPanelCanvasGroup;
    //道具パネルのCanvasGroup
    private CanvasGroup m_toolPanelCanvasGroup;
    //道具を使う選択パネルのCanvasGroup
    private CanvasGroup m_useToolPanelCanvasGroup;

    //キャラクター名
    private Text m_characterNameText;
    //ステータスタイトルテキスト
    private Text m_statusTitleText;
    //ステータスパラメータテキスト1
    private Text m_statusParam1Text;
    //ステータスパラメータテキスト2
    private Text m_statusParam2Text;
    //　情報表示タイトルテキスト
    private Text m_informationTitleText;
    //　情報表示テキスト
    private Text m_informationText;

    //アイテム使用時のボタンのプレハブ
    [SerializeField]
    private GameObject m_useItemPanelButtonPrefab = null;
    //　アイテムボタン一覧
    private List<GameObject> m_itemPanelButtonList = new List<GameObject>();

    //最後に選択していたゲームオブジェクトをスタック
    private Stack<GameObject> selectedGameObjectStack = new Stack<GameObject>();

    void Awake()
    {
        //メニュー画面を開く処理をしているPlayerMenuを取得
        m_playerMenu = GameObject.FindWithTag("Player").GetComponent<PlayerMenu>();
        //現在のメニューを初期化
        m_menuMode = MenuMode.MenuPanel;
        //階層を辿ってを取得
        m_firstSelectButton = transform.Find("MenuPanel/StatusButton").gameObject;
        //パネル系を取得
        m_menuPanel = transform.Find("MenuPanel").gameObject;
        m_statusPanel = transform.Find("StatusPanel").gameObject;
        m_itemSelectPanel = transform.Find("ItemSelectPanel").gameObject;
        m_toolPanel = transform.Find("ToolPanel").gameObject;
        m_content = m_toolPanel.transform.Find("Mask/Content").gameObject;
        m_useToolPanel = transform.Find("UseToolPanel").gameObject;
        m_itemInformationPanel = transform.Find("ItemInformationPanel").gameObject;
        m_useItemInformationPanel = transform.Find("UseItemInformationPanel").gameObject;

        //CanvasGroupを取得
        m_menuPanelCanvasGroup = m_menuPanel.GetComponent<CanvasGroup>();
        m_itemSelectPanelCanvasGroup = m_itemSelectPanel.GetComponent<CanvasGroup>();
        m_toolPanelCanvasGroup = m_toolPanel.GetComponent<CanvasGroup>();
        m_useToolPanelCanvasGroup = m_useToolPanel.GetComponent<CanvasGroup>();

        //ステータス用テキストを取得
        m_characterNameText = m_statusPanel.transform.Find("CharacterNamePanel/Text").GetComponent<Text>();
        m_statusTitleText = m_statusPanel.transform.Find("StatusParamPanel/Title").GetComponent<Text>();
        m_statusParam1Text = m_statusPanel.transform.Find("StatusParamPanel/Param1").GetComponent<Text>();
        m_statusParam2Text = m_statusPanel.transform.Find("StatusParamPanel/Param2").GetComponent<Text>();

        //　情報表示用テキスト
        m_informationTitleText = m_itemInformationPanel.transform.Find("Title").GetComponent<Text>();
        m_informationText = m_itemInformationPanel.transform.Find("Information").GetComponent<Text>();

    }

    //メニューパネルがアクティブになったら実行
    private void OnEnable()
    {
        //現在のメニューを初期化
        m_menuMode = MenuMode.MenuPanel;
        //メニュー表示時に他のパネルは非表示にする
        m_statusPanel.SetActive(false);

        selectedGameObjectStack.Clear();

        m_menuPanelCanvasGroup.interactable = true;

        EventSystem.current.SetSelectedGameObject(m_firstSelectButton);


        m_toolPanel.SetActive(false);
        m_useToolPanel.SetActive(false);
        m_itemInformationPanel.SetActive(false);
        m_useItemInformationPanel.SetActive(false);

        //アイテムパネルボタンがあれば全て削除
        for (int i = m_content.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_content.transform.GetChild(i).gameObject);
        }
        //アイテムを使うキャラクター選択ボタンがあれば全て削除
        for (int i = m_useToolPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(m_useToolPanel.transform.GetChild(i).gameObject);
        }

        m_itemPanelButtonList.Clear();

        m_toolPanelCanvasGroup.interactable = false;
        m_useToolPanelCanvasGroup.interactable = false;

    }

    private void Update()
    {
        //キャンセルボタンを押した時の処理
        if (Input.GetButtonDown("B_Button"))
        {
            //メニュー選択画面時
            if (m_menuMode == MenuMode.MenuPanel)
            {
                m_playerMenu.ExitMenu();//メニュー閉じる時に行う関数
                gameObject.SetActive(false);//このスクリプトがアタッチされているオブジェクトを非アクティブに
            }
            //ステータス表示時
            else if (m_menuMode == MenuMode.StatusPanel)
            {
                m_statusPanel.SetActive(false);
                m_menuMode = MenuMode.MenuPanel;
            }
            //どのアイテムを表示するかの選択時
            else if (m_menuMode == MenuMode.ItemSelectPanel)
            {
                m_itemSelectPanelCanvasGroup.interactable = false;
                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                m_menuPanelCanvasGroup.interactable = true;
                m_menuMode = MenuMode.MenuPanel;
            }
            //道具一覧表示時
            else if (m_menuMode == MenuMode.ToolPanel)
            {
                m_toolPanelCanvasGroup.interactable = false;
                m_toolPanel.SetActive(false);
                m_itemInformationPanel.SetActive(false);
                //リストをクリア
                m_itemPanelButtonList.Clear();
                //ToolPanelでCancelを押したらcontent以下のアイテムパネルボタンを全削除
                for (int i = m_content.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(m_content.transform.GetChild(i).gameObject);
                }

                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                m_itemSelectPanelCanvasGroup.interactable = true;
                m_menuMode = MenuMode.ItemSelectPanel;
            }
            ////アイテム一覧表示時
            //else if (m_menuMode == MenuMode.ToolPanel)
            //{
            //    m_itemPanelCanvasGroup.interactable = false;
            //    m_itemPanel.SetActive(false);
            //    m_itemInformationPanel.SetActive(false);
            //    //　リストをクリア
            //    m_itemPanelButtonList.Clear();
            //    //　ItemPanelでCancelを押したらcontent以下のアイテムパネルボタンを全削除
            //    for (int i = m_content.transform.childCount - 1; i >= 0; i--)
            //    {
            //        Destroy(m_content.transform.GetChild(i).gameObject);
            //    }

            //    EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
            //}
            ////アイテムを選択し、どう使うかを選択している時
            //else if (m_menuMode == MenuMode.UseItemPanel)
            //{
            //    m_useItemPanelCanvasGroup.interactable = false;
            //    m_useToolPanel.SetActive(false);
            //    //　UseItemPanelでCancelボタンを押したらUseItemPanelの子要素のボタンの全削除
            //    for (int i = m_useToolPanel.transform.childCount - 1; i >= 0; i--)
            //    {
            //        Destroy(m_useToolPanel.transform.GetChild(i).gameObject);
            //    }

            //    EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
            //    m_itemPanelCanvasGroup.interactable = true;
            //    m_menuMode = MenuMode.ItemPanel;
            //}
        }

        ////　アイテムを捨てるを選択した後の状態
        //if (m_menuMode == MenuMode.UseItemPanelToUseItemPanel)
        //{
        //    if (Input.anyKeyDown
        //        || !Mathf.Approximately(Input.GetAxis("Horizontal"), 0f)
        //        || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f)
        //        )
        //    {
        //        m_menuMode = MenuMode.UseItemPanel;
        //        m_useItemInformationPanel.SetActive(false);
        //        m_useToolPanel.transform.SetAsLastSibling();
        //        m_useItemPanelCanvasGroup.interactable = true;
        //    }
        //}
        ////　アイテムを使用、渡す、捨てるを選択した後にそのアイテムの数が0になった時
        //else if (m_menuMode == MenuMode.NoItemPassed)
        //{
        //    if (Input.anyKeyDown
        //        || !Mathf.Approximately(Input.GetAxis("Horizontal"), 0f)
        //        || !Mathf.Approximately(Input.GetAxis("Vertical"), 0f)
        //        )
        //    {
        //        m_menuMode = MenuMode.ItemPanel;
        //        m_useItemInformationPanel.SetActive(false);
        //        m_useToolPanel.SetActive(false);
        //        m_itemPanel.transform.SetAsLastSibling();
        //        m_itemPanelCanvasGroup.interactable = true;

        //        //アイテムパネルボタンがあれば最初のアイテムパネルボタンを選択
        //        if (m_content.transform.childCount != 0)
        //        {
        //            EventSystem.current.SetSelectedGameObject(m_content.transform.GetChild(0).gameObject);
        //        }
        //        else
        //        {
        //            //　アイテムパネルボタンがなければ（アイテムを持っていない）ItemSelectPanelに戻る
        //            //currentCommand = CommandMode.ItemPanelSelectCharacter;
        //            //itemPanelCanvasGroup.interactable = false;
        //            //itemPanel.SetActive(false);
        //            //selectCharacterPanelCanvasGroup.interactable = true;
        //            //selectCharacterPanel.SetActive(true);
        //            //EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
        //        }
        //    }
        //}
    }
    //ボタンが押されたときコマンドを設定することにより
    public void SelectCommand(string command)
    {
        if (command == "Status")
        {
            m_menuMode = MenuMode.StatusPanel;
            m_menuPanelCanvasGroup.interactable = false;
            //selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
            GetComponent<Button>().onClick.AddListener(() => ShowPlayerStatus(m_playerStatus));
        }
        else if (command == "Item")
        {
            m_menuMode = MenuMode.ItemSelectPanel;
            m_statusPanel.SetActive(false);
            m_itemSelectPanel.SetActive(true);
            m_menuPanelCanvasGroup.interactable = false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
        }
        else if (command == "ToolItem")
        {
            m_menuMode = MenuMode.ToolPanel;
            m_toolPanel.SetActive(true);
            m_itemSelectPanelCanvasGroup.interactable = false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
           
            GetComponent<Button>().onClick.AddListener(() => CreateToolPanelButton(m_playerStatus));
        }

    }

    //プレイヤーのステータス表示
    public void ShowPlayerStatus(AllyStatus playerStatus)
    {
        m_menuMode = MenuMode.StatusPanel;
        m_statusPanel.SetActive(true);
        //キャラクターの名前を表示
        m_characterNameText.text = playerStatus.GetCharacterName();

        //タイトルの表示
        var text = "レベル\n";
        text += "経験値\n";
        text += "HP\n";
        text += "MP\n";
        text += "素早さ\n";
        text += "攻撃力\n";
        text += "魔法力\n";
        text += "防御力\n";
        text += "魔法防御力\n";
        text += "装備武器\n";
        text += "装備防具\n";
        text += "所持金\n";
        m_statusTitleText.text = text;

        //HPとMPのDivision記号の表示
        text = "\n";
        text += "\n";
        text += playerStatus.GetHp() + "\n";
        text += playerStatus.GetMp() + "\n";
        m_statusParam1Text.text = text;

        //　ステータスパラメータの表示
        text = playerStatus.GetLevel() + "\n";
        text += playerStatus.GetEarnedExperience() + "\n";
        text += playerStatus.GetMaxHp() + "\n";
        text += playerStatus.GetMaxMp() + "\n";
        text += playerStatus.GetAgility() + "\n";
        text += playerStatus.GetPower() + (playerStatus.GetEquipWeapon()?.GetPowerAmount() ?? 0) + "\n";
        text += playerStatus.GetMagicPower() + (playerStatus.GetEquipWeapon()?.GetMagicAmount() ?? 0) + "\n";
        text += playerStatus.GetPowerDefense() + (playerStatus.GetEquipArmor()?.GetPowerAmount() ?? 0) + "\n";
        text += playerStatus.GetMagicDefense() + (playerStatus.GetEquipArmor()?.GetMagicAmount() ?? 0) + "\n";
        text += playerStatus?.GetEquipWeapon()?.GetKanjiName() ?? "";
        text += "\n";
        text += playerStatus.GetEquipArmor()?.GetKanjiName() ?? "";
        text += "\n";
        text += playerStatus.GetEarnedMoney() + "\n";
        m_statusParam2Text.text = text;
    }

    //キャラクターが持っている道具のボタン表示
    public void CreateToolPanelButton(AllyStatus m_playerStatus)
    {
        m_itemInformationPanel.SetActive(true);

        //　アイテムパネルボタンを何個作成したかどうか
        int itemPanelButtonNum = 0;
        GameObject itemButtonIns;
        //　選択したキャラクターのアイテム数分アイテムパネルボタンを作成
        //　持っているアイテム分のボタンの作成とクリック時の実行メソッドの設定
        foreach (var item in m_playerStatus.GetItemDictionary().Keys)
        {
            itemButtonIns = Instantiate<GameObject>(m_useItemPanelButtonPrefab, m_content.transform);
            itemButtonIns.transform.Find("ItemName").GetComponent<Text>().text = item.GetKanjiName();
            itemButtonIns.GetComponent<Button>().onClick.AddListener(() => SelectItem(m_playerStatus, item));
            //itemButtonIns.GetComponent<ItemPanelButtonScript>().SetParam(item);

            //　アイテム数を表示
            itemButtonIns.transform.Find("Num").GetComponent<Text>().text = m_playerStatus.GetItemNum(item).ToString();

            //　アイテムボタンリストに追加
            m_itemPanelButtonList.Add(itemButtonIns);
            //　アイテムパネルボタン番号を更新
            itemPanelButtonNum++;
        }

        ////アイテムパネルの表示と最初のアイテムの選択
        //if (m_content.transform.childCount != 0)
        //{
        //    //SelectCharacerPanelで最後にどのゲームオブジェクトを選択していたか
        //    selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
        //    m_menuMode = MenuMode.ToolPanel;
        //    itemPanel.SetActive(true);
        //    itemPanel.transform.SetAsLastSibling();
        //    itemPanelCanvasGroup.interactable = true;
        //    EventSystem.current.SetSelectedGameObject(content.transform.GetChild(0).gameObject);
        //}
        //else
        //{
        //    informationTitleText.text = "";
        //    informationText.text = "アイテムを持っていません。";
        //    selectCharacterPanelCanvasGroup.interactable = true;
        //}
    }

    //道具をどうするかの選択
    public void SelectItem(AllyStatus allyStatus, Item item)
    {
       if (item.GetItemType() == Item.Type.HPRecovery|| item.GetItemType() == Item.Type.MPRecovery)
        {

            var itemMenuButtonIns = Instantiate<GameObject>(m_useItemPanelButtonPrefab, m_useToolPanel.transform);
            itemMenuButtonIns.GetComponentInChildren<Text>().text = "使う";
            //itemMenuButtonIns.GetComponent<Button>().onClick.AddListener(() => UseItem(allyStatus, item));

            itemMenuButtonIns = Instantiate<GameObject>(m_useItemPanelButtonPrefab, m_useToolPanel.transform);
            itemMenuButtonIns.GetComponentInChildren<Text>().text = "捨てる";
            //itemMenuButtonIns.GetComponent<Button>().onClick.AddListener(() => ThrowAwayItem(allyStatus, item));

            m_useToolPanel.SetActive(true);
            m_toolPanelCanvasGroup.interactable = false;
            m_menuMode = MenuMode.UseItemPanel;
            //　ItemPanelで最後にどれを選択していたか？
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);

            m_useToolPanel.transform.SetAsLastSibling();
            EventSystem.current.SetSelectedGameObject(m_useToolPanel.transform.GetChild(0).gameObject);
            m_useToolPanelCanvasGroup.interactable = true;
            Input.ResetInputAxes();

        }

    }

    //道具を使用する
    //public void UseItem(AllyStatus allyStatus, Item item)
    //{
    //    useItemPanelCanvasGroup.interactable = false;
    //    //UseItemPanelでどれを最後に選択していたか
    //    selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);

    //    GameObject characterButtonIns;
    //    //　パーティメンバー分のボタンを作成
    //    foreach (var member in partyStatus.GetAllyStatus())
    //    {
    //        characterButtonIns = Instantiate<GameObject>(characterPanelButtonPrefab, useItemSelectCharacterPanel.transform);
    //        characterButtonIns.GetComponentInChildren<Text>().text = member.GetCharacterName();
    //        characterButtonIns.GetComponent<Button>().onClick.AddListener(() => UseItemToCharacter(allyStatus, member, item));
    //    }
    //    //　UseItemSelectCharacterPanelに移行する
    //    currentCommand = CommandMode.UseItemSelectCharacterPanel;
    //    useItemSelectCharacterPanel.transform.SetAsLastSibling();
    //    EventSystem.current.SetSelectedGameObject(useItemSelectCharacterPanel.transform.GetChild(0).gameObject);
    //    useItemSelectCharacterPanelCanvasGroup.interactable = true;
    //    Input.ResetInputAxes();
    //}


    ////　捨てる
    //public void ThrowAwayItem(AllyStatus allyStatus, Item item)
    //{
    //    //　アイテム数を減らす
    //    allyStatus.SetItemNum(item, allyStatus.GetItemNum(item) - 1);
    //    //　アイテム数が0になった時
    //    if (allyStatus.GetItemNum(item) == 0)
    //    {

    //        //　装備している武器を捨てる場合の処理
    //        if (item == allyStatus.GetEquipArmor())
    //        {
    //            var equipArmorButton = itemPanelButtonList.Find(itemPanelButton => itemPanelButton.transform.Find("ItemName").GetComponent<Text>().text == item.GetKanjiName());
    //            equipArmorButton.transform.Find("Equip").GetComponent<Text>().text = "";
    //            equipArmorButton = null;
    //            allyStatus.SetEquipArmor(null);
    //        }
    //        else if (item == allyStatus.GetEquipWeapon())
    //        {
    //            var equipWeaponButton = itemPanelButtonList.Find(itemPanelButton => itemPanelButton.transform.Find("ItemName").GetComponent<Text>().text == item.GetKanjiName());
    //            equipWeaponButton.transform.Find("Equip").GetComponent<Text>().text = "";
    //            equipWeaponButton = null;
    //            allyStatus.SetEquipWeapon(null);
    //        }
    //    }
    //    //　ItemPanelの子要素のアイテムパネルボタンから該当するアイテムのボタンを探して数を更新する
    //    var itemButton = itemPanelButtonList.Find(obj => obj.transform.Find("ItemName").GetComponent<Text>().text == item.GetKanjiName());
    //    itemButton.transform.Find("Num").GetComponent<Text>().text = allyStatus.GetItemNum(item).ToString();
    //    useItemInformationPanel.GetComponentInChildren<Text>().text = item.GetKanjiName() + "を捨てました。";

    //    //　アイテム数が0だったらボタンとキャラクターステータスからアイテムを削除
    //    if (allyStatus.GetItemNum(item) == 0)
    //    {
    //        selectedGameObjectStack.Pop();
    //        itemPanelButtonList.Remove(itemButton);
    //        Destroy(itemButton);
    //        allyStatus.GetItemDictionary().Remove(item);

    //        currentCommand = CommandMode.NoItemPassed;
    //        useItemPanelCanvasGroup.interactable = false;
    //        useItemPanel.SetActive(false);
    //        useItemInformationPanel.transform.SetAsLastSibling();
    //        useItemInformationPanel.SetActive(true);
    //        //　ItemPanelに戻る為UseItemPanelの子要素のボタンを全削除
    //        for (int i = useItemPanel.transform.childCount - 1; i >= 0; i--)
    //        {
    //            Destroy(useItemPanel.transform.GetChild(i).gameObject);
    //        }
    //    }
    //    else
    //    {
    //        useItemPanelCanvasGroup.interactable = false;
    //        useItemInformationPanel.transform.SetAsLastSibling();
    //        useItemInformationPanel.SetActive(true);
    //        currentCommand = CommandMode.UseItemPanelToUseItemPanel;
    //    }

    //    Input.ResetInputAxes();

    //}
}
