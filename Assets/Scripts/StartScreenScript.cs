using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	public Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic
	public float fadeSpeed = 0.8f;		// the fading speed
	
	private int drawDepth = -1000;		// the texture's order in the draw hierarchy: a low number means it renders on top
	private float alpha = 1.0f;			// the texture's alpha value between 0 and 1
	private int fadeDir = -1;			// the direction to fade: in = -1 or out = 1
	GameObject tapScreen;
	private int touchCounter = 0;
	public static Sprite[] runeSprite = new Sprite[8];
	public static bool started = false;

	void OnGUI()
	{
		fadeOutTexture = Texture2D.whiteTexture;
		// fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
		alpha = Mathf.Clamp01(alpha);
		
		// set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;																// make the black texture render on top (drawn last)
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		// draw the texture to fit the entire screen area
		if (!started) {
			if (alpha > 0.01) {
				Event.current.clickCount = 0;
				touchCounter = Input.touchCount;
			} else {
				Time.timeScale = 0;
			}
			if (Input.touchCount > touchCounter || Event.current.clickCount > 0) {
				Time.timeScale = 1;
				//HIDE TAP-TO-START SCREEN
				if (Application.loadedLevel == 1)
					tapScreen.SetActive(false);
				started = true;
			}
		}
	}
	
	// sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadeSpeed);
	}
	
	// OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
	void OnLevelWasLoaded()
	{
		// alpha = 1;		// use this if the alpha is not set to 1 by default
		BeginFade(-1);		// call the fade in function
	}// Use this for initialization

	void OnApplicationFocus()
	{
		if (started) {
			tapScreen.SetActive(true);
			started = false;
		} 
	}

	void togglePauseScreen() {
		if (started) {
			tapScreen.SetActive(true);
			started = false;
		} else {
			Time.timeScale = 1;
			//HIDE TAP-TO-START SCREEN
			started = true;
			if (Application.loadedLevel == 1)
				tapScreen.SetActive(false);
		}
	}
	// Use this for initialization
	void Start () {
		tapScreen = GameObject.FindGameObjectWithTag("TapToStart");
		started = false;
		
		if (Application.loadedLevel == 1) {
			for (int i = 0; i < 8; i++) {
				runeSprite[i] = (GameObject.FindGameObjectWithTag ("RuneType"+i).GetComponent<SpriteRenderer> ().sprite);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)){
			togglePauseScreen ();
		}
	}

	void startNewGame() {
		Application.LoadLevel (1); //Loads scene for gameplay
	}
}
