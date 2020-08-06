using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    //クリックしたら次のシーンへ
    public void OnClickStart()
    {
        SceneManager.LoadScene("CastleTown");
    }
}
