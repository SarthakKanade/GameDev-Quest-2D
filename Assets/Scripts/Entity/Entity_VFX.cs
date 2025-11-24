using UnityEngine;
using System.Collections;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageVFXMaterial;
    [SerializeField] private float onDamageVFXDuration = .15f;
    private Material defaultMaterial;
    private Coroutine onDamageVFXCo;

    [Header("On Giving Damage VFX")]
    [SerializeField] private GameObject onHitVFX;
    [SerializeField] private Color onHitVFXColor = Color.white;
    

    [Header("Knockback VFX")]
    [SerializeField] private Vector2 knockbackPower;
    private Coroutine knockbackVFXCo;

    protected void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
    }

    public void CreateOnHitVFX(Transform target)
    {
        if (onHitVFX != null)
        {
            GameObject vfx = Instantiate(onHitVFX, target.position, Quaternion.identity);
            vfx.GetComponentInChildren<SpriteRenderer>().color = onHitVFXColor;
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
