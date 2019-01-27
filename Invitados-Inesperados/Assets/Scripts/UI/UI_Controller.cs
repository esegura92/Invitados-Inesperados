using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
	public static UI_Controller Instance;
	[SerializeField]
	List<UI_Modal> openedWindows = new List<UI_Modal>();
	[SerializeField]
	Transform mainUITransform;

	[SerializeField]
	GameObject raycastBlocker;
	UI_Tweener raycastBlockerTweener;
	UI_Modal lastClosedWindow;
	CanvasGroup raycastBlockerGroup;
	bool isBlocking;
	private void Awake()
	{
		Instance = this;
		raycastBlockerTweener = raycastBlocker.GetComponent<UI_Tweener>();
		raycastBlockerGroup = raycastBlocker.GetComponent<CanvasGroup>();
		BlockRaycast(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UI_Modal actualWindow = GetActualWindow();
			if (actualWindow)
			{
				CloseWindow(actualWindow);
			}
		}
	}

	public void OpenWindow(UI_Modal window)
	{
		if (!openedWindows.Contains(window))
		{
			if (window.CanBeOpened())
			{
				UI_Modal actualWindow = GetActualWindow();
				if (window.IsFading && !window.ForceOpen)
					return;
				if (!window.IsPopUp)
				{
					if (actualWindow)
					{
						if (actualWindow.IsFading && !actualWindow.ForceClose)
							return;
						if (actualWindow.CloseWhenOpenOtherModal)
						{
							CloseLastOpenedWindow();
						}
						else if (actualWindow.IsHideable)
						{
							actualWindow.Hide();
						}
					}
				}
				openedWindows.Insert(0, window);
				window.gameObject.SetActive(true);
				window.OpenWindow();
				window.transform.SetAsLastSibling();
				ResetRaycastBlocker();
			}
		}
	}
	public void CloseWindow(UI_Modal window)
	{
		if (openedWindows.Contains(window))
		{
			bool closedSuccesfully = window.CloseWindow();
			if (closedSuccesfully)
			{
				lastClosedWindow = window;
				openedWindows.Remove(window);
				UI_Modal actualWindow = GetActualWindow();
				if (actualWindow)
				{
					if (actualWindow.IsHideable)
						actualWindow.Show();
				}
				ResetRaycastBlocker();
			}
		}
	}
	public void CloseLastOpenedWindow()
	{
		if (openedWindows.Count > 0)
		{
			CloseWindow(GetActualWindow());
		}
	}

	UI_Modal GetActualWindow()
	{
		UI_Modal actualWindow = null;
		if (openedWindows.Count > 0)
			actualWindow = openedWindows[0];
		return actualWindow;
	}

	public void BlockRaycast(bool block)
	{
		if (block)
		{
			raycastBlocker.SetActive(true);
			if (!isBlocking)
				raycastBlockerTweener.FadeInDefault();
			isBlocking = true;
			raycastBlockerGroup.blocksRaycasts = true;
		}
		else
		{
			if (isBlocking)
				raycastBlockerTweener.FadeOutDefault();
			isBlocking = false;
			raycastBlockerGroup.blocksRaycasts = false;
		}
	}
	public void ResetRaycastBlocker()
	{
		UI_Modal actualWindow = GetActualWindow();
		BlockRaycast(actualWindow.BlockRaycast);
	}
	#region Default PopUp
#endregion
}
