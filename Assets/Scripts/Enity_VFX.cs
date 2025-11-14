using UnityEngine;
using System.Collections;

public class Enity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageVFXMaterial;
    [SerializeField] private float onDamageVFXDuration = .15f;
    private Material defaultMaterial;
    private Coroutine onDamageVFXCo;

    [Header("Knockback VFX")]
    [SerializeField] private Vector2 knockbackPower;
    [SerializeField] private float knockbackDuration = .15f;
    private Coroutine knockbackVFXCo;

    protected void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
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
