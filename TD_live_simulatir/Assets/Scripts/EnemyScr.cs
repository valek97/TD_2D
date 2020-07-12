using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScr : MonoBehaviour
{

    List<GameObject> wayPoints = new List<GameObject>();

    int wayIndex = 0;
    int speed = 5;
    public int health = 30;

    private void Start()
    {
        GetWaypoints();   
    }
    //Присвоение списку вейпоинтов список из менеджера уровней
    void GetWaypoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScr>().wayPoints;
    }
    void Update()
    {
        Move();
        CheckIsAlive();
    }
    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        //Смещение  вектора перемещения к центру
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                            currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        Vector3 dir = currWayPos - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * speed);

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
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    //Уничтожение объекта при здоровье <= 0
    void CheckIsAlive()
    {
        if (health <= 0)
            Destroy(gameObject);
    }  

}
