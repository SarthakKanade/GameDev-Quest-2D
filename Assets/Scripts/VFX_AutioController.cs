
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy;
    [SerializeField] private float destroyDealy = 1f;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Random Rotation")]
    [SerializeField] private float xMinOffset = -0.3f;
    [SerializeField] private float xMaxOffset = 0.3f;
    [Space]
    [SerializeField] private float yMinOffset = -0.3f;
    [SerializeField] private float yMaxOffset = 0.3f;



    private void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();
     
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDealy);
        }
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

        float zRotation = Random.Range(0f, 360f);
        transform.Rotate(0, 0, zRotation);
    }
}
