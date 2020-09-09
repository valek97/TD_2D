using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{

    List<GameObject> wayPoints = new List<GameObject>();

    public Enemy SelfEnemy; 

    int wayIndex = 0;

    private void Start()
    {
        GetWaypoints();
        GetComponent<SpriteRenderer>().sprite = SelfEnemy.spr;
    }
    //Присвоение списку вейпоинтов список из менеджера уровней
    void GetWaypoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScr>().wayPoints;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        //Смещение  вектора перемещения к центру
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                            currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currWayPos - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * SelfEnemy.Speed);

        if (Vector3.Distance(transform.position, currWayPos) < 0.3f)
        {
            if (wayIndex < wayPoints.Count - 1)
            {
                wayIndex++;
            }
           else { Destroy(gameObject); }
               
        }
    }
    //Получение урона
    public void TakeDamage(float damage)
    {
        SelfEnemy.Health -= damage;
        CheckIsAlive();
    }
    //Уничтожение объекта при здоровье <= 0
    void CheckIsAlive()
    {
        if (SelfEnemy.Health <= 0)
        {
            //Зарабатывание бабла за убийства
            MoneyManagerScr.Instance.GameMoney += 10;
            Destroy(gameObject);
        }
            
    }  

    //Замедление врага
    public void StartSlow(float duration, float slowValue)
    {
        //Приостановка замедления (не будет стакаться)
        StopCoroutine("GetSlow");
        SelfEnemy.Speed = SelfEnemy.StartSpeed;
        StartCoroutine(GetSlow(duration, slowValue));
    }
    //Замедление конкретного врага на время
    IEnumerator GetSlow (float duration, float slowValue)
    {
        SelfEnemy.Speed -= slowValue;
        yield return new WaitForSeconds(duration);
        SelfEnemy.Speed = SelfEnemy.StartSpeed;
    }
    //Урон по площади
    public void AOEDamage(float range, float damage)
    {
        List<EnemyScr> enemies = new List<EnemyScr>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(transform.position, go.transform.position) <= range)
                enemies.Add(go.GetComponent<EnemyScr>());
        }
        foreach (EnemyScr es in enemies)
            es.TakeDamage(damage);
    }

}
