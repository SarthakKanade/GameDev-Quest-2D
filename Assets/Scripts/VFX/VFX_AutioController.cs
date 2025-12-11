using UnityEngine;
using System.Collections;

public class VFX_AutoController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool autoDestroy;
    [SerializeField] private float destroyDealy = 1f;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Fade Effect")]
    [SerializeField] private bool canFade;
    [SerializeField] private float fadeSpeed = 1f;

    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0f;
    [SerializeField] private float maxRotation = 360f;

    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [Space]
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        if (canFade)
        {
            StartCoroutine(FadeEffectCo());
        }

        ApplyRandomOffset();
        ApplyRandomRotation();
     
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDealy);
        }
    }

    private IEnumerator FadeEffectCo()
    {
        Color targetColor = Color.white;

        while (targetColor.a > 0)
        {
            targetColor.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = targetColor;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
        {
            return;
        }

        float randomX = Random.Range(xMinOffset, xMaxOffset);
        float randomY = Random.Range(yMinOffset, yMaxOffset);

        transform.position += new Vector3(randomX, randomY, 0);
    }

    private void ApplyRandomRotation()
    {
        if (randomRotation == false)
        {
            return;
        }

        float zRotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0, 0, zRotation);
    }
}
