using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class GraphicsSetting : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown;

    [Header("Volume")]
    public GameObject Volumes;
    private VolumeActive volumeActive;

    void Start()
    {
        volumeActive = Volumes.GetComponent<VolumeActive>();
        dropdown.value = QualitySettings.GetQualityLevel();
        switch (dropdown.value)
        {
            case 0:
                volumeActive.LowGraph();
                break;
            case 1:
                volumeActive.MedGraph();
                break;
            case 2:
                volumeActive.HighGraph();
                break;
            case 3:
                volumeActive.FanGraph();
                break;
            default:
                break;
        }
    }

    public void ChangeLevel(int value)
    {
        value = dropdown.value;
        switch (value) 
        {
            case 0:
                volumeActive.LowGraph();
                break;
            case 1:
                volumeActive.MedGraph();
                break;
            case 2:
                volumeActive.HighGraph();
                break;
            case 3:
                volumeActive.FanGraph();
                break;
            default:
                break;
        }

        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }
}
