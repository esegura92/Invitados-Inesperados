using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class UI_Element : MonoBehaviour
{
	#region Properties
	public bool IsShowing { get { return isShowing; } }
	public bool IsInteractible { get { return isInteractible; } }
	public bool IsFading{ 
		get {
			bool isFading = false;
			if(longestTweener){
				isFading = longestTweener.IsFading;
			}
			return isFading;
		}
	}
	#endregion
	bool isShowing;
	bool isInteractible;
	public List<UI_Tweener> tweeners = new List<UI_Tweener>();
	public UI_Tweener tweener;
	[HideInInspector]
	public UI_Tweener longestTweener;
	// Use this for initialization
	public virtual void Awake()
	{
		float longestTweenerDuration = 0;
		if (tweener)
		{
			longestTweenerDuration = tweener.fadeDuration + tweener.delayAtStart;
			longestTweener = tweener;
		}
		foreach(UI_Tweener tweener in tweeners)
		{
			float tweenerDuration = tweener.TotalDuration;
			if (tweenerDuration > longestTweenerDuration)
			{
				longestTweenerDuration = tweenerDuration;
				longestTweener = tweener;
			}
		}
	}

	public virtual void Hide(){
		if (isShowing)
		{
			if (tweener)
				tweener.FadeOut();
			foreach (UI_Tweener tweener in tweeners)
				tweener.FadeOut();
            isShowing = false;
		}
	}

	public virtual void Show()
	{
		isShowing = true;
		if (tweener)
			tweener.FadeIn();
		foreach (UI_Tweener tweener in tweeners)
			tweener.FadeIn();
	}



}
#if UNITY_EDITOR
[CustomEditor(typeof(UI_Element), true)]
public class UI_ElementEditor : Editor
{
	UI_Element element;
	private void OnEnable()
	{
		element = (UI_Element)target;
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (!element.tweener && GUILayout.Button("Set tweener"))
		{
			element.tweener = element.gameObject.GetComponent<UI_Tweener>();
			if (!element.tweener)
				element.tweener = element.gameObject.AddComponent<UI_Tweener>();
		}
		if (element.tweener && GUILayout.Button("Remove tweener"))
		{
			DestroyImmediate(element.tweener);
			element.tweener = null;
		}
		if (GUILayout.Button("Add dependant tweeners from children"))
		{
			element.tweeners.Clear();
			UI_Tweener[] tweeners = element.gameObject.GetComponentsInChildren<UI_Tweener>();
			foreach (UI_Tweener tweener in tweeners)
				element.tweeners.Add(tweener);
		}
	}
}
#endif