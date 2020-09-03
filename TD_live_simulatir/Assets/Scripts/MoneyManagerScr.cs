using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagerScr : MonoBehaviour
{
    public static MoneyManagerScr Instance;
    public Text MoneyTxt;
    public int GameMoney;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        MoneyTxt.text = GameMoney.ToString();
    }
}
