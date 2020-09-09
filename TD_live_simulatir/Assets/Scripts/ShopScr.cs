using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScr : MonoBehaviour
{
    //Панель
    public GameObject ItemPref;
    //Ячейка
    public Transform ItemGrid;

    GameControllerScr gcs;
    //Ячейка на которую нажали и где будет строиться башня
    public CellScr selfCell;
    void Start()
    {
        //Для каждого элемента списка башни будем создавать товар в магазине
        gcs = FindObjectOfType<GameControllerScr>();
        foreach(Tower tower in gcs.AllTowers)
        {   //Вставляем 0 объект башни из массива типов
            GameObject tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ShopItemScr>().SetStartData(tower, selfCell);
        }
    }
    //Закрытие магазина удаляет объект
    public void CloseShop()
    {
        Destroy(gameObject);
    }

}
