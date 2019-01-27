using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NavigationButton : MonoBehaviour {
	UI_Controller controller;
	// Use this for initialization
	void Start () {
		controller = UI_Controller.Instance;
	}
	
	public void OpenWindow(UI_Modal window){
		controller.OpenWindow(window);
		
	}
	public void CloseWindow(UI_Modal window){
		controller.CloseWindow(window);
	}
	public void CloseLastWindow()
	{
		controller.CloseLastOpenedWindow();
	}
}
