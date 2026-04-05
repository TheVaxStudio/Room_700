using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textValuesScript : MonoBehaviour
{
    [Header("DON'T TOUCH")]
    [SerializeField] private mapGenerator mapGenerator;
    [SerializeField] private valuesAdjusterScript valuesAdjusterScript;
    [Header("GRASS BIOME")]
    //Grass River
    [SerializeField] private TextMeshProUGUI grassBiomeRiverExtensionAmountText;
    [SerializeField] private Slider grassBiomeRiverExtensionSlider;
    public int grassBiomeRiverExtensionAmount;

    [SerializeField] private Slider grassBiomeRiverAmountSlider;
    public int grassBiomeRiverAmount;


    //Forest
    [SerializeField] private TextMeshProUGUI grassBiomeForestExtensionAmountText;
    [SerializeField] private Slider grassBiomeForestExtensionSlider;
    public int grassBiomeForestExtensionAmount;

    [SerializeField] private Slider grassBiomeForestAmountSlider;
    public int grassBiomeForestAmount;

    //Jungle Rocks 
    [SerializeField] private TextMeshProUGUI grassBiomeJungleRocksExtensionAmountText;
    [SerializeField] private Slider grassBiomeJungleRocksExtensionSlider;
    public int grassBiomeJungleRocksExtensionAmount;

    [SerializeField] private Slider grassBiomeJungleRocksAmountSlider;
    public int grassBiomeJungleRocksAmount;

    //Flowers 
    [SerializeField] private TextMeshProUGUI grassBiomeFlowersExtensionAmountText;
    [SerializeField] private Slider grassBiomeFlowersExtensionSlider;
    public int grassBiomeFlowersExtensionAmount;

    [SerializeField] private Slider grassBiomeFlowersAmountSlider;
    public int grassBiomeFlowersAmount;


    [Header("SAND BIOME")]
    //Sand River
    [SerializeField] private TextMeshProUGUI sandBiomeRiverExtensionAmountText;
    [SerializeField] private Slider sandBiomeRiverExtensionSlider;
    public int sandBiomeRiverExtensionAmount;

    [SerializeField] private Slider sandBiomeRiverAmountSlider;
    public int sandBiomeRiverAmount;

    //Sand Rocks
    [SerializeField] private TextMeshProUGUI sandBiomeSandRocksExtensionAmountText;
    [SerializeField] private Slider sandBiomeSandRocksExtensionSlider;
    public int sandBiomeSandRocksExtensionAmount;

    [SerializeField] private Slider sandBiomeRocksAmountSlider;
    public int sandBiomeRocksAmount;

    //Palm Tree
    [SerializeField] private TextMeshProUGUI sandBiomePalmTreeExtensionAmountText;
    [SerializeField] private Slider sandBiomePalmTreeExtensionSlider;
    public int sandBiomePalmTreeExtensionAmount;

    [SerializeField] private Slider sandBiomePalmTreeAmountSlider;
    public int sandBiomePalmTreeAmount;

    //Cactus
    [SerializeField] private TextMeshProUGUI sandBiomeCactusExtensionAmountText;
    [SerializeField] private Slider sandBiomeCactusExtensionSlider;
    public int sandBiomeCactusExtensionAmount;

    [SerializeField] private Slider sandBiomeCactusAmountSlider;
    public int sandBiomeCactusAmount;

    //Biomes Amount
    [SerializeField] private TextMeshProUGUI biomesAmountText;

    [SerializeField] private GameObject forestScreen;
    [SerializeField] private GameObject desertScreen;

    [SerializeField] private GameObject forestIconSelected;
    [SerializeField] private GameObject desertIconSelected;

    [SerializeField] private GameObject forestScreen2;
    [SerializeField] private GameObject desertScreen2;

    [SerializeField] private GameObject forestIconSelected2;
    [SerializeField] private GameObject desertIconSelected2;

    //Others
    [SerializeField] TextMeshProUGUI grassWaterAmountText;
    [SerializeField] TextMeshProUGUI grassForestAmountText;
    [SerializeField] TextMeshProUGUI grassRocksAmountText;
    [SerializeField] TextMeshProUGUI grassFlowersAmountText;

    [SerializeField] TextMeshProUGUI sandWaterAmountText;
    [SerializeField] TextMeshProUGUI sandPalmTreeAmountText;
    [SerializeField] TextMeshProUGUI sandRocksAmountText;
    [SerializeField] TextMeshProUGUI sandCactusAmountText;


    [SerializeField] private Slider biomesAmountSlider;
    public int biomesAmount;

    //Forest Biome Selected in the editor
    public void ForestScreenSelected()
    {
        forestScreen.SetActive(true);
        desertScreen.SetActive(false);
        desertIconSelected.SetActive(false);
        forestIconSelected.SetActive(true);

        forestScreen2.SetActive(true);
        desertScreen2.SetActive(false);
        desertIconSelected2.SetActive(false);
        forestIconSelected2.SetActive(true);
    }

    //Desert Biome Selected in the editor
    public void DesertScreenSelected()
    {
        forestScreen.SetActive(false);
        desertScreen.SetActive(true);
        desertIconSelected.SetActive(true);
        forestIconSelected.SetActive(false);

        forestScreen2.SetActive(false);
        desertScreen2.SetActive(true);
        forestIconSelected2.SetActive(false);
        desertIconSelected2.SetActive(true);
    }
}
