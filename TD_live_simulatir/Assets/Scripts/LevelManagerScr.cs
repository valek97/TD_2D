using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class LevelManagerScr : MonoBehaviour
{
    public int fieldWidth, fieldHeight;
    public GameObject cellPref;

    public Transform cellParent;

    public Sprite[] tileSpr = new Sprite[2];

    List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[17,8];

    int currWayX, currWayY;
    GameObject firstCell;

    void Start()
    {
        CreateLevel();
        loadWaypoints();
    }
    void CreateLevel()
    {
        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height,0));

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
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));
        if (isGround)
        {
            tmpCell.GetComponent<CellScr>().isGround = true;

            if (firstCell == null)
            {
                firstCell = tmpCell;
                currWayX = x;
                currWayY = y;
            }
        }
    }
    string[] LoadLevelText( int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");

        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);
        return tmpStr.Split('!');
    }
    void loadWaypoints()
    {
        GameObject currWayGo;
        wayPoints.Add(firstCell);
        while (true)
        {
            currWayGo = null;

            if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
            {
                currWayGo = allCells[currWayY, currWayX - 1];
                currWayX--;
                Debug.Log("Next Cell is Left");
            }
            else if (currWayX < (fieldWidth - 1) && allCells[currWayY, currWayX + 1].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
            {
                currWayGo = allCells[currWayY, currWayX + 1];
                currWayX++;
                Debug.Log("Next Cell is Right");
            }
            else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<CellScr>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
            {
                currWayGo = allCells[currWayY - 1, currWayX];
                currWayX--;
                Debug.Log("Next Cell is Up");
            }
            else if (currWayY < (fieldHeight - 1) && allCells[currWayY + 1, currWayX].GetComponent<CellScr>().isGround &&
               !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
            {
                currWayGo = allCells[currWayY + 1, currWayX];
                currWayX++;
                Debug.Log("Next Cell is Down");
            }
            else
                break;
            wayPoints.Add(currWayGo);
        }
    }
}
