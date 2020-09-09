using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagerScr : MonoBehaviour
{
    //Экземпляр класса
    public static MoneyManagerScr Instance;
    //Текстовое поле с деньгами
    public Text MoneyTxt;
    //Начальная сумма денег
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
