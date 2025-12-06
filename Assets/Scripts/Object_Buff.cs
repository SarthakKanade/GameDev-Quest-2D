using UnityEngine;
using System.Collections;

public class Object_Buff : MonoBehaviour
{

    private SpriteRenderer sr;

    [Header("Buff Details")]
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

        StartCoroutine(UseBuffCo());
    }

    private IEnumerator UseBuffCo()
    {
        canBeUsed = false;
        sr.color = Color.clear;

        yield return new WaitForSeconds(buffDuration);

        Destroy(gameObject);
    }

}
