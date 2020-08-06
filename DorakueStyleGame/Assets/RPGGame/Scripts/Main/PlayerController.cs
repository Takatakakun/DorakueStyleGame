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
        Normal,
        Talk
    }

    private PlayerState m_state;//プレイヤーの状態
    private CharacterController m_characterCtl;
    private Animator m_animator;
    private Vector3 m_velocity;

    public Transform m_verRot;       //縦の視点移動の変数(カメラに合わせる)
    public Transform m_horRot;       //横の視点移動の変数(プレイヤーに合わせる)
    public float m_jumpPower;    //ジャンプ力
    public float m_moveSpeed;    //移動速度

    private void Start()
    {
        m_state = PlayerState.Normal;
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

        }
        m_characterCtl.Move(m_velocity);
        m_velocity.y += Physics.gravity.y * Time.deltaTime;

    }

    //回転処理
    public void PlayerRot()
    {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        m_horRot.transform.Rotate(new Vector3(0, X_Rotation * 2, 0));
        m_verRot.transform.Rotate(-Y_Rotation * 2, 0, 0);

    }
    //移動処理
    public void PlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_characterCtl.Move(this.gameObject.transform.forward * m_moveSpeed * Time.deltaTime);
            m_animator.SetBool("run", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_characterCtl.Move(this.gameObject.transform.forward * -1.0f * m_moveSpeed * Time.deltaTime);
            m_animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_characterCtl.Move(this.gameObject.transform.right * -1.0f * m_moveSpeed * Time.deltaTime);
            m_animator.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_characterCtl.Move(this.gameObject.transform.right * m_moveSpeed * Time.deltaTime);
            m_animator.SetBool("run", true);
        }
    }
    //ジャンプ処理
    public void PlayerJump()
    {
        if (m_characterCtl.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_velocity.y = m_jumpPower;
            }
        }
    }
}
