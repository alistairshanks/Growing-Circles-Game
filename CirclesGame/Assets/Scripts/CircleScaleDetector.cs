using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleScaleDetector : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject innerCircle;

    private float  innerRingScale;
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

    private bool hasHitInnerRing = false;
    private bool hasHitOuterRing = false;

    public event Action InnerRingHit;
    public event Action OuterRingHit;
    public event Action<GameObject> ObjectDestroyed;


    private void Update()
    {
        //check size of inner circle and if target scales are reached then send event and change bool so event is only sent once

        if (innerCircle != null)
        {
            if (innerCircle.transform.localScale.x >= innerRingScale && !hasHitInnerRing)
            {
                InnerRingHit?.Invoke();

                hasHitInnerRing = true;
            }

            if (innerCircle.transform.localScale.x >= outerRingScale && !hasHitOuterRing)
            {
                OuterRingHit?.Invoke();

                hasHitOuterRing = true;
            }


        }
    }
    private void OnDestroy()
    {
        ObjectDestroyed?.Invoke(this.gameObject);
    }

}