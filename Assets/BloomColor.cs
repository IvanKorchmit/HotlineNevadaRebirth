using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomColor : MonoBehaviour
{
    PostProcessVolume postProcessVolume;
    void Start()
    {
        Bloom bloom = postProcessVolume.profile.GetSetting<Bloom>();
        var colorParameter = new ColorParameter();
        colorParameter.value = Color.white;     //Or insert any other color
        bloom.color.Override(colorParameter);
    }
}
