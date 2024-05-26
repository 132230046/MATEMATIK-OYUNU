using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class zamanManager : MonoBehaviour
{
    public float zaman ;
    public Text zamanText;
    private bool oyunBitti;
    [SerializeField]
    private GameObject sonucPaneli;

    void Start()
    {
        zaman = 100;
        zamanText.text = "" + (int)zaman;
    }
    void Update()
    {
        if (!oyunBitti)
        {
            zaman -= Time.deltaTime;
            zamanText.text = "" + (int)zaman;
            if (zaman <= 0)
            {
                oyunuBitir();
                Debug.Log("Oyun Bitti");
            }
        }
        void oyunuBitir()
        {
            oyunBitti = true;
            sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        }
    }
}