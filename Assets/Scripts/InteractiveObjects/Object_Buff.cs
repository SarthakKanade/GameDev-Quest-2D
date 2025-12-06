using UnityEngine;
using System.Collections;

[System.Serializable]
public class Buff
{
    public Stat_Type type;
    public float value;
}


public class Object_Buff : MonoBehaviour
{

    private SpriteRenderer sr;
    private Entity_Stats statsToModify;

    [Header("Buff Details")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private bool canBeUsed = true;
    
    [Header("Floaty Movement Details")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = 0.2f;
    private Vector3 startingPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startingPosition + new Vector3(0, offset, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed)
        {
            return;
        }

        statsToModify = collision.GetComponent<Entity_Stats>();
        StartCoroutine(UseBuffCo(buffDuration));
    }

    private IEnumerator UseBuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;

        ApplyBuff(true);

        yield return new WaitForSeconds(duration);

        ApplyBuff(false);

        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach (var buff in buffs)
        {
            if (apply)
            {
                statsToModify.GetStatByType(buff.type).AddModifier(buff.value, buffName);
            }
            else
            {
                statsToModify.GetStatByType(buff.type).RemoveModifier(buffName);
            }
        }
    }

}
