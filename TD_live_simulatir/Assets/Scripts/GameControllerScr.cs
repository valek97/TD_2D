using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    //Логика для башни
    //range - радиус действия башни
    //CurrCooldown - макс время отсчета, 
    //Cooldown - текущее время отсчета
    //Type для снарядов
    public int type;
    public float range, Cooldown, CurrCooldown = 0;
    public Sprite Spr;

    //Башни
   public Tower(int type, float range, float Cooldown, string path)
    {
        this.type = type;
        this.range = range;
        this.Cooldown = Cooldown;
        Spr = Resources.Load<Sprite>(path);
    }

}
//Снаряды башен
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

public class Enemy
{
    public float Health, Speed, StartSpeed;
    public Sprite spr;
    public Enemy (float health, float speed, string sprPath){
        Health = health;
        StartSpeed = Speed = speed;

        spr = Resources.Load<Sprite>(sprPath);
    }
    public Enemy (Enemy other)
    {
        Health = other.Health;
        StartSpeed = Speed = other.StartSpeed;
        spr = other.spr; 
    }
   
}
public enum TowerType
{
    FIRST_TOWER,
    SECOND_TOWER
}


public class GameControllerScr : MonoBehaviour
{
    //Список типа башен
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectile> AllProjectile = new List<TowerProjectile>();
    public List<Enemy> AllEnemies = new List<Enemy>();

    private void Awake()
    {
        AllTowers.Add(new Tower(0, 2, .3f, "Tower/Ftower"));
        AllTowers.Add(new Tower(1, 5, 2f, "Tower/STower"));

        AllProjectile.Add(new TowerProjectile(7, 10, "ProjectileSprites/FProjectile"));
        AllProjectile.Add(new TowerProjectile(5, 30, "ProjectileSprites/SProjectile"));

        AllEnemies.Add(new Enemy(30, 3, "EnemySprites/enemy1"));
        AllEnemies.Add(new Enemy(2, 4, "EnemySprites/enemy2"));
    }

}
