using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class valuesAdjusterScript : MonoBehaviour
{
    [Header("GRASS BIOME SPAWN VALUES (0-5)")]
    //GRASS BIOME SPAWN VALUES
    public int grassBiomesRiverSpawn;
    public int grassBiomesForestSpawn;
    public int grassBiomesRocksSpawn;
    public int grassBiomesFlowersSpawn;


    [Header("GRASS BIOME EXPANSION VALUES (0-100)")]
    //GRASS BIOME EXPANSION VALUES
    public int grassBiomesRiverExpansionPercentage;
    public int grassBiomesForestExpansionPercentage;
    public int grassBiomesJungleRocksExpansionPercentage;
    public int grassBiomesFlowersExpansionPercentage;


    [Header("SAND BIOME SPAWN VALUES (0-5)")]
    //GRASS BIOME SPAWN VALUES
    public int sandBiomesRiverSpawn;
    public int sandBiomesSandRocksSpawn;
    public int sandBiomesCactusSpawn;
    public int sandBiomesPalmTreeSpawn;

    [Header("SAND BIOME EXPANSION VALUES (0-100)")]
    //SAND BIOME VALUES
    public int sandBiomesRiverExpansionPercentage;
    public int sandBiomesPalmTreeExpansionPercentage;
    public int sandBiomesCactusExpansionPercentage;
    public int sandBiomesSandRocksExpansionPercentage;


    [Header("CHOOSE TILE SCRIPT VALUES")]
    //LENGHT MUST BE 20
    public int[] weightedChoicesGrass = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    public int[] weightedChoicesSand = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
}
