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
    public float speed;
    public int damage;
    public Sprite Spr;

    public TowerProjectile(float speed, int damage, string path)
    {
        this.speed = speed;
        this.damage = damage;
        Spr = Resources.Load<Sprite>(path);
        //GameObject.Find("FTower").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Tower/FTower");
    }

}

public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER
}


public class GameControllerScr : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectile> AllProjectile = new List<TowerProjectile>();

    private void Awake()
    {
        AllTowers.Add(new Tower(2, .3f, "Tower/Ftower"));
        AllTowers.Add(new Tower(5, 1, "Tower/STower"));

        AllProjectile.Add(new TowerProjectile(7, 10, "ProjectileSprites/FProjectile"));
        AllProjectile.Add(new TowerProjectile(3, 30, "ProjectileSprites/SProjectile"));
    }

}
