using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class panelManager : MonoBehaviour
{
    public GameObject girisPaneli;

    void Start()
    {
        girisPaneli.GetComponent<CanvasGroup>().DOFade(0,5f);
    }

    
}
