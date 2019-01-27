using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class UI_PopUp : UI_Modal
{
	public enum Response { Ok, No };
	public delegate void ResponseAction(Response response);
	event ResponseAction OnResponse;
	[SerializeField]
	TextMeshProUGUI txtTitle;
	[SerializeField]
	TextMeshProUGUI txtMessage;
	[SerializeField]
	TextMeshProUGUI txtAcceptMessage;
	[SerializeField]
	TextMeshProUGUI txtCancelMessage;
	public List<RebuildLayout> rebuilders = new List<RebuildLayout>();

	[SerializeField]
	protected List<GameObject> buttons = new List<GameObject>();
	// Use this for initialization
	public override void Start()
	{
		isPopUp = true;
		isPermanent = false;
		base.Start();
	}

	public virtual void SetPopUp(string title, string message, string okButtonMessage, ResponseAction callback=null)
	{
		txtTitle.text = title;
		txtMessage.text = message;
		txtAcceptMessage.text = okButtonMessage;
		if(callback!=null)
		OnResponse += callback;
		HideButtons();
		buttons[0].SetActive(true);
		RebuildLayouts();
	}

	public virtual void SetPopUp(string title, string message, string okButtonMessage, string noButtonMessage, ResponseAction callback=null)
	{
		txtTitle.text = title;
		txtMessage.text = message;
		txtAcceptMessage.text = okButtonMessage;
		txtCancelMessage.text = noButtonMessage;
		if(callback!=null)
		OnResponse += callback;
		HideButtons();
		buttons[0].SetActive(true);
		buttons[1].SetActive(true);
		RebuildLayouts();
	}

	void RebuildLayouts()
	{
		for (int i = 0; i < rebuilders.Count; i++)
			rebuilders[i].ForceRebuild();
	}

	void HideButtons()
	{
		for (int i = 0; i < buttons.Count; i++)
			buttons[i].SetActive(false);
	}

	public void OkResponse()
	{
		CallResponse(Response.Ok);
	}
	public void NoResponse()
	{
		CallResponse(Response.No);
	}
	void CallResponse(Response response)
	{
		UI_Controller.Instance.CloseWindow(this);
		if (OnResponse != null)
		{
			OnResponse(response);
			OnResponse = null;
		}
	}
}
#if UNITY_EDITOR
[CustomEditor(typeof(UI_PopUp))]
public class PopUpEditor:Editor{
	UI_PopUp popUp;
	private void OnEnable()
	{
		popUp = target as UI_PopUp;
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EditorGUI.BeginChangeCheck();
		List<RebuildLayout> newRebuilders = new List<RebuildLayout>();
		if(GUILayout.Button("Load Rebuilders"))
		{
			RebuildLayout[] rebuilders = popUp.transform.GetComponentsInChildren<RebuildLayout>(true);
			for (int i = 0; i < rebuilders.Length; i++)
				newRebuilders.Add(rebuilders[i]);
		}
		if(EditorGUI.EndChangeCheck()){
			if(GUI.changed){
				Undo.RecordObject(target, "Changed rebuilders");
				EditorUtility.SetDirty(target);
				popUp.rebuilders.Clear();
				for (int i = 0; i < newRebuilders.Count; i++)
					popUp.rebuilders.Add(newRebuilders[i]);
			}
		}
	}
}
#endif