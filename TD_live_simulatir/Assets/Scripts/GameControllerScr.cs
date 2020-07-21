using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    //Логика для башни
    //range - радиус действия башни
    //CurrCooldown - макс время отсчета, 
    //Cooldown - текущее время отсчета
    public float range, Cooldown, CurrCooldown = 0;
    public Sprite Spr;

   public Tower(float range, float Cooldown, string path)
    {
        this.range = range;
        this.Cooldown = Cooldown;
        Spr = Resources.Load<Sprite>(path);
    }

}

public class TowerProjectile
{

}

public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER
}


public class GameControllerScr : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();

    private void Awake()
    {
        AllTowers.Add(new Tower(2, .3f, "TowerSprites/FTower"));
        AllTowers.Add(new Tower(5, 1, "TowerSprites/STower"));
    }

}
