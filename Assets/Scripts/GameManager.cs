using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject zagal;
    public GameObject player;

    public enum GarbageTypes
    {
        Plastic,
        Paper,
        Glass,
        Organic
    }

	public GameObject[] garbage = new GameObject[4];

    private Queue<GarbageTypes> flyingGarbage = new Queue<GarbageTypes>();

    private GarbageTypes playerTrashCan;

    public float playerTimeMargin = 0.1f;

    private float playerPushTime = 0;

	float failThreshold = 0.05f;

    public enum BeatTypes{
		Normal,
		ZagalLaunch,
		Catch,
		Fail,
		FailGreen,
		FailYellow,
		FailGrey,
		FailBlue,
		CatchBlue,
		CatchYellow,
		CatchGrey,
		CatchGreen
	}

	bool succeed = false;

    public BeatTypes[] cicle = new BeatTypes[4];

    public int startBeatOffset = 8;
    public int currentBeat;

	float BeatDuration = 0;
	float lastBeatTime = 0;
	FMOD.Studio.EVENT_CALLBACK beatCallback;
	FMOD.Studio.EventInstance musicInstance;

	private GameObject currentgarbage;

	void Start()
	{
        EventManager.StartListening("OnBeatZagal", zagal.GetComponent<ZagalBehaviour>().Beat);
        EventManager.StartListening("OnBeatPlayer", player.GetComponent<PlayerBehaviour>().Beat);

        currentBeat = 0 - startBeatOffset;

		BeatDuration = Time.time;

		beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

		musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/MUSIC");

		musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
		musicInstance.start();
	}

    private void Update()
    {
        /*switch(Input.anyKeyDown)
        {
            case "1";
        }*/


        if(Input.anyKeyDown)
        {
			//Debug.Log ("CUALQUIERBOTON");
			var succeed = false;
			playerPushTime = Time.time;

			float beatProgression = playerPushTime - lastBeatTime;
			//Debug.Log ("playerPushTime" + playerPushTime);
			//Debug.Log ("lastBeatTime" + lastBeatTime);
			//Debug.Log ("beatprogresion --> " + beatProgression);
			//Debug.Log ("(BeatDuration - beatProgression) --> " + (BeatDuration - beatProgression));
			if (beatProgression < failThreshold || (BeatDuration - beatProgression) < failThreshold) {
				succeed = true;Debug.Log ("biennnn-------");
			} else {
				succeed = false;Debug.Log ("mallll-------");
			}

			//if (beatProgression >= failThreshold || (BeatDuration - beatProgression) >= failThreshold)


			if (Input.GetButtonDown ("Button1")) {
				
				playerTrashCan = GarbageTypes.Paper;
				if (succeed) {
					//EventManager.TriggerEvent ("OnBeatZagal", BeatTypes.Catch);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.CatchBlue);
				} else {
					//EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Fail);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.FailBlue);
				}
					
			} else if (Input.GetButtonDown ("Button2")) {
				playerTrashCan = GarbageTypes.Plastic;
				if (succeed) {
					//EventManager.TriggerEvent ("OnBeatZagal", BeatTypes.Catch);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.CatchYellow);
				} else {
					//EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Fail);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.FailYellow);
				}
			} else if (Input.GetButtonDown ("Button3")) {
				playerTrashCan = GarbageTypes.Organic;
				if (succeed) {
					//EventManager.TriggerEvent ("OnBeatZagal", BeatTypes.Catch);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.CatchGrey);
				} else {
					//EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Fail);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.FailGrey);
				}
			} else if (Input.GetButtonDown ("Button4")) {
				playerTrashCan = GarbageTypes.Glass;
				if (succeed) {
					//EventManager.TriggerEvent ("OnBeatZagal", BeatTypes.Catch);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.CatchGreen);
				} else {
					//EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Fail);
					EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.FailGreen);
				}
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
			else if (Input.GetKeyDown (KeyCode.Return)) {
				musicInstance.start();
				}
	        }

        /*if(Input.GetButtonUp("Button1") || 
            Input.GetButtonUp("Button2") || 
            Input.GetButtonUp("Button3") || 
            Input.GetButtonUp("Button4") )
        {
            playerTrashCan = null;
        }*/
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




				BeatDuration = Time.time - lastBeatTime;
				lastBeatTime = Time.time;
				//Debug.Log ("Time " + BeatDuration);

                currentBeat++;
                if(currentBeat > 3)
                {
                    currentBeat = 0;
                }

                if(currentBeat >= 0)
                {
                    switch(cicle[currentBeat])
                    {
                        case BeatTypes.Normal:
                            EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Normal);
							EventManager.TriggerEvent("OnBeatPlayer", BeatTypes.Normal);
                            break;

					case BeatTypes.ZagalLaunch:
						EventManager.TriggerEvent ("OnBeatZagal", BeatTypes.ZagalLaunch);
						EventManager.TriggerEvent ("OnBeatPlayer", BeatTypes.ZagalLaunch);

						int garbageType = UnityEngine.Random.Range (0, 4);
						flyingGarbage.Enqueue ((GarbageTypes)garbageType);
						currentgarbage = Instantiate (garbage [garbageType], new Vector3 (4.9f, 7.1f, 0f), Quaternion.identity);
						currentgarbage.GetComponent<Animator> ().speed = 1*(1/(BeatDuration*2));

                            break;

                        case BeatTypes.Catch:
                            
                            if (flyingGarbage.Dequeue() == playerTrashCan && playerTimeMargin > (Time.time - playerPushTime))
                            {
								EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Catch);
								//EventManager.TriggerEvent("OnBeatPlayer", BeatTypes.Catch);
                            }
                            else
                            {
								if (!succeed) {
									EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Fail);
									EventManager.TriggerEvent("OnBeatPlayer", BeatTypes.Fail);
								}
								
                            }
                            
                            break;

                        default:
							EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Normal);
							EventManager.TriggerEvent("OnBeatPlayer", BeatTypes.Normal);
                            break;
                    }

                }
                else
                {
                    EventManager.TriggerEvent("OnBeatZagal", BeatTypes.Normal);
					EventManager.TriggerEvent("OnBeatPlayer", BeatTypes.Normal);
                }


                
			}
			break;
		case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
			{
				var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
				//Debug.Log ("marks:" + parameter.name + " " + parameter.position );
			}
			break;
		}
		succeed = false;
		return FMOD.RESULT.OK;
	}
}
