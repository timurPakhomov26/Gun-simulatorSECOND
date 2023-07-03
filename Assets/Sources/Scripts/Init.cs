using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class PlayerData 
{
    public int CoinsValue = 0;
    public int Level = 1;
    public float MaxExperienceCount = 300;
    public float UpLevelBonusCoinsValue = 50;
    public float LastExperienceCount =0;
    public bool[] items = new bool[10];
    public bool otherVKBtn = false;
    public bool tutorial;

    public float exp;
}


public enum Platform 
{
    Editor,
    Yandex, 
    VK,
    GameArter
}

public class Init : MonoBehaviour
{
	[SerializeField] private GameObject tutorialObj;

    public Platform platform;
    [SerializeField] private GameObject gameArterPrefab;

    [Header("Mobile")]
    public bool mobile;
    public GameObject leaderboardBtn;

    [Header("Game Scripts")]
    [SerializeField] private Item[] _items;
    public GameObject otherGameBtn;
 
    [Header("Rewarded")]
    [SerializeField] private UiController uiController;
    string rewardTag;

    [Header("Purchase")]
    string purchasedTag;
    private bool adOpen;

    [Header("Localization")]
    public string language;
    [SerializeField] private TextMeshProUGUI upLevelText;
    [SerializeField] private TextMeshProUGUI _500coinsText;
    [SerializeField] private TextMeshProUGUI alwaysMagazineText;
    [SerializeField] private TextMeshProUGUI otherGamesText;

    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private TextMeshProUGUI claimX3Text;
    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private TextMeshProUGUI tutText1;
	[SerializeField] private TextMeshProUGUI tutText2;
	[SerializeField] private TextMeshProUGUI tutText3;
	[SerializeField] private TextMeshProUGUI tutText4;
	[SerializeField] private TextMeshProUGUI tutText5;
	[SerializeField] private TextMeshProUGUI tutText6;

	[SerializeField] private TextMeshProUGUI tutTitleText;
    [SerializeField] private TextMeshProUGUI continueTutText;

    [SerializeField] private TextMeshProUGUI saveText;


    [Header("Save")]
    public PlayerData playerData;
    bool wasLoad;

    [Header("Links")]
    string developerNameYandex = "GeeKid%20-%20школа%20программирования";
    [SerializeField] private string colorDebug;


    public void ItIsMobile()
    {
        mobile = true;
        leaderboardBtn.SetActive(true);
    }

    private void Awake()
    {
        if (platform != Platform.GameArter)
        {
            Destroy(gameArterPrefab);
        }
        else
        {
            gameArterPrefab.SetActive(true);
        }

        switch (platform)
        {
            case Platform.Editor:
                StartCoroutine(BannerVK());
                language = "ru";
                Localization();
                break;
            case Platform.Yandex:
                language = Utils.GetLang();
                if (wasLoad)
                    Utils.LoadExtern();
                Localization();
                ShowInterstitialAd();
                break;
            case Platform.VK:
                language = "ru";
                Localization();
                StartCoroutine(BannerVK());
                StartCoroutine(RewardLoad());
                StartCoroutine(InterLoad());
                if (wasLoad)
                    Utils.VK_Load();
                break;
        }
    }

    //РЕКЛАМА//
    IEnumerator RewardLoad()
    {
    	yield return new WaitForSeconds(15);
    	switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>REWARD LOAD</color>");
                break;
            case Platform.VK:
                Utils.VK_AdRewardCheck();
                break;
        }
    }

    IEnumerator InterLoad()
    {
    	while (true)
    	{	
    		yield return new WaitForSeconds(15);
	    	switch (platform)
	        {
	            case Platform.Editor:
	                Debug.Log($"<color={colorDebug}>INTERSTITIAL LOAD</color>");
	                break;
	            case Platform.VK:
	                Utils.VK_AdInterCheck();
	                break;
	        }
    	}
    }


    IEnumerator BannerVK()
    {
    	yield return new WaitForSeconds(5);
    	ShowBannerAd();
    }

    public void ShowInterstitialAd()
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>INTERSTITIAL SHOW</color>");
                break;
            case Platform.Yandex:
                Utils.AdInterstitial();
                break;
            case Platform.VK:
                Utils.VK_Interstitial();
                break;
        }
    }

    public void ShowRewardedAd(string idOrTag)
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>REWARD SHOW</color>");
                rewardTag = idOrTag;
                OnRewarded();
                break;
            case Platform.Yandex:
                rewardTag = idOrTag;
                Utils.AdReward();
                break;
            case Platform.VK:
                rewardTag = idOrTag;
                Utils.VK_Rewarded();
                break;
        }
    }

    public void ShowBannerAd()
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>BANNER SHOW</color>");
                break;
            case Platform.VK:
                Utils.VK_Banner();
                break;
        }
    }

    public void OnRewarded()
    {
        if (rewardTag == "Coins")
        {
            uiController.GetMoneyReward(500);
        }
        else if (rewardTag == "Time")
        {
            uiController.TimeReward();
        }
        else if (rewardTag == "Bonus")
        {
            uiController.LevelBonusReward();
        }
        else if (rewardTag == "OC")
        {
            uiController.WeaponReward();
        }
        else if (rewardTag == "Group")
        {
            playerData.otherVKBtn = true;
            uiController.GetMoneyReward(5000);
            otherGameBtn.SetActive(false);
            Save();
        }
        Debug.Log($"<color=yellow>REWARD:</color> {rewardTag}");
        StartCoroutine(RewardLoad());
    }
    //РЕКЛАМА//


    //ПАУЗА И ПРОДОЛЖЕНИЕ//
    public void StopMusAndGame()
    {
        adOpen = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    public void ResumeMusAndGame()
    {
        adOpen = false;
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }
    //ПАУЗА И ПРОДОЛЖЕНИЕ//



    //ЛОКАЛИЗАЦИЯ//
    public void Localization()
    {
        if (language == "ru")
        {
            saveText.text = "Сохранить";
            upLevelText.text = "+уровень";
            _500coinsText.text = "500 долларов";
            alwaysMagazineText.text = "бесконечные патроны";
            if (platform != Platform.VK)
            	otherGamesText.text = "другие игры";
            else
            	otherGamesText.text = "+5000$\n Вступить в группу";
            claimX3Text.text = "Получить Х3";
            continueText.text = "Продолжить";
            bonusText.text = "Бонус";

            tutText1.text = "Приветствуем тебя, Стрелок! Перед тем, как отточить свое мастерство стрельбы, ты должен знать следующее:";
            tutText2.text = "Для того, чтобы начать стрелять, тебе необходимо вставить патроны в оружие. Для этого перенеси их мышкой к нему.";
            tutText3.text = "Далее нужно дослать патрон в патронник. Для этого необходимо передернуть затвор, наведя на него курсор мыши и кликнув по нему ЛКМ.";
            tutText4.text = "Для стрельбы наведи курсор мыши на спусковой крючок и нажми ЛКМ.";
            tutText5.text = "За каждый выстрел ты получаешь очки опыта и деньги. Уровень можно повысить, только после того, как шкала опыта будет заполнена.";
            tutText6.text = "Для смены оружия, кликай по стрелкам.";
            tutTitleText.text = "Обучение";
            continueTutText.text = "Далее";
        }
        if (language == "en")
        {
            saveText.text = "Save";
            upLevelText.text = "+level";
            _500coinsText.text = "500 dollars";
            alwaysMagazineText.text = "Infinite Ammo";
            if (platform != Platform.VK)
            	otherGamesText.text = "other games";
            else
            	otherGamesText.text = "+5000$\n Join a group";

            claimX3Text.text = "Claim Х3";
            continueText.text = "Continue";
            bonusText.text = "Bonus";

            tutText1.text = "Hi, Shooter! Before you hone your archery skills, you should know the following:";
            tutText2.text = "In order to start shooting, you need to insert ammo into the weapon. To do this, move it with the mouse to it.";
            tutText3.text = "Next, you need to send a cartridge into the chamber. To do this, move the bolt by hovering over it with the mouse cursor and clicking on it.";
            tutText4.text = "To shoot, move your mouse cursor over the trigger and press LMB.";
            tutText5.text = "For every shot you shoot, you get experience points and money. You can level up only after the experience bar is full.";
            tutText6.text = "To change weapons, click on the arrows.";

            tutTitleText.text = "Tutorial";
            continueTutText.text = "Сontinue";
        }
        if (language == "tr")
        {
            saveText.text = "Kaydetmek";
            upLevelText.text = "+seviye";
            _500coinsText.text = "500 dolar";
            alwaysMagazineText.text = "Sonsuz Cephane";
            if (platform != Platform.VK)
            	otherGamesText.text = "diğer oyunlar";
            else
            	otherGamesText.text = "+5000$\n Bir gruba katılın";

            claimX3Text.text = "Х3'ü talep et";
            continueText.text = "Devam";
            bonusText.text = "Bonus";

            tutText1.text = "Merhaba Shooter! Okçuluk becerilerinizi geliştirmeden önce aşağıdakileri bilmelisiniz:";
            tutText2.text = "Ateş etmeye başlamak için silaha cephane yerleştirmeniz gerekir. Bunu yapmak için fare ile mermiyi üzerine getirin.";
            tutText3.text = "Ardından, fişek yatağına bir fişek göndermeniz gerekiyor. Bunun için, fare imlecini üzerine getirerek ve üzerine tıklayarak sürgüyü hareket ettirin.";
            tutText4.text = "Ateş etmek için fare imlecinizi tetiğin üzerine getirin ve LMB'ye basın.";
            tutText5.text = "Attığınız her atış için deneyim puanı ve para kazanırsınız. Ancak deneyim çubuğu dolduktan sonra seviye atlayabilirsiniz.";
            tutText6.text = "Silahları değiştirmek için oklara tıklayın.";

            tutTitleText.text = "Öğretici";
            continueTutText.text = "Devam etmek";
        }
    }
    //ЛОКАЛИЗАЦИЯ//



    //КНОПКА ДРУГИЕ ИГРЫ//
    public void OpenOtherGames()
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>OPEN OTHER GAMES</color>");
                break;
            case Platform.Yandex:
                var domain = Utils.GetDomain();
                Application.OpenURL($"https://yandex.{domain}/games/developer?name=" + developerNameYandex);
                break;
            case Platform.VK:
            	rewardTag = "Group";
                Utils.VK_ToGroup();
                break;
        }
    }
    //КНОПКА ДРУГИЕ ИГРЫ//



    //ОТЗЫВЫ//
    public void RateGameFunc()
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>REWIEV GAME</color>");
                break;
            case Platform.Yandex:
                Utils.RateGame();
                break;
            case Platform.VK:
                Debug.Log($"<color={colorDebug}>RATE GAME VK</color>");
                break;
        }
    }
    //ОТЗЫВЫ//



    //СОХРАНЕНИЕ И ЗАГРУЗКА//
    public void Save()
    {
        for (int i = 0; i < 10; i++)
        {
            playerData.items[i] = _items[i].IsBuyed;
        }
        playerData.exp = uiController._currentExperienceCount;
        Debug.Log(playerData.exp);

        string jsonString = "";

        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>SAVE</color>");
                break;
            case Platform.Yandex:
                if (wasLoad)
                {   
                    //_saveData = PlayerDataContainer.Instance.Data;
                    jsonString = JsonUtility.ToJson(playerData);
                    Utils.SaveExtern(jsonString);
                    Debug.Log("Save");
                }
                break;
            case Platform.VK:
                jsonString = JsonUtility.ToJson(playerData);
                Utils.VK_Save(jsonString);
                break;
        }
    }

    public void SetPlayerData(string value)
    {
        playerData = JsonUtility.FromJson<PlayerData>(value);
        for (int i = 0; i < 10; i++)
        {
            _items[i].IsBuyed = playerData.items[i];
        }
        //PlayerData._data = _saveData;
        wasLoad = true;
        uiController.AfterLoad();
        if (playerData.otherVKBtn)
        {
            otherGameBtn.SetActive(false);
        }
        if (playerData.tutorial)
        {
        	tutorialObj.SetActive(false);
        }
        uiController._currentExperienceCount = playerData.exp;
        uiController.SliderFill();
    }

    void OnDestroy()
    {
        Save();
    }
    //СОХРАНЕНИЕ И ЗАГРУЗКА//



    //ВНУТРИИГРОВЫЕ ПОКУПКИ
    public void RealBuyItem(string idOrTag) //открыть окно покупки
    {
        switch (platform)
        {
            case Platform.Editor:
                purchasedTag = idOrTag;
                OnPurchasedItem();
                Debug.Log($"<color={colorDebug}>PURCHASE: </color> {purchasedTag}");
                break;
            case Platform.Yandex:
                purchasedTag = idOrTag;
                Utils.BuyItem(idOrTag);
                break;
            case Platform.VK:
                purchasedTag = idOrTag;
                Debug.Log($"<color={colorDebug}>PURCHASE VK</color>");
                break;
        }
    }

    public void CheckBuysOnStart(string idOrTag) //проверить покупки на старте
    {
        Utils.CheckBuyItem(idOrTag);
    }

    private void OnPurchasedItem() //начислить покупку (при удачной оплате)
    {
        if (purchasedTag == "purchasedID")
        {
            
        }
    }

    public void SetPurchasedItem() //начислить уже купленные предметы на старте
    {
        if (purchasedTag == "purchasedID")
        {
            
        }
    }
    //ВНУТРИИГРОВЫЕ ПОКУПКИ



    //ЛИДЕРБОРД
    public void Leaderboard(string leaderboardName, int value)
    {
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>SET LEADERBOARD:</color> {value}");
                break;
            case Platform.Yandex:
                Utils.SetToLeaderboard(value, leaderboardName);
                break;
            case Platform.VK:
                if (mobile)
                    Utils.VK_OpenLeaderboard(value);
                break;
        }
    }

    public void LeaderboardBtn(int value)
    {
    	value = playerData.Level;
        switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>SET LEADERBOARD:</color> {value}");
                break;
            case Platform.Yandex:
                break;
            case Platform.VK:
                Utils.VK_OpenLeaderboard(value);
                break;
        }
    }
    //ЛИДЕРБОРД


    //ЗВУК И ПАУЗА ПРИ СВОРАЧИВАНИИ
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.volume = silence ? 0 : 1;
        Time.timeScale = silence ? 0 : 1;

        if (adOpen)
        {
            Time.timeScale = 0;
            AudioListener.volume = 0;
        }
    }
    //ЗВУК И ПАУЗА ПРИ СВОРАЧИВАНИИ


    //СБРОС СОХРАНЕНИЙ
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerData = new PlayerData();
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Leaderboard();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    //СБРОС СОХРАНЕНИЙ

    //СОЦИАЛЬНЫЕ ФУНКЦИИ VK//
    public void ToStarGame()
    {
    	switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>GAME TO STAR</color>");
                break;
            case Platform.Yandex:
                break;
            case Platform.VK:
                Utils.VK_Star();
                break;
        }
    }

    public void ShareGame()
    {
    	switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>SHARE</color>");
                break;
            case Platform.Yandex:
                break;
            case Platform.VK:
                Utils.VK_Share();
                break;
        }
    }

    public void InvitePlayers()
    {
    	switch (platform)
        {
            case Platform.Editor:
                Debug.Log($"<color={colorDebug}>INVITE</color>");
                break;
            case Platform.Yandex:
                break;
            case Platform.VK:
                Utils.VK_Invite();
                break;
        }
    }
    //СОЦИАЛЬНЫЕ ФУНКЦИИ VK//


    public void TutorialClose()
    {
    	playerData.tutorial = true;
    	Save();
    }
}



