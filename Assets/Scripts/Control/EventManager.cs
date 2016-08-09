﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    Dictionary<EventType, UnityEvent> eventDictionary;

    static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType<EventManager>();
                if (!eventManager)
                {
                    GameObject go = new GameObject();
                    go.name = "EventManager";
                    go.AddComponent<EventManager>();
                    eventManager = FindObjectOfType<EventManager>();
                    Debug.Log("EventManger script created on a GameObject in your scene.");
                }
                eventManager.Init();
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventType, UnityEvent>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void StartListening(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventType, thisEvent);
        }
    }

    public static void StopListening(EventType eventType, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EventType eventType)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public enum EventType
    {
        OnBulletKill, OnLevelChanged, OnGameOver, OnScoreChanged, OnMultiplierChanged, OnUpgradesShown, OnEnemyKilled, OnEnemyHealing,
        OnSpawnPaused, OnSpawnResumed, OnUpgradesHidden, OnParticleClock, OnPlayerHurt, OnPlayerHealed, OnMusicMuted, OnMusicUnmuted,
        OnGamePaused, OnGameResumed
    }

}