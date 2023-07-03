using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject buyBtn;
	  [SerializeField] private TextMeshProUGUI adText;
    [SerializeField] private Init initSDK;
    public static int WeaponIndex = 0;
    [SerializeField] private Init _init;
    [SerializeField] private TextMeshProUGUI _coinsValueText;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Item[] _items;
    [SerializeField] private TextMeshProUGUI _currentBuletsCountText;
    [SerializeField] private TextMeshProUGUI _bulletsInMagazineText;

    [SerializeField] private Animator _uiAnimator;


     
    [SerializeField] private GameObject _bulletsCountPanel;
    [SerializeField] private GameObject _infinityBulletsSign;
    [SerializeField] private TextMeshProUGUI _infinityTimeText;
    [SerializeField] private GameObject _infinityButton;


   
    [Header("ForTranslate")]
     private const string InfinityBulletsString = "Бесконечные патроны: ";
     private const string InfinityBulletsStringEn = "Infinity Bullets: ";
     private const string InfinityBulletsStringTr = "Sonsuzluk Mermileri: ";
     private const string SecondsString = " Секунд";
     private const string SecondsStringEn = "Seconds";
     private const string SecondsStringTr = "Saniye";
    [SerializeField] private float _infinityTime = 30f;
    private float _startInfinityTime;
    public static bool InfinityBulletsActive = false;
    private float _timeForInfinityBullets = 15f;
    private const string WeaponNameString = "Оружие: ";
    private const string WeaponNameStringEn = "Weapon: ";
    private const string WeaponNameStringTr = "Silah: ";
    private const string WeaponClassString = "Класс: ";
    private const string WeaponClassStringEn = "Class: ";
    private const string WeaponClassStringTr = "Sınıf: ";

    [Header("Weapon Info")]
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private TextMeshProUGUI _weaponClass;
    private const string LevelString = "Уровень ";
    private const string LevelStringEn = "Level ";
    private const string LevelStringTr = "Seviye ";
    [SerializeField] private GameObject _pricePanel;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _needLevelFoOpen;
    

     
    [Header("Level")]
    private  const float MaxExperienceCountKoefficent = 1.4f;
    private const float UpLevelBonusCoinsKoefficent = 2f;
    [SerializeField] private Image _levelView;
    [SerializeField] private GameObject _upLevelBonusPanel;
    [SerializeField] private TextMeshProUGUI _upLevelBonusCoins;
    [SerializeField] private TextMeshProUGUI _levelText;
    public float _currentExperienceCount;
    [SerializeField] private Button _upLevelButton;
    [SerializeField] private Color _upLevelButtonColorOnActive;
    [SerializeField] private Color _upLevelButtonColorOnNotActive;




    private bool _isReadyToUpgrade = false;
    private float _levelPoints;

    [Header("Ads")]
    [SerializeField] private TextMeshProUGUI _500dollarsText;
    [SerializeField] private float _timeForAds;
    private int _moneyCountPerAds = 500;
     

    
    public void AfterLoad()
    {
      
    }

    private void OnDisable() 
    {
      _init.playerData.LastExperienceCount = _currentExperienceCount;  
    }
 
    private void Start() 
    {
      _timeForInfinityBullets = _infinityTime;
      _startInfinityTime = _infinityTime;
      _infinityButton.SetActive(true);
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       _upLevelBonusPanel.SetActive(false);
       ApplyUiElements();
       SetPricePanel();
       //_levelView.fillAmount = _currentExperienceCount;
       SetBulletsCountView(false,true);

    }

    private void Update()
    {
       if((_init.playerData.Level < _items[UiController.WeaponIndex].WeaponInfoo.LevelFoOpen) || (_init.playerData.CoinsValue < _items[WeaponIndex].WeaponInfoo.Price && _items[WeaponIndex].WeaponInfoo.BuyForRealMoney == false))
        {
          buyBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
          buyBtn.GetComponent<Button>().interactable = true;
        }
        
      if(_currentExperienceCount >= _init.playerData.MaxExperienceCount)
      {
          SetUpLevelButton(true,_upLevelButtonColorOnActive); 
          _uiAnimator.SetTrigger("ActiveUppButton"); 
      }
      else
      {
          SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
          _uiAnimator.SetTrigger("NotActiveUppButton"); 
      }
       _timeForAds += Time.deltaTime;
      TimeInfinity();
      ShowAdsInterstitial();

    }

    public void OnLeftButtonDown()
    {
        if(WeaponIndex <= 0)
        {
           WeaponIndex = _weapon.ItemsLenght - 1;
        }
        else
        {
          WeaponIndex --;
        }

       ApplyUiElements();
       SetPricePanel();

       if((_init.playerData.Level < _items[UiController.WeaponIndex].WeaponInfoo.LevelFoOpen) || (_init.playerData.CoinsValue < _items[WeaponIndex].WeaponInfoo.Price && _items[WeaponIndex].WeaponInfoo.BuyForRealMoney == false))
        {
          buyBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
          buyBtn.GetComponent<Button>().interactable = true;
        }

       initSDK.Save();
    }


    public void OnRightButtonDown()
    {
       if(WeaponIndex >= _weapon.ItemsLenght - 1)
        {
           WeaponIndex = 0;
        }
        else
        {
          WeaponIndex ++;
        }

        ApplyUiElements();
        SetPricePanel();

        if((_init.playerData.Level < _items[UiController.WeaponIndex].WeaponInfoo.LevelFoOpen) || (_init.playerData.CoinsValue < _items[WeaponIndex].WeaponInfoo.Price && _items[WeaponIndex].WeaponInfoo.BuyForRealMoney == false))
        {
          buyBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
          buyBtn.GetComponent<Button>().interactable = true;
        }

        initSDK.Save();
    }

    
    public void ApplyUiElements()
    {
       _bulletsInMagazineText.text = _items[WeaponIndex].WeaponInfoo.BulletsCount.ToString(); 
       _currentBuletsCountText.text = _items[WeaponIndex].CurrentBulletsCount.ToString() + "/"; 
       if (initSDK.language == "ru")
       {
          _weaponName.text = WeaponNameString + _items[WeaponIndex].WeaponInfoo.WeaponName.ToString();
       }
       else if (initSDK.language == "en")
       {
          _weaponName.text = WeaponNameStringEn + _items[WeaponIndex].WeaponInfoo.WeaponNameEn.ToString();
       }
       else if (initSDK.language == "tr")
       {
          _weaponName.text = WeaponNameStringTr + _items[WeaponIndex].WeaponInfoo.WeaponNameTr.ToString();
       }

       if (initSDK.language == "ru")
       {
          _weaponClass.text = WeaponClassString + _items[WeaponIndex].WeaponInfoo.WeaponClass.ToString();
       }
       else if (initSDK.language == "en")
       {
          _weaponClass.text = WeaponClassStringEn + _items[WeaponIndex].WeaponInfoo.WeaponClassEn.ToString();
       }
       else if (initSDK.language == "tr")
       {
          _weaponClass.text = WeaponClassStringTr + _items[WeaponIndex].WeaponInfoo.WeaponClassTr.ToString();
       }

       if(_items[WeaponIndex].WeaponInfoo.BuyForRealMoney == true)
       {
          if (initSDK.language == "ru")
          {
              _price.text = "За рекламу";
          }
          else if (initSDK.language == "en")
          {
            _price.text = "For AD";
          }
          else if (initSDK.language == "tr")
          {
            _price.text = "Reklam için";
          }
       }  
       else
       {
        _price.text = _items[WeaponIndex].WeaponInfoo.Price.ToString();
       }

       _coinsValueText.text = _init.playerData.CoinsValue.ToString();
       if (initSDK.language == "ru")
          {
              _levelText.text = LevelString + _init.playerData.Level.ToString();
          }
          else if (initSDK.language == "en")
          {
            _levelText.text = LevelStringEn + _init.playerData.Level.ToString();
          }
          else if (initSDK.language == "tr")
          {
            _levelText.text = LevelStringTr + _init.playerData.Level.ToString();
          }
        _levelView.fillAmount = _currentExperienceCount / _init.playerData.MaxExperienceCount;
        _upLevelBonusCoins.text = ((int)(_init.playerData.UpLevelBonusCoinsValue)).ToString();
        if (initSDK.language == "ru")
          {
               _needLevelFoOpen.text = LevelString + _items[WeaponIndex].WeaponInfoo.LevelFoOpen.ToString();
          }
          else if (initSDK.language == "en")
          {
             _needLevelFoOpen.text = LevelStringEn + _items[WeaponIndex].WeaponInfoo.LevelFoOpen.ToString();
          }
          else if (initSDK.language == "tr")
          {
             _needLevelFoOpen.text = LevelStringTr + _items[WeaponIndex].WeaponInfoo.LevelFoOpen.ToString();
          }
    }

    private void SetPricePanel()
    {
       if(_items[WeaponIndex].IsBuyed == true)
       {
         _pricePanel.SetActive(false);
         _items[WeaponIndex].Builder.Enabled= true;
         _items[WeaponIndex].Builder.SetEnable();

       }
       else
       {
         _pricePanel.SetActive(true);
         _items[WeaponIndex].Builder.Enabled = false;
          _items[WeaponIndex].Builder.SetEnable();
       }
    }

    public void BuyWeapon()
    {
        if(_init.playerData.CoinsValue >= _items[WeaponIndex].WeaponInfoo.Price && 
          _init.playerData.Level >= _items[UiController.WeaponIndex].WeaponInfoo.LevelFoOpen)
        {
          if(_items[WeaponIndex].WeaponInfoo.BuyForRealMoney == false)
          {
             SetBuyedWeapon();
          }
          else
          {
            initSDK.ShowRewardedAd("OC");
          }
        }
        initSDK.Save();
    }

    public void WeaponReward()
    {
      SetBuyedWeapon();
    }

    private void SetBuyedWeapon()
    {
       _items[WeaponIndex].IsBuyed = true;
       _items[WeaponIndex].GunTriggers.enabled = true;
       _init.playerData.CoinsValue -= _items[WeaponIndex].WeaponInfoo.Price;
       SetPricePanel();
       ApplyUiElements();
       initSDK.Save();
    }

    public void UppLevel()
    {
      if(_isReadyToUpgrade == true)
      {
       _uiAnimator.SetTrigger("NotActiveUppButton");
       _init.playerData.Level ++;
       _currentExperienceCount = 0;
       _init.playerData.MaxExperienceCount *= MaxExperienceCountKoefficent;
       _init.playerData.UpLevelBonusCoinsValue *= UpLevelBonusCoinsKoefficent;
       
       ApplyUiElements();
       OpenBonusPanel();
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       initSDK.Save();
       initSDK.Leaderboard("Level", _init.playerData.Level);

       initSDK.RateGameFunc();

      }
       
    }

    public void CloseBonusPanel()
    {
       _init.playerData.CoinsValue += (int)(_init.playerData.UpLevelBonusCoinsValue);
       ApplyUiElements();
       _upLevelBonusPanel.SetActive(false);
       initSDK.Save();
    }


    private void OpenBonusPanel()
    {
      _upLevelBonusPanel.SetActive(true);
    }

    public void GetBonusPerNewLevel()
    {
       initSDK.ShowRewardedAd("Bonus");
    }

    public void LevelBonusReward()
    {
      _init.playerData.CoinsValue += (int)(_init.playerData.UpLevelBonusCoinsValue * 3);
      ApplyUiElements();
      _upLevelBonusPanel.SetActive(false);
      initSDK.Save();
    }
     

    public  void AddExperience()
    {  
       _currentExperienceCount += _items[WeaponIndex].WeaponInfoo.ExperiencePerShot;
       _levelView.fillAmount = _currentExperienceCount / _init.playerData.MaxExperienceCount;
       SetUpLevelButton(false,_upLevelButtonColorOnNotActive);
       initSDK.Save();
    }

    public void SliderFill()
    {
        _levelView.fillAmount = _currentExperienceCount / _init.playerData.MaxExperienceCount;
    }

    private void SetUpLevelButton(bool isReady,Color color)
    {
       _isReadyToUpgrade = isReady;
       _upLevelButton.GetComponent<Image>().color = color;
    }

    public void SetInfinityBullets()
    {
       initSDK.ShowRewardedAd("Time");
    }

    public void TimeReward()
    {
      InfinityBulletsActive = true;
       StartCoroutine(StartInfinityBulletsEffect());
    }

    private void TimeInfinity()
    {
       if(InfinityBulletsActive == true && _infinityTime > 0)
       {
         _infinityButton.SetActive(false);
         _infinityTime -= Time.deltaTime;
         _infinityTimeText.enabled = true;
         if (initSDK.language == "ru")
         {
            _infinityTimeText.text = InfinityBulletsString +  Mathf.Ceil(_infinityTime).ToString() + SecondsString;
         }
         else if (initSDK.language == "en")
         {
            _infinityTimeText.text = InfinityBulletsStringEn +  Mathf.Ceil(_infinityTime).ToString() + SecondsStringEn;
         }
         else if (initSDK.language == "tr")
         {
            _infinityTimeText.text = InfinityBulletsStringTr +  Mathf.Ceil(_infinityTime).ToString() + SecondsStringTr;
         }
       }
    }

    private IEnumerator StartInfinityBulletsEffect()
    {
       SetBulletsCountView(true,false);
       yield return new WaitForSeconds(_timeForInfinityBullets);
       SetBulletsCountView(false,true);
       InfinityBulletsActive = false;
       _infinityTime = _startInfinityTime;
        _infinityTimeText.enabled = false;
       _infinityButton.SetActive(true);
    }

    private void SetBulletsCountView(bool infinityBulletsActive,bool bulletsCountPanelActive)
    {
        _infinityBulletsSign.SetActive(infinityBulletsActive);
       _bulletsCountPanel.SetActive(bulletsCountPanelActive);
    }

    public void GetMoneyPerAds()
    {
       initSDK.ShowRewardedAd("Coins");
    }

    public void GetMoneyReward(int r)
    {
      _init.playerData.CoinsValue += r;
       ApplyUiElements();
       initSDK.Save();
    }

    private void ShowAdsInterstitial()
    {
       if(_timeForAds >= 60)
       {
       		StartCoroutine(AD());
       }
    }

   IEnumerator AD()
   {
   		_timeForAds = 0;
   		adText.gameObject.SetActive(true);
   		for (int i = 5; i > 0; i--)
   		{
   			if (initSDK.language == "ru")
   				adText.text = "Реклама через " + i.ToString();
   			else if (initSDK.language == "en")
   				adText.text = "Advertising through " + i.ToString();
   			else if (initSDK.language == "tr")
   				adText.text = "Aracılığıyla reklam " + i.ToString();
   			yield return new WaitForSeconds(1);
   		}
   		adText.gameObject.SetActive(false);
        initSDK.ShowInterstitialAd();
        _timeForAds = 0;
    }
}
