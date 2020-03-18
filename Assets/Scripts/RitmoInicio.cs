using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;


public class RitmoInicio : MonoBehaviour {

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;
    private Animator myAnimation;


    void Start()
    {

        myAnimation = GetComponent<Animator>();
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/INTRO");
        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }

    private void Update()
    {
        
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instance, IntPtr parameterPtr)
    {
        switch (type)
        {
            case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                {
                    var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));

                    myAnimation.SetTrigger("Beat");
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                {
                    var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                    //Debug.Log ("marks:" + parameter.name + " " + parameter.position );
                }
                break;
        }
        return FMOD.RESULT.OK;
    }

void IntroRitmo()
    {
        
        

        
        musicInstance.start();
    }

    void IntroPuerta()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/PUERTA ABRIENDOSE");

    }

}
