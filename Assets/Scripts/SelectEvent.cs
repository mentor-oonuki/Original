﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectEvent : MonoBehaviour {

	public void Onclick_Kohaku() 
	{
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 0;
		SceneManager.LoadScene("Kohaku");
	}
	public void Onclick_Yuko() 
	{
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 1;
		SceneManager.LoadScene("Yuko");
	}
	public void Onclick_Misaki() 
	{
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 2;
		SceneManager.LoadScene("Misaki");
	}
}
