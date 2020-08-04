using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlashing : MonoBehaviour
{
    private Text m_text;

    private float m_time;
    private float m_speed = 1.0f;//アルファ値の移動スピード

    private void Start()
    {
        m_text = GetComponent<Text>();
    }

    private void Update()
    {
        m_text.color = AlphaColor(m_text.color);
    }

    Color AlphaColor(Color color)
    {
        m_time += Time.deltaTime * 5.0f * m_speed;
        color.a = Mathf.Sin(m_time) * 0.5f + 0.5f;
        return color;
    }
}
