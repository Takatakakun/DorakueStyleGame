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
        ItemPanel,      //アイテム選択パネル
        ToolPanel,      //道具パネル
        UseItemPanel,   //アイテムの使用や渡す、捨てる等を表示するパネルの状態
        UseItemPanelToUseItemPanel,//アイテムを捨てるを選択した後にまだそのアイテムがある状態
        UseItemSelectCharacterPanelToUseItemPanel,//アイテムを使用、渡すを選択した後に使用する、渡す相手を選択した後の状態
        NoItemPassed//アイテムを使用、渡す、捨てるを選択した後にそのアイテムが0になり他のアイテムも一つも持っていない状態
    }
    [SerializeField][Header("PlayerStayusを入れる")]
    private AllyStatus m_playerStatus;
    private MenuMode m_menuMode;
    private PlayerMenu m_playerMenu;//プレイヤーのメニュースクリプト

    //最初に選択するButtonのTransform
    private GameObject m_firstSelectButton;
    //メニューパネル
    private GameObject m_menuPanel;
    //ステータス表示パネル
    private GameObject m_statusPanel;
    //アイテムパネル
    private GameObject m_itemPanel;

    //メニューパネルのCanvasGroup
    private CanvasGroup m_menuPanelCanvasGroup;

    //キャラクター名
    private Text m_characterNameText;
    //ステータスタイトルテキスト
    private Text m_statusTitleText;
    //ステータスパラメータテキスト1
    private Text m_statusParam1Text;
    //ステータスパラメータテキスト2
    private Text m_statusParam2Text;

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
        m_itemPanel = transform.Find("ItemPanel").gameObject;
        //CanvasGroupを取得
        m_menuPanelCanvasGroup = m_menuPanel.GetComponent<CanvasGroup>();
        //ステータス用テキストを取得
        m_characterNameText = m_statusPanel.transform.Find("CharacterNamePanel/Text").GetComponent<Text>();
        m_statusTitleText = m_statusPanel.transform.Find("StatusParamPanel/Title").GetComponent<Text>();
        m_statusParam1Text = m_statusPanel.transform.Find("StatusParamPanel/Param1").GetComponent<Text>();
        m_statusParam2Text = m_statusPanel.transform.Find("StatusParamPanel/Param2").GetComponent<Text>();
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
    }

    private void Update()
    {
        //キャンセルボタンを押した時の処理
        if (Input.GetButtonDown("B_Button"))
        {
            //メニュー選択画面時
            if (m_menuMode == MenuMode.MenuPanel)
            {
                m_playerMenu.ExitMenu();
                gameObject.SetActive(false);
            }
            //ステータス表示時
            else if (m_menuMode == MenuMode.StatusPanel)
            {
                m_statusPanel.SetActive(false);
                m_menuMode = MenuMode.MenuPanel;
            }
            //アイテム選択画面時
            else if (m_menuMode == MenuMode.ItemPanel)
            {
                m_itemPanel.SetActive(false);

                //前のパネルで選択していたゲームオブジェクトを選択
                EventSystem.current.SetSelectedGameObject(selectedGameObjectStack.Pop());
                m_menuPanelCanvasGroup.interactable = true;
                m_menuMode = MenuMode.MenuPanel;
            }
        }
    }

    //選択したコマンドで処理分け
    public void SelectCommand(string command)
    {
        if (command == "Status")
        {
            m_menuMode = MenuMode.StatusPanel;
            //UIのオン・オフや選択アイコンの設定
            m_menuPanelCanvasGroup.interactable = false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
            GetComponent<Button>().onClick.AddListener(() => ShowPlayerStatus(m_playerStatus));
        }
        else if(command == "Item")
        {
            m_menuMode = MenuMode.ItemPanel;
            m_statusPanel.SetActive(false);
            m_itemPanel.SetActive(true);
            //UIのオン・オフや選択アイコンの設定
            m_menuPanelCanvasGroup.interactable = false;
            selectedGameObjectStack.Push(EventSystem.current.currentSelectedGameObject);
            //GetComponent<Button>().onClick.AddListener(() => ShowPlayerStatus(m_playerStatus));
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
}
