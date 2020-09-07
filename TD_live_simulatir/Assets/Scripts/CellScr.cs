using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellScr : MonoBehaviour
{
    //Находится ли на ячейке башня
    public bool isGround, hasTower = false;
    // Базовая и застроенная расцветка
    public Color BaseColor, CurrColor;

    public GameObject ShopPref;

    private void OnMouseEnter()
    {
        if (!isGround && FindObjectsOfType<ShopScr>().Length == 0)
            GetComponent<SpriteRenderer>().color = CurrColor;
        
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = BaseColor;
    }
    private void OnMouseDown()
    {
        if (!isGround && FindObjectsOfType<ShopScr>().Length == 0)
            if (!hasTower)
            {
                var shopObj = Instantiate(ShopPref);

                shopObj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            }
    }
}
