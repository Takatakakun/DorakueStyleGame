using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの行動関係の処理
/// </summary>
public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Normal,//通常
        Talk,  //話す
        Wait,   //待機
        Menu
    }

    private PlayerState         m_state;        //プレイヤーの状態
    private CharacterController m_characterCtl;
    private Animator            m_animator;
    private Vector3             m_velocity;     //速度

    public float                m_jumpPower;    //ジャンプ力
    public float                m_moveSpeed;    //移動速度

    private void Start()
    {
        m_state = PlayerState.Wait;
        m_characterCtl = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (m_state == PlayerState.Normal)
        {
            PlayerRot();
            PlayerMove();
            PlayerJump();
        }
        else if (m_state == PlayerState.Talk)
        {

        }else if (m_state == PlayerState.Wait)
        {

        }

        //シーン遷移後に移動させるとデフォルトの位置にキャラクターがセットされてしまうので回避用
        if (m_state != PlayerState.Wait)
        {
            m_velocity.y += Physics.gravity.y * Time.deltaTime;
            m_characterCtl.Move(m_velocity * Time.deltaTime);
        }
        else
        {
            //遷移後パッドを動かすとステータスをノーマルに
            if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0.0f) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0.0f))
            {
                SetState(PlayerState.Normal);
            }
        }
    }

    //回転処理
    public void PlayerRot()
    {
        //float X_Rotation = Input.GetAxis("Mouse X");
        //float Y_Rotation = Input.GetAxis("Mouse Y");
        //m_horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0));
        //m_verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);

    }
    //移動処理
    public void PlayerMove()
    {
        m_velocity = Vector3.zero;

        var input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (input.magnitude > 0.1f)
        {
            transform.LookAt(transform.position + input.normalized);
            m_animator.SetBool("run", true);
            m_velocity += transform.forward * m_moveSpeed;
        }
        else
        {
            m_animator.SetBool("run", false);
        }
    }
    //ジャンプ処理
    public void PlayerJump()
    {
        if (m_characterCtl.isGrounded)
        {
            if (Input.GetButtonDown("A_Button"))
            {
                m_velocity.y = m_jumpPower;
            }
        }
    }
    //ステータス変更
    public void SetState(PlayerState state)
    {
        this.m_state = state;

        if (state == PlayerState.Talk)
        {
            m_velocity = Vector3.zero;
            m_animator.SetBool("run", false);
        }
        else if(state == PlayerState.Wait)
        {
            m_velocity = Vector3.zero;
            m_animator.SetBool("run", false);
        }
        else if (state == PlayerState.Menu)
        {
            m_velocity = Vector3.zero;
            m_animator.SetBool("run", false);
        }
    }

    //ステータス取得
    public PlayerState GetState()
    {
        return m_state;
    }
}
