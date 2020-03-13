using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    private PlayerController pc;
    private GameObject text;
    private Text tt;

    void Start()
    {
        player = GameObject.Find("Player");
        text = GameObject.Find("Canvas/Text");
        pc = player.GetComponent<PlayerController>();
        tt = text.GetComponent<Text>();
    }

    void Update()
    {
        tt.text = "HP: " + pc.HP + "\n" + "SP: " + pc.SP;
    }
}
