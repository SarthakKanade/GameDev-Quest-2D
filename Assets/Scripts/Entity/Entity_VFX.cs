using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageVFXMaterial;
    [SerializeField] private float onDamageVFXDuration = .15f;
    private Material defaultMaterial;
    private Coroutine onDamageVFXCo;

    [Header("On Giving Damage VFX")]
    [SerializeField] private Color onHitVFXColor = Color.white;
    [SerializeField] private GameObject onHitVFX;
    [SerializeField] private GameObject onCritHitVFX;
    
    [Header("Element VFX")]
    [SerializeField] private Color chillVFX = Color.cyan;
    [SerializeField] private Color burnVFX = Color.red;
    [SerializeField] private Color electrifyVFX = Color.yellow;
    private Color originalOnHitVFXColor;
    
    protected void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
        originalOnHitVFXColor = onHitVFXColor;
    }

    public void CreateOnHitVFX(Transform target, bool isCritical)
    {
        GameObject hitPrefab = isCritical ? onCritHitVFX : onHitVFX;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = onHitVFXColor;

        if (entity.facingDirection == -1 && isCritical)
        {
            vfx.transform.Rotate(0, 180, 0);
        }
    }

    public void UpdateOnHitVFXColor(ElementType element)
    {
        if (element == ElementType.Ice)
        {
            onHitVFXColor = chillVFX;
        }

        if (element == ElementType.Fire)
        {
            onHitVFXColor = burnVFX;
        }
        
        if (element == ElementType.None)
        {
            onHitVFXColor = originalOnHitVFXColor;
        }
    }

    public void PlayOnDamageVFX()
    {
        if (onDamageVFXCo != null)
        {
            StopCoroutine(onDamageVFXCo);
        }

        onDamageVFXCo = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        sr.material = onDamageVFXMaterial;
        yield return new WaitForSeconds(onDamageVFXDuration);
        sr.material = defaultMaterial;
    }

    public void PlayOnStatusEffectVFX(float duration, ElementType element)
    {
        if (element == ElementType.Ice)
        {
            StartCoroutine(StatusEffectCo(duration, chillVFX));
        }

        if (element == ElementType.Fire)
        {
            StartCoroutine(StatusEffectCo(duration, burnVFX));
        }
        
        if (element == ElementType.Lightning)
        {
            StartCoroutine(StatusEffectCo(duration, electrifyVFX));
        }
    }
    
    public void StopAllVFX()
    {
        StopAllCoroutines();
        sr.color = Color.white;
        sr.material = defaultMaterial;
    }

    private IEnumerator StatusEffectCo(float duration, Color effectVFXColor)
    {
        Color lightColor = effectVFXColor * 1.2f;
        Color darkColor = effectVFXColor * 0.8f;

        float tickInterval = 0.25f;
        float timeHasPassed = 0f;

        bool effectColorToggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = effectColorToggle ? lightColor : darkColor;
            effectColorToggle = !effectColorToggle;

            yield return new WaitForSeconds(tickInterval);
            timeHasPassed = timeHasPassed + tickInterval;
        }

        sr.color = Color.white;
    }
}
