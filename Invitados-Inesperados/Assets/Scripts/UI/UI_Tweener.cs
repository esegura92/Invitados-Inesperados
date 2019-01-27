﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
#endif
[RequireComponent(typeof(CanvasGroup))]
public class UI_Tweener : MonoBehaviour
{
	public enum TweenState{
		starting,
        finishing
	}
	[SerializeField]
	UnityEvent OnFadeInFinished;
	[SerializeField]
	UnityEvent OnFadeOutFinished;

	public delegate void TweenEvent(TweenState state,bool isFadeIn);
	public event TweenEvent OnTweenChanged;
	bool isFading;
	public bool forceFade;
	public bool IsFading{
		get{
			return isFading;
		}
	}
	public float TotalDuration{
		get{
			return fadeDuration + delayAtStart;
		}
	}

	[SerializeField]
	CanvasGroup canvasGroup;
	[SerializeField]
	RectTransform rectTransform;
	public bool fadeAtStart;
	public bool fadeOnEnable;
	public float delayAtStart;
	public bool startWithInValues = true;
	public float fadeDuration = .4f;

	#region Alpha Options
	public bool enableAlpha;
	public float outAlpha = 0;
	public float inAlpha = 1;
	public AnimationCurve alphaCurve = new AnimationCurve();
	#endregion
	#region Scale Options
	public bool enableScale;
	public Vector3 outScale;
	public Vector3 inScale = Vector2.one;
	public AnimationCurve scaleCurve = new AnimationCurve();
	#endregion
	#region AnchoredPosition Options
	public bool enableAnchoredPosition;
	public Vector3 inAnchoredPosition;
	public Vector3 outAnchoredPosition;
	public AnimationCurve anchoredPositionCurve = new AnimationCurve();
	#endregion
	#region SizeDelta Options
	public bool enableSizeDelta;
	public Vector3 inSizeDelta;
	public Vector3 outSizeDelta;
	public AnimationCurve sizeDeltaCurve = new AnimationCurve();
	#endregion
	#region Rotation Options
	public bool enableRotation;
	public Vector3 inRotation;
	public Vector3 outRotation;
	public AnimationCurve rotationCurve = new AnimationCurve();

	#endregion
	bool fadeWasSetted;
	// Use this for initialization
	private void Awake()
	{
		LoadElements();
	}
	private void Start()
	{
		SetValues(startWithInValues);
		if (fadeAtStart)
			FadeIn(fadeDuration,delayAtStart);
	}
	private void OnEnable()
	{
		if (fadeOnEnable)
		{
			FadeIn(fadeDuration, delayAtStart);
		}
	}
	private void OnDisable()
	{
		isFading = false;
	}
	public void FadeInDefault(){
		FadeIn();
	}
	public void FadeIn(float duration){
		FadeIn(duration, delayAtStart);      
	}
	public void FadeIn(float duration=-1, float delay=-1)
	{
		if (duration == -1)
			duration = fadeDuration;
		if (delay == -1)
			delay = delayAtStart;
		if (!isFading||forceFade)
		{
			if (enableAlpha)
			{
				canvasGroup.alpha = outAlpha;
				canvasGroup.DOFade(inAlpha, duration).SetEase(alphaCurve).SetDelay(delay);
			}
			if (enableScale)
			{
				rectTransform.localScale = outScale;
				rectTransform.DOScale(inScale, duration).SetEase(scaleCurve).SetDelay(delay);
			}
			if (enableAnchoredPosition)
			{
				rectTransform.anchoredPosition = outAnchoredPosition;
				rectTransform.DOAnchorPos(inAnchoredPosition, duration).SetEase(anchoredPositionCurve).SetDelay(delay);
			}
			if (enableSizeDelta)
			{
				rectTransform.sizeDelta = outSizeDelta;
				rectTransform.DOSizeDelta(inSizeDelta, duration).SetEase(sizeDeltaCurve).SetDelay(delay);
			}
			if (enableRotation)
			{
				rectTransform.localRotation =Quaternion.Euler(outRotation);
				rectTransform.DOLocalRotate(inRotation, duration).SetEase(rotationCurve).SetDelay(delay);
			}
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(BeginTween(duration, delay, true));
			}
		}
		fadeWasSetted = true;
	}
	public void FadeOutDefault(){
		
		FadeOut();
	}
	public void FadeOut(float duration){
		FadeOut(duration, delayAtStart, true);
	}
	public void FadeOut(float duration=-1,float delay=-1,bool useOriginalDelay=false)
	{
		if (duration == -1)
			duration = fadeDuration;
		if (delay == -1)
			delay = 0;
		if (useOriginalDelay)
			delay = delayAtStart;
		if (!isFading||forceFade)
		{
			if (enableAlpha)
			{
				canvasGroup.alpha = inAlpha;
				canvasGroup.DOFade(outAlpha, duration).SetEase(alphaCurve).SetDelay(delay);
			}
			if (enableScale)
			{
				rectTransform.localScale = inScale;
				rectTransform.DOScale(outScale, duration).SetEase(scaleCurve).SetDelay(delay);
			}
			if (enableAnchoredPosition)
			{
				rectTransform.anchoredPosition = inAnchoredPosition;
				rectTransform.DOAnchorPos(outAnchoredPosition, duration).SetEase(anchoredPositionCurve).SetDelay(delay);
			}
			if (enableSizeDelta)
			{
				rectTransform.sizeDelta = inSizeDelta;
				rectTransform.DOSizeDelta(outSizeDelta, duration).SetEase(sizeDeltaCurve).SetDelay(delay);
			}
			if (enableRotation)
            {
				rectTransform.localRotation = Quaternion.Euler(inRotation);
				rectTransform.DOLocalRotate(outRotation, duration).SetEase(rotationCurve).SetDelay(delay);
            }
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(BeginTween(duration, delay, false));
			}
		}

		fadeWasSetted = true;
	}
	public void SetValues(bool isIn)
	{
		if (!fadeWasSetted)
		{
			LoadElements();
			if (enableAlpha)
			{
				if (isIn)
					canvasGroup.alpha = inAlpha;
				else
					canvasGroup.alpha = outAlpha;
			}
			if (enableScale)
			{
				if (isIn)
					rectTransform.localScale = inScale;
				else
					rectTransform.localScale = outScale;
			}
			if (enableAnchoredPosition)
			{
				if (isIn)
					rectTransform.anchoredPosition = inAnchoredPosition;
				else
					rectTransform.anchoredPosition = outAnchoredPosition;
			}
			if (enableSizeDelta)
			{
				if (isIn)
					rectTransform.sizeDelta = inSizeDelta;
				else
					rectTransform.sizeDelta = outSizeDelta;
			}
			if (enableRotation)
			{
				if (isIn)
					rectTransform.localRotation = Quaternion.Euler(inRotation);
				else
					rectTransform.localRotation = Quaternion.Euler(outRotation);
			}
		}
	}
	public void LoadElements()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		if (!canvasGroup)
			canvasGroup = gameObject.AddComponent<CanvasGroup>();

		rectTransform = GetComponent<RectTransform>();
		if (!rectTransform)
			Debug.LogWarning(gameObject.name + " doesn't have a Rect Transform");

	}
	IEnumerator BeginTween(float duration, float delay,bool isFadeIn){
		isFading = true;
		if (OnTweenChanged != null)
			OnTweenChanged.Invoke(TweenState.starting, isFadeIn);
		yield return new WaitForSeconds(duration + delay);
		isFading = false;
		if (isFadeIn)
			OnFadeInFinished.Invoke();
		else
			OnFadeOutFinished.Invoke();
        if (OnTweenChanged != null)
			OnTweenChanged.Invoke(TweenState.finishing, isFadeIn);
	}
#region HELPERS
	public void SetTweenValues(bool isIn)
	{
		LoadElements();
		SetAlphaValues(isIn);
		SetScaleValues(isIn);
		SetAnchoredPosition(isIn);
		SetSizeDelta(isIn);
	}

	public void SetAlphaValues(bool isIn){
		if (isIn)
			inAlpha = canvasGroup.alpha;
		else
			outAlpha = canvasGroup.alpha;
	}
	public void SetScaleValues(bool isIn){
		if (isIn)
			inScale = rectTransform.localScale;
		else
			outScale = rectTransform.localScale;
	}
	public void SetAnchoredPosition(bool isIn){
		if (isIn)
			inAnchoredPosition = rectTransform.anchoredPosition;
		else
			outAnchoredPosition = rectTransform.anchoredPosition;
	}
	public void SetSizeDelta(bool isIn){
		if (isIn)
			inSizeDelta = rectTransform.sizeDelta;
		else
			outSizeDelta = rectTransform.sizeDelta;
	}
	public void SetRotationValues(bool isIn){
		if (isIn)
			inRotation = rectTransform.localRotation.eulerAngles;
		else
			outSizeDelta = rectTransform.localRotation.eulerAngles;
	}
#endregion
}
#if UNITY_EDITOR
[CustomEditor(typeof(UI_Tweener)),CanEditMultipleObjects()]
public class UI_TweenerEditor:Editor{
	SerializedProperty OnFadeInFinished;
	SerializedProperty OnFadeOutFinished;

	UI_Tweener tweener;
	AnimBool showAlphaOptions;
	AnimBool showScaleOptions;
	AnimBool showAnchoredPositionOptions;
	AnimBool showSizeDeltaOptions;
	AnimBool showRotationOptions;
	private void OnEnable()
	{
		tweener = (UI_Tweener)target;
		tweener.LoadElements();
		showAlphaOptions = new AnimBool(false);
		showAlphaOptions.valueChanged.AddListener(Repaint);
		showScaleOptions = new AnimBool(false);
		showAlphaOptions.valueChanged.AddListener(Repaint);
		showAnchoredPositionOptions = new AnimBool(false);
		showAnchoredPositionOptions.valueChanged.AddListener(Repaint);
		showSizeDeltaOptions = new AnimBool(false);
		showSizeDeltaOptions.valueChanged.AddListener(Repaint);
		showRotationOptions = new AnimBool(false);
		showRotationOptions.valueChanged.AddListener(Repaint);
		OnFadeInFinished = serializedObject.FindProperty("OnFadeInFinished");
		OnFadeOutFinished = serializedObject.FindProperty("OnFadeOutFinished");
	}
	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.LabelField("General Values");
		tweener.fadeDuration = EditorGUILayout.FloatField("Tween Duration", tweener.fadeDuration);
		tweener.startWithInValues = EditorGUILayout.Toggle("Start with In values", tweener.startWithInValues);
		tweener.fadeAtStart = EditorGUILayout.Toggle("Tween at start", tweener.fadeAtStart);
		tweener.fadeOnEnable = EditorGUILayout.Toggle("Tween OnEnable", tweener.fadeOnEnable);
		tweener.forceFade = EditorGUILayout.Toggle("Force tween", tweener.forceFade);
		tweener.delayAtStart = EditorGUILayout.FloatField("Delay", tweener.delayAtStart);
		AlphaOptions();
		ScaleOptions();
		RotationOptions();
		AnchoredPositionOptions();
		SizeDeltaOptions();
		//EditorGUIUtility.LookLikeControls();
		EditorGUILayout.PropertyField(OnFadeInFinished);
		EditorGUILayout.PropertyField(OnFadeOutFinished);
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Save All FadeIn Values"))
			tweener.SetTweenValues(true);
		if (GUILayout.Button("Save All FadeOut Values"))
			tweener.SetTweenValues(false);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Preview FadeIn"))
        {
			if (Application.isPlaying)
				tweener.FadeIn();
			else
				tweener.FadeOut();
        }
		if(GUILayout.Button("Preview FadeOut")){
			if (Application.isPlaying)
				tweener.FadeOut();
			else
				tweener.FadeIn();

		}
		EditorGUILayout.EndHorizontal();
		if (EditorGUI.EndChangeCheck())
		{
			if (GUI.changed)
			{
				Undo.RecordObject(target, tweener.gameObject.name + " Tweener changed");
				EditorUtility.SetDirty(target);
				serializedObject.ApplyModifiedProperties();
			}
		}

	}

	void AlphaOptions(){
		tweener.enableAlpha = EditorGUILayout.ToggleLeft("Enable Alpha tween", tweener.enableAlpha);
        showAlphaOptions.target = tweener.enableAlpha;
        if (EditorGUILayout.BeginFadeGroup(showAlphaOptions.faded))
        {
			EditorGUI.indentLevel++;
            if (FloatField(ref tweener.inAlpha, "In alpha"))
                tweener.SetAlphaValues(true);
			if (FloatField(ref tweener.outAlpha, "Out alpha"))
				tweener.SetAlphaValues(false);
			CurveField(ref tweener.alphaCurve, "Curve");
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();
	}

	void ScaleOptions(){
		tweener.enableScale = EditorGUILayout.ToggleLeft("Enable Scale tween", tweener.enableScale);
		showScaleOptions.target = tweener.enableScale;
		if(EditorGUILayout.BeginFadeGroup(showScaleOptions.faded)){
			EditorGUI.indentLevel++;
			if (VectorField(ref tweener.inScale, "In scale"))
				tweener.SetScaleValues(true);
			if (VectorField(ref tweener.outScale, "Out scale"))
				tweener.SetScaleValues(false);
			CurveField(ref tweener.scaleCurve, "Curve");
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();
	}
	void RotationOptions(){
		tweener.enableRotation = EditorGUILayout.ToggleLeft("Enable Rotation tween", tweener.enableRotation);
		showRotationOptions.target = tweener.enableRotation;
		if(EditorGUILayout.BeginFadeGroup(showRotationOptions.faded)){
			EditorGUI.indentLevel++;
			if (VectorField(ref tweener.inRotation, "In rotation"))
				tweener.SetRotationValues(true);
			if (VectorField(ref tweener.outRotation, "Out rotation"))
				tweener.SetRotationValues(false);
			CurveField(ref tweener.rotationCurve,"Curve");
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();
	}
	void AnchoredPositionOptions(){
		tweener.enableAnchoredPosition = EditorGUILayout.ToggleLeft("Enable Anchored Position tween", tweener.enableAnchoredPosition);
		showAnchoredPositionOptions.target = tweener.enableAnchoredPosition;
		if(EditorGUILayout.BeginFadeGroup(showAnchoredPositionOptions.faded)){
			EditorGUI.indentLevel++;
			if (VectorField(ref tweener.inAnchoredPosition, "In anchored position"))
				tweener.SetAnchoredPosition(true);
			if (VectorField(ref tweener.outAnchoredPosition, "Out anchored position"))
				tweener.SetAnchoredPosition(false);
			CurveField(ref tweener.anchoredPositionCurve, "Curve");
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();
	}
	void SizeDeltaOptions(){
		tweener.enableSizeDelta = EditorGUILayout.ToggleLeft("Enable Size Delta tween", tweener.enableSizeDelta);
		showSizeDeltaOptions.target = tweener.enableSizeDelta;
		if(EditorGUILayout.BeginFadeGroup(showSizeDeltaOptions.faded)){
			EditorGUI.indentLevel++;
			if (VectorField(ref tweener.inSizeDelta, "In size delta"))
				tweener.SetSizeDelta(true);
			if (VectorField(ref tweener.outSizeDelta, "Out size delta"))
				tweener.SetSizeDelta(false);
			CurveField(ref tweener.sizeDeltaCurve, "Curve");
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.EndFadeGroup();
	}
	bool FloatField(ref float value, string fieldName)
	{
		bool wasPressed = false;
		EditorGUILayout.BeginHorizontal();
		value = EditorGUILayout.FloatField(fieldName, value);

		if (GUILayout.Button("Component Values"))
			wasPressed = true;
		EditorGUILayout.EndHorizontal();
		return wasPressed;
	}
	bool VectorField(ref Vector3 value,string fieldName){
		bool wasPressed = false;
		EditorGUILayout.BeginHorizontal();
		value = EditorGUILayout.Vector3Field(fieldName, value);
		if (GUILayout.Button("Component Values"))
			wasPressed = true;
		EditorGUILayout.EndHorizontal();
		return wasPressed;
	}
	void CurveField(ref AnimationCurve value, string fieldName){
		value = EditorGUILayout.CurveField(fieldName, value);
	}
   
}

#endif