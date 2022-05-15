using UnityEngine;

public class ShakeableTransform : MonoBehaviour
{
    [SerializeField]
    float frequency = 1;
    [SerializeField]
    Vector3 maximumAngularShake = Vector3.one * 2;

    float seed;

    [SerializeField]
    float recoverySpeed = 1.5f;
    [SerializeField]
    float traumaExponent = 2f;
    float trauma = 0f;

    float shake;
    void Awake()
    {
        seed = Random.value;
    }

    private void Update()
    {
        shake = Mathf.Pow(trauma, traumaExponent);
        ShakePosition();
        ShakeRotation();
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }

    public void InduceStress(float stress)
    {
        trauma = Mathf.Clamp01(trauma + stress);
    }

    void ShakePosition()
    {
        transform.localPosition = new Vector3(
            Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1,
            Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1,
            Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1) * shake;
    }

    void ShakeRotation()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(
            maximumAngularShake.x * (Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1),
            maximumAngularShake.y * (Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1),
            maximumAngularShake.z * (Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1)
            ) * shake);
    }
}