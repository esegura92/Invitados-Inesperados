using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class RebuildLayout : MonoBehaviour
{
	[SerializeField]
	RectTransform rect;
	// Use this for initialization
	void Start()
	{
		ForceRebuild();
	}
	public void ForceRebuild(){
		if (!rect)
			rect = GetComponent<RectTransform>();
		if(rect)
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
	}
	private void OnEnable()
	{
		ForceRebuild();
	}
}
#if UNITY_EDITOR
[CustomEditor(typeof(RebuildLayout),false)]
public class RebuildLayoutEditor:Editor{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUILayout.Button("Rebuild layout")){
			((RebuildLayout)target).ForceRebuild();
		}
	}
}
#endif