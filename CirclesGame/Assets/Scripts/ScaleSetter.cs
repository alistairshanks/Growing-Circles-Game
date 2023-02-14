using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSetter : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject outerRing;
    [SerializeField] private GameObject innerRing;
    [SerializeField] private CircleScaleDetector scaleDetector;
    [SerializeField] private FlatFxSend flatFxSend;
    [SerializeField] private CircleGrow circleGrow;

    private float outerRandomScale;
    private float innerRandomScale;

    private Vector3 totalRandomScaleOuterRing;
    private Vector3 totalRandomScaleInnerRing;

    private void Awake()
    {
        GenerateRandomScales();

        totalRandomScaleOuterRing.x = totalRandomScaleOuterRing.y = outerRandomScale;
        totalRandomScaleInnerRing.x = totalRandomScaleInnerRing.y = innerRandomScale;

        outerRing.transform.localScale = totalRandomScaleOuterRing;
        innerRing.transform.localScale = totalRandomScaleInnerRing;

        //send values to innerCircle for detecting when to destroy


        //send values to scale detector for detecting scores
        scaleDetector.OuterRingScale = outerRandomScale;
        scaleDetector.InnerRingScale = innerRandomScale;

        //send values to FlatFxSend for detecting size of effects

        flatFxSend.OuterRingScale = outerRandomScale;
        flatFxSend.InnerRingScale = innerRandomScale;

        circleGrow.MaxScale = totalRandomScaleOuterRing;

    }

    private void GenerateRandomScales()
    {
        outerRandomScale = Random.Range(1f, 3f);
        innerRandomScale = Random.Range((outerRandomScale - 0.75f), (outerRandomScale - 0.25f));

       /* Debug.Log(outerRandomScale);
        Debug.Log(innerRandomScale);
       */
    }
}
