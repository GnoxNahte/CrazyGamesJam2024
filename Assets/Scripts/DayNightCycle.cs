using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] Light2D sun;
    [SerializeField] Light2D moon;
    [SerializeField] Volume nightVolume;

    [SerializeField] [Range(0f, 1f)]
    float time;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve sunCurve;
    [SerializeField] AnimationCurve moonCurve;

    void UpdateTime()
    {
        transform.rotation = Quaternion.AngleAxis(360 * time, Vector3.back);

        sun.intensity = sunCurve.Evaluate(time);

        float nightWeight = moonCurve.Evaluate(time);
        moon.intensity = moonCurve.Evaluate(time);
        nightVolume.weight = nightWeight;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time + Time.deltaTime / duration) % 1;

        UpdateTime();
    }

    private void OnValidate()
    {
        UpdateTime();
    }
}
