using UnityEngine;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
	private Dictionary<string, Action<GameManager.BeatTypes>> eventDictionary;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init();
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, Action<GameManager.BeatTypes>>();
		}
	}

	public static void StartListening(string eventName, Action<GameManager.BeatTypes> listener)
	{

		Action<GameManager.BeatTypes> thisEvent;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent += listener;
		}
		else
		{
			thisEvent += listener;
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, Action<GameManager.BeatTypes> listener)
	{
		if (eventManager == null) return;
		Action<GameManager.BeatTypes> thisEvent;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent -= listener;
		}
	}

	public static void TriggerEvent(string eventName, GameManager.BeatTypes type)
	{
		Action<GameManager.BeatTypes> thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(type);
		}
	}
}
