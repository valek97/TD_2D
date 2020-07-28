using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerScr : MonoBehaviour
{
    public GameObject Progectile;
    Tower selfTower;
    public TowerType selfType;
    GameControllerScr gsc;

    private void Start()
    {
        gsc = FindObjectOfType<GameControllerScr>();
        selfTower = gsc.AllTowers[(int)selfType];
        //GetComponent<SpriteRenderer>().sprite = selfTower.Spr;
    }

    private void Update()
    {
        if (CanShoot())
            SearchTarget();

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
    //Отвечает за выстрел башни
    //Принимает постоянно позицию врага в реальном времени
    void Shoot(Transform enemy)
    {
        selfTower.CurrCooldown = selfTower.Cooldown;

        GameObject proj = Instantiate(Progectile);
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProgectileScr>().SetTarget(enemy);
    }


}
