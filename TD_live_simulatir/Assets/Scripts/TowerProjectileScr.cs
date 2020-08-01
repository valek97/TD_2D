using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScr : MonoBehaviour
{

    Transform target;
    public TowerProjectile selfProjectile;
    private void Start()
    {
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
                target.GetComponent<EnemyScr>().TakeDamage(selfProjectile.damage);
                Destroy(gameObject);
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

}
