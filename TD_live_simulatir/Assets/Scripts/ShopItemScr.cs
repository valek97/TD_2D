using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemScr : MonoBehaviour , IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    //Объект башни
    Tower selfTower;
     //Нажатая ячейка
    CellScr selfCell;
    //Изображение башни
    public Image TowerLogo;
    //Название,цена
    public Text TowerName, TowerPrice;

    public Color BaseColor, CurrColor;
    public void SetStartData(Tower tower, CellScr cell)
    {
        selfTower = tower;
        TowerLogo.sprite = tower.Spr;
        TowerName.text = tower.Name;
        TowerPrice.text = tower.Price.ToString();
        selfCell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)//Нажал
    {
        //Списывание денег на покупку
        if (MoneyManagerScr.Instance.GameMoney >= selfTower.Price)
        {
            selfCell.BuildTower(selfTower);
            MoneyManagerScr.Instance.GameMoney -= selfTower.Price;
        }
    }

    public void OnPointerExit(PointerEventData eventData)//Отвел
    {
        //Подсветка  на базовый цвет
        GetComponent<Image>().color = BaseColor;
    }

    public void OnPointerEnter(PointerEventData eventData)//Наведение
    {
        //Подсветка при наведении
        GetComponent<Image>().color = CurrColor;
    }
}
