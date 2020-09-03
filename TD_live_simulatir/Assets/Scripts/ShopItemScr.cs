using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemScr : MonoBehaviour
{
    Tower selfTower;
    public Image TowerLogo;
    public Text TowerName, TowerPrice;
    public void SetStartData(Tower tower)
    {
        selfTower = tower;
        TowerLogo.sprite = tower.Spr;
        TowerName.text = tower.Name;
        TowerPrice.text = tower.Price.ToString();
    }
}
