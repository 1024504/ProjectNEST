using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class FalconFindQueenCutScene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    //public Player player;
    public Animator animatorToBind;
    //public Animator playerAnimatorToBind;
    public string trackName;
    //public string trackName1;
    public void OnEnable()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        /*player = GameManager.Instance.playerController.GetComponent<Player>();
        playerAnimatorToBind = player.GetComponentInParent<Animator>();
      
            /*if (playableAssetOutput.streamName == trackName1)
            {
                playableDirector.SetGenericBinding(playableAssetOutput.sourceObject ,playerAnimatorToBind);
            }*/
        animatorToBind = GameManager.Instance.playerController.GetComponentInChildren<Animator>();
        foreach (var playableAssetOutput in playableDirector.playableAsset.outputs)
        {
            if (playableAssetOutput.streamName == trackName)
            {
                playableDirector.SetGenericBinding(playableAssetOutput.sourceObject ,animatorToBind);
                break;
            }
        }
    }
}
