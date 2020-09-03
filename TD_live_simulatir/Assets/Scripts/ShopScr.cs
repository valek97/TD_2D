using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScr : MonoBehaviour
{
    public GameObject ItemPref;
    public Transform ItemGrid;

    GameControllerScr gcs;
    // Start is called before the first frame update
    void Start()
    {
        gcs = FindObjectOfType<GameControllerScr>();
        foreach(Tower tower in gcs.AllTowers)
        {
            GameObject tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ShopItemScr>().SetStartData(tower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
