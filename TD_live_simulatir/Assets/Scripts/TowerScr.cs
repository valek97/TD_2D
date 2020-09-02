using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerScr : MonoBehaviour
{
    public GameObject Projectile;
    Tower selfTower;
    public TowerType selfType;
    GameControllerScr gcs;

    private void Start()
    {
        gcs = FindObjectOfType<GameControllerScr>();
        selfTower = gcs.AllTowers[(int)selfType];
        //Помещаем спрайт в конструктор класса
        GetComponent<SpriteRenderer>().sprite = selfTower.Spr;
        //Повтор поиска врага раз в 1/10 секунды
        InvokeRepeating("SearchTarget", 0, .1f);
    }

    private void Update()
    {
        if (selfTower.CurrCooldown > 0)
            selfTower.CurrCooldown -= Time.deltaTime;
    }
    //Проверка, может ли башня стрелять
    bool CanShoot()
    {
        if (selfTower.CurrCooldown <= 0)
            return true;
        return false;
    }
    //Поиск ближайшей цели от башни
    void SearchTarget()
    {
        //Выполняться будет если башня может стрелять
        if (CanShoot())
        {
            Transform nearestEnemy = null;
            float nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                //Поиск ближайшего врага
                float currDistance = Vector2.Distance(transform.position, enemy.transform.position);
                //Сравнение дистанции с радиусом стрельбы башни
                if (currDistance < nearestEnemyDistance && currDistance <= selfTower.range)
                {
                    //Назначение дистанции до врага
                    nearestEnemy = enemy.transform;
                    nearestEnemyDistance = currDistance;
                }

            }

            if (nearestEnemy != null)
            {
                Shoot(nearestEnemy);
            }
        }
    }
    //Отвечает за выстрел башни
    //Принимает постоянно позицию врага в реальном времени
    void Shoot(Transform enemy)
    {
        selfTower.CurrCooldown = selfTower.Cooldown;

        GameObject proj = Instantiate(Projectile);
        proj.GetComponent<TowerProjectileScr>().selfTower = selfTower;
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileScr>().SetTarget(enemy);
    }


}
