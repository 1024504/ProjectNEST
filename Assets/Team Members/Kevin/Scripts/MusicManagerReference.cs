using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class MusicManagerReference : MonoBehaviour
{
    //reference to desire timeline
    public PlayableDirector playableDirector;
    
    //reference to Music Manager
    public MusicManagerScript musicManager;
    
    //string name of signal emitter
    public string signalTrackName;
    
    public void OnEnable()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    //to ensure that the game manager has been loaded into game a hack delay
    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        musicManager = GameManager.Instance.playerController.GetComponentInChildren<MusicManagerScript>();
        foreach (var playableAssetOutput in playableDirector.playableAsset.outputs)
        {
            if (playableAssetOutput.streamName == signalTrackName)
            {
                playableDirector.SetGenericBinding(playableAssetOutput.sourceObject ,musicManager);
                break;
            }
        }
    }
}
