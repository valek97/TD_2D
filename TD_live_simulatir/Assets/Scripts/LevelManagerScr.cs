using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class LevelManagerScr : MonoBehaviour
{   //ширина и высота поля в ячейках
    public int fieldWidth, fieldHeight;
    //Хранение префаба
    public GameObject cellPref;

    //Объект родитель ячеек
    public Transform cellParent;

    public Sprite[] tileSpr = new Sprite[2];
    //Лист вейпоинтов
    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[8,17];

    //координаты ячейки
    int currWayX, currWayY;
    //1 ячейка пути
    GameObject firstCell;

    void Start()
    {
        CreateLevel();
        LoadWaypoints();
    }
    void CreateLevel()
    {
        // Появление объекта в определенных координатах. Верхний левый угол. Мировые координаторы камеры
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height,0));

        // 1 цикл проходит п овертикальному ряду, 2 цикл по горизонтали
        // Проекция уровня из текстового формата.
        for(int i = 0; i < fieldHeight; i++)
            for (int k = 0; k<fieldWidth;k++)
            {
                int sprIndex = int.Parse(LoadLevelText(1)[i].ToCharArray()[k].ToString());
                Sprite spr = tileSpr[sprIndex];
                bool isGround = spr == tileSpr[1] ? true : false;
                CreateCell(isGround, spr, k, i, worldVec);
            }
    }

    void CreateCell(bool isGround, Sprite spr, int x, int y,Vector3 wV)
    {
        GameObject tmpCell = Instantiate(cellPref);
        //Установить объект родитель
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        //Размер ячеек
        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        // Смена позиции ячейки
        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));
        
        if (isGround)
        {
            tmpCell.GetComponent<CellScr>().isGround = true;

            if (firstCell == null)
            {
                firstCell = tmpCell;
                //Индексы тек ячейки
                currWayX = x;
                currWayY = y;
            }
        }
        allCells[y, x] = tmpCell;
    }
    //Загрузка карты уровня из файла
    string[] LoadLevelText( int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");
        //Загрузка 1 массива, преобразование в 1 строку
        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);
        //Разделитель текста !
        return tmpStr.Split('!');
    }
    //
    void LoadWaypoints()
    { 
        GameObject currWayGo;
        wayPoints.Add(firstCell);
        //Цикл на установление пути и проверку ячейки на наличие соседнего пути
        while (true )
        {
            currWayGo = null;
            if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
            {
                currWayGo = allCells[currWayY, currWayX - 1];
                currWayX--;
                Debug.Log("Left");
            }
            else if (currWayX < (fieldWidth - 1) && allCells[currWayY, currWayX + 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
            {
                currWayGo = allCells[currWayY, currWayX + 1];
                currWayX++;
                Debug.Log("Right");
            }
            else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
            {
                currWayGo = allCells[currWayY - 1, currWayX];
                currWayY--;
                Debug.Log("Up");
            }
            else if (currWayY < (fieldHeight - 1) && allCells[currWayY + 1, currWayX].GetComponent<CellScr>().isGround &&
               !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
            {
                currWayGo = allCells[currWayY + 1, currWayX];
                currWayY++;
                Debug.Log("Down");
            }
            else
                break;
            wayPoints.Add(currWayGo);

        }
       

        Debug.Log("Stop");
    }
}
