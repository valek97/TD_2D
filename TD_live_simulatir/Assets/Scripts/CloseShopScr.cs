using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseShopScr : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Button CloseButton;
    void Start()
    {
        CloseButton.onClick.AddListener(Close);
    }
    public void Close()
    {
        Destroy(gameObject);
    }
}
