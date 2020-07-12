using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerScr : MonoBehaviour
{
    //Логика для башни
    //range - радиус действия башни
    float range = 2;
    //CurrCooldown - макс время отсчета, 
    //Cooldown - текущее время отсчета
    public float CurrCooldown, Cooldown;

    public GameObject Progectile;

    private void Update()
    {
        if (CanShoot())
            SearchTarget();

        if (CurrCooldown > 0)
            CurrCooldown -= Time.deltaTime;
    }
    //Проверка, может ли башня стрелять
    bool CanShoot()
    {
        if (CurrCooldown <= 0)
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
            if (currDistance < nearestEnemyDistance && currDistance <= range)
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
        CurrCooldown = Cooldown;

        GameObject proj = Instantiate(Progectile);
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProgectileScr>().SetTarget(enemy);
    }


}
