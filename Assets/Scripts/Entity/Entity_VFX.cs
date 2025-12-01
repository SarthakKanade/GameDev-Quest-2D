using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageVFXMaterial;
    [SerializeField] private float onDamageVFXDuration = .15f;
    private Material defaultMaterial;
    private Coroutine onDamageVFXCo;

    [Header("On Giving Damage VFX")]
    [SerializeField] private Color onHitVFXColor = Color.white;
    [SerializeField] private GameObject onHitVFX;
    [SerializeField] private GameObject onCritHitVFX;
    

    [Header("Knockback VFX")]
    [SerializeField] private Vector2 knockbackPower;
    private Coroutine knockbackVFXCo;

    protected void Awake()
    {
        entity = GetComponent<Entity>();
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
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
}
