using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellScr : MonoBehaviour
{
    //Находится ли на ячейке башня
    public bool isGround, hasTower = false;
    // Базовая и застроенная расцветка
    public Color BaseColor, CurrColor, DestroyColor;

    public GameObject ShopPref, TowerPref;

    private void OnMouseEnter() //Наведение мыши
    {
       //Выделение ячейки при наведении
        if (!isGround && FindObjectsOfType<ShopScr>().Length == 0)
            if(!hasTower)
            GetComponent<SpriteRenderer>().color = CurrColor;
        else
            GetComponent<SpriteRenderer>().color = DestroyColor;
    }
    private void OnMouseExit()//Покидание мыши
    {
        GetComponent<SpriteRenderer>().color = BaseColor;
    }
    //Создает магазин при клике (ячейка пуста)
    private void OnMouseDown()
    {
        if (!isGround && FindObjectsOfType<ShopScr>().Length == 0)
            if (!hasTower)
            {
                var shopObj = Instantiate(ShopPref);

                shopObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                //Передача нажатой ячейки
                shopObj.GetComponent<ShopScr>().selfCell = this;
            }
    }
    //Строительство башни
    public void BuildTower (Tower tower)
    {
        GameObject tmpTower = Instantiate(TowerPref);
        tmpTower.transform.SetParent(transform, false);
         Vector2 towerPos = new Vector2(transform.position.x + tmpTower.GetComponent<SpriteRenderer>().bounds.size.x /2 ,
                                        transform.position.y - tmpTower.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        tmpTower.transform.position = towerPos;
        //tmpTower.transform.position = transform.position;
        tmpTower.GetComponent<TowerScr>().selfType = (TowerType)tower.type;
        hasTower = true;
        FindObjectOfType<ShopScr>().CloseShop();
    }
}
