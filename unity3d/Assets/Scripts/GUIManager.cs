﻿using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public static GUIManager instance;

	private static GUITexture powerImg;
	private static GUITexture crosshairImg;
	private static GUITexture matchImg;

	private float hintsTimer = 0.0f;
	private static GUIText hintsText;

	private int score;
	private static int hiScore;

	private int bullet;

	private Player player;

	private GUIText hpText;
	private GUIText bulletText;

	private GUIText hiScoreText;
	private GUIText scoreText;

	// Use this for initialization
	void Start () {
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		GUITexture[] hudGUIs = GetComponentsInChildren<GUITexture>();
		powerImg = hudGUIs[0];
		crosshairImg = hudGUIs[1];
		matchImg = hudGUIs[2];

		GUIText[] guiText = this.GetComponentsInChildren<GUIText>();
		hintsText = guiText[0];
		hpText = guiText[1];
		bulletText = guiText[2];
		hiScoreText = guiText[3];
		scoreText = guiText[4];
	}
	
	// Update is called once per frame
	void Update () {
		CheckAndDisableHintsText();
	}

	private void CheckAndDisableHintsText(){
		if(hintsText.enabled){
			hintsTimer += Time.deltaTime;
			if(hintsTimer >= 4){
				hintsText.enabled = false;
				hintsTimer = 0.0f;
			}
		}
	}

	public static void CheckAndActivatePowerImg(){
		if(!powerImg.enabled){
			powerImg.enabled = true;
		}
	}

	public static void CheackAndDestoryPowerImg(){
		if (powerImg != null && powerImg.enabled) {
			Destroy(powerImg);
			//应该在收集完毕能源后门上的灯直接变绿，而不是等等碰撞的时候才变绿
			//TriggerZone.ChangeDoorLight(Color.green);
		}
	}

	public static void ChangePowerImg(Texture2D texture){
		//chargeHudGUI.texture = hudCharge[charge];
		//print(powerImg);
		//下面不能写成powerImg.material.mainTexture
		powerImg.texture = texture;
	}

	public static void ShowHints(string hints){
		hintsText.text = hints;
		if(!hintsText.enabled){
			hintsText.enabled = true;
		}
	}

	public static void EnableCrosshairImg(){
		crosshairImg.enabled = true;
	}

	public static void DisableCrosshairImg(){
		crosshairImg.enabled = false;
	}

	public static void EnableMatchImg(){
		matchImg.enabled = true;
	}

	public static void DestoryMatchImg(){
		Destroy(matchImg);
	}

	void OnGUI(){
		if(player.IsDeath()){
			GUI.skin.label.alignment = TextAnchor.MiddleCenter;
			GUI.skin.label.fontSize = 40;
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Game Over");

			GUI.skin.label.fontSize = 30;
			if(GUI.Button(new Rect(Screen.width*0.5f - 150, Screen.height*0.75f, 300, 40), "Try again")){
				UnityEngine.SceneManagement.SceneManager.LoadScene("demo");
			}
		}
	}

	public void AddScore(int added){
		score += added;
		scoreText.text = "得分: " + score;

		if(score > hiScore){
			hiScore = score;
			hiScoreText.text = "最高分: " + hiScore;
		}
	}

	public void SubBullet(int subed){
		bullet -= subed;
		if(bullet <= 0){
			bullet = 100 - bullet;
		}
		bulletText.text = bullet + " / 100";
	}

	public void SetHp(int hp){
		hpText.text = hp.ToString();
	}

}
