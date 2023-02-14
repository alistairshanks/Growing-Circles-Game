using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatFxManager : MonoBehaviour
{
    //make singleton so can be easily accessed by objects just spawned
    public static FlatFxManager instance;

    Color limeSoap;
    Color ufoGreen;
    Color wildWaterMelon;
    Color waterMelon;

    [SerializeField] private FlatFX flatFX;

    private void Awake()
    {
        instance = this;

        ColorUtility.TryParseHtmlString("#7bed9f", out limeSoap);
        ColorUtility.TryParseHtmlString("#2ed573", out ufoGreen);
        ColorUtility.TryParseHtmlString("#ff6b81", out wildWaterMelon);
        ColorUtility.TryParseHtmlString("#ff4757", out waterMelon);

    }



    public void AddEffectInnerRing(Vector2 position, float startSize, float endSize)
    {
        flatFX.settings[1].start.innerColor = limeSoap;
        flatFX.settings[1].start.outerColor = ufoGreen;
        flatFX.settings[1].end.innerColor = ufoGreen;
        flatFX.settings[1].end.outerColor = limeSoap;

        flatFX.AddEffect(position, 1);

    }

    public void AddEffectOuterRing(Vector2 position, float startSize, float endSize)
    {
        flatFX.settings[1].start.innerColor = waterMelon;
        flatFX.settings[1].start.outerColor = wildWaterMelon;
        flatFX.settings[1].end.innerColor = wildWaterMelon;
        flatFX.settings[1].end.outerColor = waterMelon;

         flatFX.AddEffect(position, 1);
    }



}
