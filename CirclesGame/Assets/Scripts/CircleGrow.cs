using System.Collections;
using UnityEngine;

public class CircleGrow : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject parentObject;

    [Header("Lerp Settings")]

    [SerializeField] private float growthSpeed = 2f;
    [SerializeField] private float duration = 1f;


    private Vector3 minScale;
    private Vector3 maxScale;
    private bool canGrow;
    private Coroutine LerpCoroutine;


    //set up property for maxScale so it can be accessed from other classes

    public Vector3 MaxScale
    {
        get
        {
            return maxScale;
        }

        set
        {
            maxScale = value;
        }
    }


    private void Start()
    {
        canGrow = true;
        //growthSpeed = Random.Range(1, 2f);

        growthSpeed = 1f;

        LerpCoroutine = StartCoroutine(OnEnableCoroutine());
    }

    // check if the circle can grow and if it can then start the coroutine -
    // once it's finished reset canGrow to false

    private IEnumerator OnEnableCoroutine()
    {
        while (canGrow)
        {
            minScale = transform.localScale;

            yield return LerpScale(minScale, maxScale, duration);
            canGrow = false;
        }
    }

    //if max size stop co-routine and destroy whole thing 
    private void Update()
    {
        if (canGrow == false)
        {
            StopCoroutine(LerpCoroutine);
            Destroy(parentObject);
        }
    }

    //lerp scale function
    private IEnumerator LerpScale(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * growthSpeed;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);

            yield return null;
        }
    }

    //if clicked then stop co-routine and destroy the whole thing by destroying parent
    private void OnMouseDown()
    {
        canGrow = false;
        StopCoroutine(LerpCoroutine);
        Destroy(parentObject);
    }

}

