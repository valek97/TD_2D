using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScr : MonoBehaviour
{

    Transform target;
    TowerProjectile selfProjectile;
    public Tower selfTower;
    GameControllerScr gcs;
    private void Start()
    {
        
        gcs = FindObjectOfType<GameControllerScr>();
        selfProjectile = gcs.AllProjectile[selfTower.type];
        GetComponent<SpriteRenderer>().sprite = selfProjectile.Spr;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }
    //Полет пули
    private void Move()
    { 
        if (target != null) 
        {   
            //Если дистанция пули меньше 1, то удаляем её
            if (Vector2.Distance(transform.position, target.position) < 1f)
            {
                Hit();
            }
            else
            {
                //Скорость и определение позиции пули и цели
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectile.speed);
            }
        }
        else Destroy(gameObject);
    }
    //Нанесение урона при попадании
    void Hit()
    {
        switch (selfTower.type)
        {
            case (int)TowerType.FIRST_TOWER:
                target.GetComponent<EnemyScr>().StartSlow(3,1);
                target.GetComponent<EnemyScr>().TakeDamage(selfProjectile.damage);
                break;
            case (int)TowerType.SECOND_TOWER:
                target.GetComponent<EnemyScr>().AOEDamage(2, selfProjectile.damage);
                break;

        }
        Destroy(gameObject);
    }

}
