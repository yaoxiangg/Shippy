using System;
using UnityEngine;
using TouchScript.Gestures;
using Random = UnityEngine.Random;

public class TapBreaker : MonoBehaviour
{
	public float Power = 10.0f;
	private bool selected = false;
	public static int cubeSelected = 0;

	public static Color[] cubeColorType =
	{
		Color.red,		//Weapon1 Index0
		Color.blue,		//Weapon2 Index1
		Color.green,	//Weapon3 Index2
		Color.yellow,	//RepairCube Index3
		Color.magenta,	//Shield1 Index4
		Color.cyan,		//Shield2 Index5
		Color.white,	//RechargeRateBoost Index6
		Color.black		//USELESSCUBE Index7
	};
	
	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		GetComponent<TapGesture>().Tapped -= tappedHandler;
	}
	
	private void tappedHandler(object sender, EventArgs e)
	{
		Color highlightColor;
		hitCount.AddHit();
		if (!selected) {
			if (cubeSelected >= 3)
				return;
		}
		selected = !selected;
		Behaviour halo = (Behaviour)GetComponent("Halo");
		if (selected) {
			halo.enabled = true; // false
			cubeSelected += 1;
		} else {
			halo.enabled = false; // false
			cubeSelected -= 1;
		}
	}
}