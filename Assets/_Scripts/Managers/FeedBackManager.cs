using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedBackManager : MonoBehaviour
{
    public static FeedBackManager Instance;

    public MMF_Player CarLandingFeedBack;
    public MMF_Player CarExplodeFeedBack;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
