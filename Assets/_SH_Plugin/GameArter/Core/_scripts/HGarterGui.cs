using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Text;
using System.Linq;

[HideInInspector]
public class HGarterGui
{
	private string openedBox = null;

	public void HideGift(){
		// predelat - mazat az po prihlaseni, na skutecna data...
		try{
			GameObject.Find ("GameArter_Gift").SetActive(false);
			Debug.LogWarning("Gift Success");
		} catch{
			Debug.LogWarning("Gift Fault");
		}
	}
		
	/// <summary>
	/// Ilustrates the browser box. Replaces box generated in browser for informating message
	/// Dedicated to login, ads, leaderbord, badge, shop, share
	/// </summary>
	public void IlustrateBrowserBox(string type, string info){
		// layer
		if (GameObject.Find ("GameArter_BrowserBox") == null) {
			openedBox = type;
			GameObject editorLayer = new GameObject ("GameArter_BrowserBox");
			Canvas canvas = editorLayer.AddComponent<Canvas> ();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = 999;
			editorLayer.AddComponent<CanvasScaler> ();
			editorLayer.AddComponent<GraphicRaycaster> ();

			// centered box
			GameObject browserBox = new GameObject ("GA_Box");
			browserBox.transform.SetParent(editorLayer.transform);
			RectTransform loginBoxPosition = browserBox.AddComponent<RectTransform>();
			loginBoxPosition.sizeDelta = new Vector2 (523, 188);
			//loginBoxPosition.transform.localPosition = new Vector2 (0, 0);
			loginBoxPosition.anchoredPosition = new Vector2 (0, 0);
			Image panel = browserBox.AddComponent<Image>();
			panel.color = Color.black;

			// text
			GameObject boxInfo = new GameObject ("GA_LoginInfo");
			boxInfo.transform.SetParent(browserBox.transform);
			Text loginText = boxInfo.AddComponent<Text> ();
			loginText.text = info;
			loginText.color = Color.white;
			loginText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
			loginText.alignment = TextAnchor.MiddleCenter;
			RectTransform logintInfoRT = boxInfo.GetComponent<RectTransform> ();
			//logintInfoRT.transform.localPosition = new Vector2 (0, 37f);
			logintInfoRT.anchoredPosition = new Vector2 (0, 37f);
			logintInfoRT.sizeDelta = new Vector2(461, 88);
			logintInfoRT.localScale = new Vector2 (1, 1);

			// close button
			GameObject closeBtnObj = new GameObject ("GA_LogIn");
			closeBtnObj.transform.SetParent (editorLayer.transform);
			Button closeBtn = closeBtnObj.AddComponent<Button> ();
			Text loginBtnText = closeBtnObj.AddComponent<Text> ();
			loginBtnText.text = "Close";
			loginBtnText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
			loginBtnText.alignment = TextAnchor.MiddleCenter;
			closeBtn.targetGraphic = loginBtnText;
			RectTransform loginBtnRT = closeBtnObj.GetComponent<RectTransform> ();
			//loginBtnRT.transform.localPosition = new Vector2 (-3f, -22f);
			loginBtnRT.anchoredPosition = new Vector2 (-3f, -22f);
			loginBtnRT.sizeDelta = new Vector2 (160, 30);
			loginBtnRT.transform.localScale = new Vector2 (1, 1);
			loginBtnRT.localScale = new Vector2 (1, 1);

			closeBtn.transition = Selectable.Transition.ColorTint;
			ColorBlock btnc = closeBtn.colors;
			btnc.highlightedColor = new Color32 (249, 201, 0, 255);
			closeBtn.colors = btnc;
			closeBtn.onClick.AddListener (RemoveBrowserBox);
		}
	}

	private void RemoveBrowserBox(){
		MonoBehaviour.Destroy (GameObject.Find ("GameArter_BrowserBox"));
		if (openedBox == "shop" || openedBox == "exchange") {
			Garter.I.SyncShop (null);
		} else {
			Garter.I.SdkWindowClosed (openedBox);
		}
	}

	/// <summary>
	/// Clears progress associated with the game (badges, progress, bought items).
	/// </summary>
	public void ClearGameProgress(bool editorMode){
		// layer
		if (GameObject.Find ("GameArter_BrowserBox") == null) {
			GameObject editorLayer = new GameObject ("GameArter_BrowserBox");
			Canvas canvas = editorLayer.AddComponent<Canvas> ();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.sortingOrder = 999;
			editorLayer.AddComponent<CanvasScaler> ();
			editorLayer.AddComponent<GraphicRaycaster> ();

			// centered box
			GameObject browserBox = new GameObject ("GA_Box");
			browserBox.transform.SetParent(editorLayer.transform);
			RectTransform loginBoxPosition = browserBox.AddComponent<RectTransform>();
			loginBoxPosition.sizeDelta = new Vector2 (523, 188);
			//loginBoxPosition.transform.localPosition = new Vector2 (0, 0);
			loginBoxPosition.anchoredPosition = new Vector2 (0, 0);
			Image panel = browserBox.AddComponent<Image>();
			panel.color = Color.black;

			// text
			GameObject boxInfo = new GameObject ("GA_LoginInfo");
			boxInfo.transform.SetParent(browserBox.transform);
			Text loginText = boxInfo.AddComponent<Text> ();
			string warningText = "Do you really want to clear all data associated with the game? Take a note, that clearing game data is irreversible action.";
			if (editorMode) {
				warningText += "Editor will be stopped after this action. New start from scene 0 required.";
			}
			loginText.text = warningText;
			loginText.color = Color.white;
			loginText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
			loginText.alignment = TextAnchor.MiddleCenter;
			RectTransform logintInfoRT = boxInfo.GetComponent<RectTransform> ();
			//logintInfoRT.transform.localPosition = new Vector2 (0, 37f);
			logintInfoRT.anchoredPosition = new Vector2 (0, 37f);
			logintInfoRT.sizeDelta = new Vector2(461, 88);
			logintInfoRT.localScale = new Vector2 (1, 1);

			// confirm button & refuse button
			GameObject confirmBtnObj = new GameObject ("GA_ConfirmBtn");
			GameObject refuseBtnObj = new GameObject ("GA_RefuseBtn");
			confirmBtnObj.transform.SetParent (editorLayer.transform);
			refuseBtnObj.transform.SetParent (editorLayer.transform);
			Button confirmBtn = confirmBtnObj.AddComponent<Button> ();
			Button refuseBtn = refuseBtnObj.AddComponent<Button> ();
			Text confirmBtnText = confirmBtnObj.AddComponent<Text> ();
			Text refuseBtnText = refuseBtnObj.AddComponent<Text> ();
			confirmBtnText.text = "Yes, I am sure by the action.";
			refuseBtnText.text = "No, close window without any action.";
			Font textfont = Resources.GetBuiltinResource<Font> ("Arial.ttf");
			confirmBtnText.font = textfont;
			refuseBtnText.font = textfont;
			confirmBtnText.alignment = TextAnchor.MiddleCenter;
			refuseBtnText.alignment = TextAnchor.MiddleCenter;
			confirmBtn.targetGraphic = confirmBtnText;
			refuseBtn.targetGraphic = refuseBtnText;
			RectTransform confirmBtnRT = confirmBtnObj.GetComponent<RectTransform> ();
			//confirmBtnRT.transform.localPosition = new Vector2 (-3f, -22f);
			confirmBtnRT.anchoredPosition = new Vector2 (-3f, -22f);
			confirmBtnRT.sizeDelta = new Vector2 (160, 30);
			confirmBtnRT.transform.localScale = new Vector2 (1, 1);
			confirmBtnRT.localScale = new Vector2 (1, 1);
			confirmBtn.transition = Selectable.Transition.ColorTint;

			RectTransform refuseBtnRT = refuseBtnObj.GetComponent<RectTransform> ();
			//refuseBtnRT.transform.localPosition = new Vector2 (-3f, -55f);
			refuseBtnRT.anchoredPosition = new Vector2 (-3f, -55f);
			refuseBtnRT.sizeDelta = new Vector2 (160, 30);
			refuseBtnRT.transform.localScale = new Vector2 (1, 1);
			refuseBtnRT.localScale = new Vector2 (1, 1);
			refuseBtn.transition = Selectable.Transition.ColorTint;

			ColorBlock btnc = confirmBtn.colors;
			btnc.highlightedColor = new Color32 (249, 201, 0, 255);
			confirmBtn.colors = btnc;
			refuseBtn.colors = btnc;
			confirmBtn.onClick.AddListener (ClearGameProgressReq);
			refuseBtn.onClick.AddListener (RemoveBrowserBox);
		}
	}

	private void ClearGameProgressReq(){
		RemoveBrowserBox ();
		Garter.I.ClearDataUserConfirm ();
	}
		
	public void EditorLogin(){
		// login layer
		GameObject editorLayer = new GameObject ("GameArter_EditorLogin");
		Canvas canvas = editorLayer.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 998;
		editorLayer.AddComponent<CanvasScaler> ();
		editorLayer.AddComponent<GraphicRaycaster> ();

		// login box
		GameObject loginBox = new GameObject ("GA_LoginBox");
		loginBox.transform.SetParent(editorLayer.transform);
		RectTransform loginBoxPosition = loginBox.AddComponent<RectTransform>();
		loginBoxPosition.sizeDelta = new Vector2 (523, 188);
		//loginBoxPosition.transform.localPosition = new Vector2 (0, 0);
		loginBoxPosition.anchoredPosition = new Vector2 (0, 0);
		Image panel = loginBox.AddComponent<Image>();
		panel.color = Color.black;

		//login header text
		GameObject logintInfo = new GameObject ("GA_LoginInfo");
		logintInfo.transform.SetParent(loginBox.transform);
		Text loginText = logintInfo.AddComponent<Text> ();
		loginText.text = "Editor login. Select one of following options. (In Production state, this box is automatically replaced for Website login. Functionality remains.)";
		loginText.color = Color.white;
		loginText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		loginText.alignment = TextAnchor.MiddleCenter;
		RectTransform logintInfoRT = logintInfo.GetComponent<RectTransform> ();
		//logintInfoRT.transform.localPosition = new Vector2 (0, 37f);
		logintInfoRT.anchoredPosition = new Vector2 (0, 37f);
		logintInfoRT.sizeDelta = new Vector2(461, 88);
		logintInfoRT.localScale = new Vector2 (1, 1);

		//login button
		GameObject logInBtnObj = new GameObject ("GA_LogIn");
		logInBtnObj.transform.SetParent(loginBox.transform);
		Button loginBtn = logInBtnObj.AddComponent<Button> ();
		Text loginBtnText = logInBtnObj.AddComponent<Text> ();
		loginBtnText.text = "Log in";
		loginBtnText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		loginBtnText.alignment = TextAnchor.MiddleCenter;
		loginBtn.targetGraphic = loginBtnText;
		RectTransform loginBtnRT = logInBtnObj.GetComponent<RectTransform> ();
		//loginBtnRT.transform.localPosition = new Vector2 (-3f, -22f);
		loginBtnRT.anchoredPosition = new Vector2 (-3f, -22f);
		loginBtnRT.sizeDelta = new Vector2(160, 30);
		loginBtnRT.transform.localScale = new Vector2 (1, 1);
		loginBtnRT.localScale = new Vector2 (1, 1);

		loginBtn.transition = Selectable.Transition.ColorTint;
		ColorBlock btnc = loginBtn.colors;
		btnc.highlightedColor = new Color32 (249, 201, 0, 255);
		loginBtn.colors = btnc;
		loginBtn.onClick.AddListener (PlayAsLogged);

		//play as guest btn
		GameObject guestBtnObj = new GameObject ("GA_GuestBtn");
		guestBtnObj.transform.SetParent(loginBox.transform);
		Button guestBtn = guestBtnObj.AddComponent<Button> ();
		Text guestBtnText = guestBtnObj.AddComponent<Text> ();
		guestBtnText.text = "Play as Guest";
		guestBtnText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		guestBtnText.alignment = TextAnchor.MiddleCenter;
		guestBtn.targetGraphic = guestBtnText;
		RectTransform guestBtnRT = guestBtnObj.GetComponent<RectTransform> ();
		//guestBtnRT.transform.localPosition = new Vector2 (-3f, -63f);
		guestBtnRT.anchoredPosition = new Vector2 (-3f, -63f);
		guestBtnRT.sizeDelta = new Vector2(160, 30);
		guestBtnRT.localScale = new Vector2 (1, 1);

		guestBtn.transition = Selectable.Transition.ColorTint;
		ColorBlock btnc1 = guestBtn.colors;
		btnc1.highlightedColor = new Color32 (249, 201, 0, 255);
		guestBtn.colors = btnc1;

		guestBtn.onClick.AddListener (PlayAsGuest);

		// EventSystem for possibility to select a button
		if(GameObject.Find("EventSystem") == null){
			if (Application.isEditor) {
				Debug.LogError ("GARTER | Please, add EventSystem (GameObject/UI/EventSystem) into the project");
			}
		}
	}
	/*	
	public void SetProgressBar(uint progress){
		GameObject progressBar = GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Progress");
		GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Progress/FillArea/Text").GetComponentInChildren<Text>().text = "PROGRES: "+progress+"%"; 
		progressBar.GetComponentInChildren<Slider> ().value = progress;
	}*/

	// ### USER PROFILE BOX ###
	public void UserDashboard(string nick, Texture2D userPicture, string[] ranks, string[] colors, float[] ud, float userStars, float userProgress, decimal uExp, char interpreter, bool localCurrencyA, bool progressBarVisibility){
		// var upToDate?
		// access boxes
		GameObject nickName = GameObject.Find  ("GameArter_DashBoard/GA_UserBoard/UserNick");
		GameObject userPhoto = GameObject.Find ("GameArter_DashBoard/GA_UserBoard/PersonalImage/RawImage");
		GameObject userRank = GameObject.Find  ("GameArter_DashBoard/GA_UserBoard/UserRank");
		GameObject starsRank = GameObject.Find ("GameArter_DashBoard/GA_UserBoard/StarsRank");
		GameObject uCoins = GameObject.Find    ("GameArter_DashBoard/GA_UserBoard/Resources/Cbox/Coins");
		//GameObject uDiamonds = GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Resources/Dbox/Diamonds");
		Transform[] starImgs = starsRank.GetComponentsInChildren<Transform> ();

		// set userNick
		nickName.GetComponentInChildren<Text> ().text = nick;
		// set userPhoto
		userPhoto.GetComponentInChildren<RawImage> ().texture = userPicture;

		// set userStars
		// 0 stars = unlogged
		float rankIndexH = userStars / 4;
		int rankIndex = (int)((rankIndexH < 1) ? Math.Ceiling (rankIndexH) : Math.Round (rankIndexH));

		// set userRank
		userRank.GetComponentInChildren<Text> ().text = (ranks [rankIndex] + " USER").ToUpper();

		Color color;
		ColorUtility.TryParseHtmlString (colors[rankIndex], out color);
		Sprite[] sprites = Resources.LoadAll<Sprite>(@"sdkgui");
		Sprite gui12 = null;
		Sprite gui13 = null;
		// find sprite
		foreach (var sprite in sprites) {
			if (sprite.name == "sdkgui_12") {
				gui12 = sprite;
			} else if (sprite.name == "sdkgui_13") {
				gui13 = sprite;
			}
		}
		// set and display stars
		byte stars = (byte)Math.Round(userStars % 4);
		for (byte s = 1; s < 5;s++){
			Image i = starImgs[s].GetComponentInChildren<Image>();
				i.color = color;
			if(s > stars){
				//Debug.Log(s+", "+starImgs[s].name);
				i.sprite = gui12; //fill sprite
			} else {
				i.sprite = gui13;
			}
		}
			
		// set Coins
		if (localCurrencyA) {
			uCoins.GetComponentInChildren<Text> ().text = Garter.I.LocalCurrency().ToString() +" // " + Math.Round((double)ud [0],0).ToString();
				Button exchange = uCoins.GetComponent<Button>();
				if(exchange == null){
					exchange = uCoins.AddComponent<Button> ();
				}
				exchange.transition = Selectable.Transition.ColorTint;
				exchange.targetGraphic = uCoins.GetComponent<Text>();

				exchange.transition = Selectable.Transition.ColorTint;
				ColorBlock btnc = exchange.colors;
				btnc.highlightedColor = new Color32 (249, 184, 48, 255);
				exchange.colors = btnc;

				exchange.onClick.RemoveAllListeners();
				exchange.onClick.AddListener(OpenExchange);
		} else {
			uCoins.GetComponentInChildren<Text> ().text = Math.Round((double)ud [0],0).ToString();
		}
		// set Diamonds
		//uDiamonds.GetComponentInChildren<Text> ().text = Math.Round((double)ud [1],0).ToString();	

		// set ProgressBar
		GameObject uProgress = GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Progress");
		if (uProgress != null) {
			if (progressBarVisibility) {
				if (interpreter.Equals ('F')) { //exp system is enabled
					GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Progress/FillArea/Text").GetComponentInChildren<Text> ().text = "PROG: " + userProgress + "% (" + uExp + "EXP)"; 
				} else {
					GameObject.Find ("GameArter_DashBoard/GA_UserBoard/Progress/FillArea/Text").GetComponentInChildren<Text> ().text = "PROG: " + userProgress + "%"; 
				}
				//Debug.Log ("PROGRESS: " + progress);
				uProgress.GetComponentInChildren<Slider> ().value = userProgress;
			} else {
				uProgress.SetActive (false);
			}
		}
	}

	private void OpenExchange(){
		Debug.Log ("Open exchage");
		Garter.I.OpenSdkWindow("exchange");
	}
		
	// moznost zobrazit cokoliv navic (ostatni cisla zaporna...)


	//Default - all displayed - Hide whole icon / hide notification icon only
	public void FeatureBoxModule(byte[] iconsVisibility, byte[] notifyIcons){
		//Debug.Log ("NI | "+notifyIcons[0]+","+notifyIcons[1]+","+notifyIcons[2]+","+notifyIcons[3]+","+notifyIcons[4]+","+notifyIcons[5]);
		string[] icons = new string[]{ "Leaderboard", "Badge", "Shop", "Login", "Settings", "Share" };

		for (byte i = 0; i < 6; i++) {
			//Debug.Log (i + " | " + icons [i]);
			if (iconsVisibility [i] == 0) { // hide icon
				GameObject.Find ("GameArter_Features/GA_FeatureBar/" + icons [i]).SetActive (false);
			} else {
				// hide notification
				GameObject notifyIcon = GameObject.Find("GameArter_Features/GA_FeatureBar/" + icons [i] + "/Notification");
				Image image = notifyIcon.GetComponent<Image> ();
				if (notifyIcons [i] == 0) { // hide notification
					Color c = image.color;
					c.a = 0;
					image.color = c;
				} else if(image.color.a != 192){ // display notification
					Color c = image.color;
					c.a = 192;
					image.color = c;
				}
			}
		}
	}

    public void NoInternetConnection()
    {
        GameObject notification = MonoBehaviour.Instantiate(Resources.Load("Garter_NoConnection", typeof(GameObject))) as GameObject;
    }

	public void DisplayNotification(Sprite sprite, string textVal){
		//Debug.LogWarning ("---> Notify | "+textVal);
		MonoBehaviour.Destroy(GameObject.Find ("Garter_Notification(Clone)"));
		if (textVal != "Destroy") {
			GameObject notification = MonoBehaviour.Instantiate(Resources.Load("Garter_Notification", typeof(GameObject))) as GameObject;
			Transform[] childrens = notification.GetComponentsInChildren<Transform> ();
			childrens [2].GetComponentInChildren<Text>().text = textVal;
			childrens [3].GetComponentInChildren<Image> ().sprite = sprite;
		}
	}

		
	public void SdkDebugger(string type, string message){
		Debug.LogWarning(message);
	}
		
	public bool[] GarterGuiAvailability(){
		bool userBox = (GameObject.Find ("GameArter_DashBoard") == null) ? false : true;
		bool featureBox = (GameObject.Find ("GameArter_Features") == null) ? false : true;
		bool giftBox = (GameObject.Find ("GameArter_Gift") == null) ? false : true;
		return new bool[]{ userBox, featureBox, giftBox };
	}


	private void RemoveScreenShot(){
		MonoBehaviour.Destroy (GameObject.Find ("Garter_ScreenShot"));
		Garter.I.SdkWindowClosed ();
	}
	private void ShareScreenShot(){
		Debug.Log("Share ScreenShot");
	}
	private void SendScreenShot(){
		Debug.Log("Send ScreenShot");
	}


	public void SignIn(){
		Debug.Log ("Display login");
	}
				
	public void BannedUser(){
		Debug.LogError ("BANNED USER");
	}
		
	// ### ILLEGAL VER ###
	/// <summary>
	/// Game is running ilegally
	/// </summary>
	/// <param name="host">Host.</param>
	public void BlockMessage(string host){
		GameObject protectLayer = new GameObject ("protectLayer");
		Canvas protection = protectLayer.AddComponent<Canvas> ();
		protection.renderMode = RenderMode.ScreenSpaceOverlay;
		Image prImage = protectLayer.AddComponent<Image> ();
		prImage.color = Color.black;
		GameObject Text = new GameObject ("message");
		Text prText = Text.AddComponent<Text> ();
		prText.transform.SetParent (protectLayer.transform);
		prText.color = Color.white;
		prText.text = host+" is running this game ilegally. Please, play the game at www.pacogames.com";
		prText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		prText.rectTransform.localPosition = new Vector2 (0, 0);
		prText.rectTransform.sizeDelta = new Vector2 (500, 100);
		prText.fontSize = 20;
	}

	// obe funkce se navraci do mista webglplayer feedback -> odchazi data na server
	// neprihlasen - uklada se do playerprefs - guest player nick
	// prihlasen - uklada se na server - logged player nick

	private void PlayAsLogged(){ // Return logged data - server
		Garter.I.EditorMode("logged");
	}
	private void PlayAsGuest(){ // return guest data - new user (server / playerPrefs)
		Garter.I.EditorMode("guest");
	}
	public void DestrolLoginBox(){
		MonoBehaviour.Destroy (GameObject.Find ("GameArter_EditorLogin"));
	}
		
	public void CreateLoadingScreen(string assetName, Texture assetImage, bool activeProgress){
		GameObject GameArter_LoadingScreen = MonoBehaviour.Instantiate(Resources.Load("GameArter_LoadingScreen", typeof(GameObject))) as GameObject;
		GameObject AssetImg = GameArter_LoadingScreen.transform.GetChild (0).gameObject ;
		GameObject AssetNameObj = GameArter_LoadingScreen.transform.GetChild (2).gameObject;

		// Set asset image
		if (assetImage != null) {
			AssetImg.GetComponent<RawImage> ().texture = assetImage;
		} else {
			AssetImg.SetActive(false);
		}

		// Set asset name
		Text assetNameObj = AssetNameObj.GetComponent<Text>();
		assetNameObj.text = assetName;
			
		// set progress
		if(!activeProgress){
			GameObject ProgressBar = GameArter_LoadingScreen.transform.GetChild (1).gameObject;
			ProgressBar.SetActive(false);
			GameObject Progress = GameArter_LoadingScreen.transform.GetChild (3).gameObject;
			Progress.SetActive(false);
			// adjust text elm
			Debug.Log("Adjusting...");
			RectTransform textElmPosition = assetNameObj.GetComponent<RectTransform>();
			textElmPosition.localPosition = new Vector2 (0, -90);
			textElmPosition.sizeDelta = new Vector2(591,26);
			assetNameObj.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;;
		}
	}
	public void UpdateLoadingScreen(float progress){
		GameObject GameArter_LoadingScreen = GameObject.Find ("GameArter_LoadingScreen(Clone)");
		if (GameArter_LoadingScreen != null) {
			GameArter_LoadingScreen.transform.GetChild (1).gameObject.GetComponent<Slider> ().value = progress / 100;
			GameArter_LoadingScreen.transform.GetChild (3).gameObject.GetComponent<Text> ().text = progress+"%";
		} else {
			Debug.LogWarning ("GARTER | Loading screen was not found");
		}
	}
	public void RemoveLoadingScreen(){
		GameObject LoadingScreen = GameObject.Find ("GameArter_LoadingScreen(Clone)");
		if (LoadingScreen != null) {
			MonoBehaviour.Destroy (LoadingScreen);
		} else {
			Debug.LogError ("GARTER | Object cannot be removed because was not found");
		}

	}



	/*
	// ### SCREENSHOTS ###
	public void ScreenShot(Texture2D img){
		// multiplayer game...
		Garter.I.SdkWindowOpened("screenshot");
		// cursor...?

		//get Sprites for buttons
		Sprite[] sprites = Resources.LoadAll<Sprite>(@"sdkgui");
		Sprite shareSprite = null;
		Sprite closeSprite = null;
		Sprite sendSprite = null;
		foreach (var sprite in sprites) {
			if (sprite.name == "sdkgui_21") {
				shareSprite = sprite;
			} else if (sprite.name == "sdkgui_20") {
				sendSprite = sprite;
			} else if (sprite.name == "sdkgui_18") {
				closeSprite = sprite;
			}
		}

		GameObject canvas = GameObject.Find("Canvas"); //canvas object
		if (canvas == null) {
			canvas = new GameObject ("Canvas");
			canvas.AddComponent<Canvas> ();
		}


		GameObject screenshot = new GameObject ("Garter_ScreenShot");
		screenshot.transform.SetParent(canvas.transform);
		//set position for screenshot
		RectTransform screenRT = screenshot.AddComponent<RectTransform>();
		screenRT.transform.localPosition = new Vector2 (0, 0);
		screenRT.sizeDelta = new Vector2(465, 270);
		screenRT.localScale = new Vector2 (1, 1);

		RawImage screenshotPreview = screenshot.AddComponent<RawImage> ();
		screenshotPreview.texture = img;

		GameObject share = new GameObject ("Share");
		share.transform.SetParent(screenshot.transform);
		RectTransform shareRT = share.AddComponent<RectTransform>();
		shareRT.transform.localPosition = new Vector2 (-62f, -131.8f);
		shareRT.sizeDelta = new Vector2(84, 28);
		shareRT.localScale = new Vector2 (1, 1);

		Button shareBtn = share.AddComponent<Button> ();
		Image shareImg = share.AddComponent<Image> ();
		shareImg.sprite = shareSprite;
		shareBtn.targetGraphic = shareImg;
		shareBtn.onClick.AddListener(ShareScreenShot);


		GameObject close = new GameObject ("Close");
		close.transform.SetParent(screenshot.transform);
		RectTransform closeRT = close.AddComponent<RectTransform>();
		closeRT.transform.localPosition = new Vector2 (0f, -131.8f);
		closeRT.sizeDelta = new Vector2(37, 28);
		closeRT.localScale = new Vector2 (1, 1);

		Button closeBtn = close.AddComponent<Button> ();
		Image closeImg = close.AddComponent<Image> ();
		closeImg.sprite = closeSprite;
		closeBtn.targetGraphic = closeImg;
		closeBtn.onClick.AddListener(RemoveScreenShot);


		GameObject send = new GameObject ("Send");
		send.transform.SetParent(screenshot.transform);
		RectTransform sendRT = send.AddComponent<RectTransform>();
		sendRT.transform.localPosition = new Vector2 (76f, -131.8f);
		sendRT.sizeDelta = new Vector2(112, 28);
		sendRT.localScale = new Vector2 (1, 1);

		Button sendBtn = send.AddComponent<Button> ();
		Image sendImg = send.AddComponent<Image> ();
		sendImg.sprite = sendSprite;
		sendBtn.targetGraphic = sendImg;
		sendBtn.onClick.AddListener(SendScreenShot);

	}
	*/
}