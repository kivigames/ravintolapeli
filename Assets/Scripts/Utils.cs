using UnityEngine;

public static class Utils
{
    /// <summary>
    ///     Convert decibels into linear volume (0-1).
    /// </summary>
    /// <param name="db"></param>
    /// <returns>volume in linear scale</returns>
    public static float DecibelToLinear(float db)
    {
        return Mathf.Pow(10.0f, db / 20.0f);
    }

    /// <summary>
    ///     Convert linear volume [0, 1] into decibels. Exactly 0 volume returns -120 dB.
    /// </summary>
    /// <param name="linear"></param>
    /// <returns>volume in dB</returns>
    public static float LinearToDecibel(float linear)
    {
        if (linear == 0f)
            return -120f;

        return Mathf.Log10(linear) * 20f;
    }
}