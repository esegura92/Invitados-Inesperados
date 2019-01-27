using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Modal : UI_Element {   
	public delegate void WindowStateChanged();
	public event WindowStateChanged OnWindowClosed;
	public event WindowStateChanged OnWindowOpened;
	[SerializeField]
	bool closeWhenOpenOtherModal;
    public bool CloseWhenOpenOtherModal
	{
		get{
			return closeWhenOpenOtherModal;
		}
	}
	public bool IsPermanent{
		get{
			return isPermanent;
		}
	}
	public bool IsHideable{
		get{
			return isHideable;
		}
	}
	public bool IsPopUp{
		get{
			return isPopUp;
		}
	}
	public bool BlockRaycast{
		get{
			return blockRaycast;
		}
	}
	public bool ForceClose{
		get{
			return forceClose;
		}
	}
	public bool ForceOpen{
		get{
			return forceOpen;
		}
	}
	[SerializeField]
	protected bool blockRaycast = true;
	[SerializeField]
	protected bool isPermanent;
	[SerializeField]
	protected bool isHideable;
	[SerializeField]
	protected bool isPopUp;
	[SerializeField]
	protected bool forceOpen;
	[SerializeField]
	protected bool forceClose;
	public virtual void Start()
	{
		if (isPermanent)
			UI_Controller.Instance.OpenWindow(this);
		if (longestTweener)
			longestTweener.OnTweenChanged += (UI_Tweener.TweenState state, bool isFadeIn) =>
			{
				if (!isFadeIn)
					if (state == UI_Tweener.TweenState.finishing)
						gameObject.SetActive(false);
			};
	}
	public virtual bool CanBeOpened(){
		return !IsFading || forceOpen;
	}
	public virtual bool OpenWindow(){
		bool canBeOpened = CanBeOpened();
		if (canBeOpened)
		{
			Show();
			if (OnWindowOpened != null)
				OnWindowOpened();
		}
		return canBeOpened;
	}
	public virtual bool CloseWindow(){
		bool canBeClosed = (!IsFading && !isPermanent)||forceClose;
		if (canBeClosed)
		{
			Hide();
			if (OnWindowClosed != null)
				OnWindowClosed();
			if (!longestTweener)
				gameObject.SetActive(false);
		}
		return canBeClosed;
	}
}
