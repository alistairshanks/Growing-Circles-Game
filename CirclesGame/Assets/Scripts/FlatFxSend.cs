using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatFxSend : MonoBehaviour
{
    [Header("References")]

    [SerializeField] CircleScaleDetector circleScaleDetector;

    private float innerRingScale;
    public float InnerRingScale
    {
        get
        {
            return innerRingScale;
        }

        set
        {
            innerRingScale = value;
        }
    }

    private float outerRingScale;

    public float OuterRingScale
    {
        get
        {
            return outerRingScale;
        }

        set
        {
            outerRingScale = value;
        }
    }

    private void Awake()
    {
        circleScaleDetector.InnerRingHit += OnInnerRingHit;
        circleScaleDetector.OuterRingHit += OnOuterRingHit;
        
    }

    private void OnInnerRingHit()
    {
        FlatFxManager.instance.AddEffectInnerRing(this.transform.position, innerRingScale, innerRingScale +2);
          
    }

    private void OnOuterRingHit()
    {
        FlatFxManager.instance.AddEffectOuterRing(this.transform.position, outerRingScale, outerRingScale + 2);

    }

    //unsubscribe from events
    private void OnDisable()
    {
        circleScaleDetector.InnerRingHit -= OnInnerRingHit;
        circleScaleDetector.OuterRingHit -= OnOuterRingHit;
    }
    private void OnDestroy()
    {
        circleScaleDetector.InnerRingHit -= OnInnerRingHit;
        circleScaleDetector.OuterRingHit -= OnOuterRingHit;
    }

}
