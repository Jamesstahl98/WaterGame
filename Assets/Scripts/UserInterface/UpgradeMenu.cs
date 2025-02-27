using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI engineTierText;
    [SerializeField] private TMPro.TextMeshProUGUI maxSpeedText;
    [SerializeField] private TMPro.TextMeshProUGUI maxThrustText;

    [SerializeField] private TMPro.TextMeshProUGUI fishingLineTierText;
    [SerializeField] private TMPro.TextMeshProUGUI maxDepthText;

    [SerializeField] private TMPro.TextMeshProUGUI fishingHookTierText;
    [SerializeField] private TMPro.TextMeshProUGUI maxFishCountText;
    void Start()
    {
        PlayerStats.UpgradeHandlerDelegate += UpdateUI;
        UpdateUI();
        gameObject.SetActive(false);
    }
    private void UpdateUI()
    {
        engineTierText.text = PlayerStats.EngineTier.ToString();
        maxSpeedText.text = $"Max speed: {PlayerStats.MaxSpeed.ToString()}";
        maxThrustText.text = $"Max thrust: {PlayerStats.MaxThrust.ToString()}";

        fishingLineTierText.text = PlayerStats.FishingLineTier.ToString();
        maxDepthText.text = $"Max fishing depth: {PlayerStats.FishingDepth.ToString()} meters";

        fishingHookTierText.text = PlayerStats.FishingHookTier.ToString();
        maxFishCountText.text = $"Max hooked fishes: {PlayerStats.FishCount.ToString()}";
    }
}
