using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class gameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dairePrefab;

    [SerializeField]
    private Transform dairePaneli;

    [SerializeField]
    private Text soruText;
    private GameObject[] dairelerDizisi = new GameObject[25];

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Transform zamanPaneli;

    [SerializeField]
    private GameObject durPaneli;

    [SerializeField]
    private Text gameOverText;


    List<int> DegerlerListesi = new List<int>();

    int ilkSayi, ikinciSayi;
    int kacinciSoru;
    int dogruSonuc;
    int butonDegeri;
    bool ButonaBasilsinmi;
    int kalanHak;

    string sorununZorlukDerecesi;
    hakManager hakManager;
    puanManager puanManager;

    [SerializeField]
    private GameObject sonucPaneli;

    [SerializeField]
    AudioSource AudioSource;

    public AudioClip butonSesi;
    private void Awake()
    {
        kalanHak = 3;
        AudioSource = GetComponent<AudioSource>();

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
       
        hakManager=Object.FindObjectOfType<hakManager>();
       
        puanManager=Object.FindObjectOfType<puanManager>();
       
        hakManager.kalanHaklariKontrolEt(kalanHak);
    }

    void Start()
    {
        ButonaBasilsinmi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        daireleriOlustur();
    }
    public void daireleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject daire = Instantiate(dairePrefab, dairePaneli);
            daire.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            dairelerDizisi[i] = daire;
        }
        DegerleriTexteYazdir();

        StartCoroutine(DoFadeRoutine());

        Invoke("soruPaneliniAc", 2f);
    }
    public void durPaneliniAc()
    {
        durPaneli.SetActive(true);
    }
    void ButonaBasildi()
    {
        if(ButonaBasilsinmi)
        {
            AudioSource.PlayOneShot(butonSesi);
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            SonucuKontrolEt();
        }
        
    }
    void SonucuKontrolEt()
    {
        if(butonDegeri == dogruSonuc)
        {
            puanManager.puaniArttir(sorununZorlukDerecesi);
            DegerlerListesi.RemoveAt(kacinciSoru);
            Debug.Log(DegerlerListesi.Count);
            if(DegerlerListesi.Count > 0)
            {
                soruPaneliniAc();
            }
            else
            {
                OyunBitti();
            }
            
        }
        else
        {
            kalanHak--;
            hakManager.kalanHaklariKontrolEt(kalanHak);
        }
        if (kalanHak <= 0)
        {
            OyunBitti();
        }
    }
    void OyunBitti()
    {
        ButonaBasilsinmi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);

        if (DegerlerListesi.Count == 0)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
            Text zamanText = zamanPaneli.GetComponentInChildren<Text>();
            zamanText.enabled = false;
        }
    }
    IEnumerator DoFadeRoutine()
    {
        foreach (var daire in dairelerDizisi)
        {
            daire.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    void DegerleriTexteYazdir()
    {
       

        for (int i = 1; i < 26; i++)
        {
            int rastgeleDeger;

            // Yeniden rastgele deðer al, eðer bölüm listesinde yoksa ekle.
            do
            {
                rastgeleDeger = Random.Range(1, 26);
            } 
            while (DegerlerListesi.Contains(rastgeleDeger));

            DegerlerListesi.Add(rastgeleDeger);
            dairelerDizisi[i - 1].transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();
        }
    }
    void soruPaneliniAc()
    {
       
        soruyuSor();
        ButonaBasilsinmi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    void soruyuSor()
    {
        int ilkSayi = Random.Range(2, 11);
        int ikinciSayi = Random.Range(2, 11);
        kacinciSoru = Random.Range(0, DegerlerListesi.Count);
        dogruSonuc = DegerlerListesi[kacinciSoru];
      
        if(ilkSayi<10)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else
        {
            if(ilkSayi>=10 && ilkSayi <= 30)
            {
                sorununZorlukDerecesi = "orta";
            }
            else
            {
                    sorununZorlukDerecesi = "zor";
            }
        }
        int operation = Random.Range(0,4); // 0: toplama, 1: çýkarma, 2: çarpma, 3: bölme

        switch (operation)
        {
            case 0:
                 
                soruText.text = ilkSayi.ToString() + " + " + ikinciSayi.ToString();
                dogruSonuc = ilkSayi + ikinciSayi;
                
                break;
            case 1:
                if (ilkSayi <= ikinciSayi)
                {
                    ilkSayi = ikinciSayi + 2;
                }
                soruText.text = ilkSayi.ToString() + " - " + ikinciSayi.ToString();
                dogruSonuc = ilkSayi - ikinciSayi;
                ikinciSayi= Random.Range(2, ilkSayi);
                if (ikinciSayi > ilkSayi)
                {
                    int temp = ilkSayi;
                    ilkSayi = ikinciSayi;
                    ikinciSayi = temp;
                }

                break;
            case 2:
                ilkSayi = Random.Range(2, 5);
                ikinciSayi = Random.Range(2,5);
                soruText.text = ilkSayi.ToString() + " * " + ikinciSayi.ToString();
                dogruSonuc = ilkSayi * ikinciSayi;
                break;
            case 3:

                if (ikinciSayi != 0)
                {
                    ikinciSayi = Random.Range(2, 11);
                    ilkSayi = ikinciSayi * dogruSonuc;
                    soruText.text = ilkSayi.ToString() + " / " + ikinciSayi.ToString();
                }
                break;
            default:
                kacinciSoru = 0;
                break;
        }
    }
    }