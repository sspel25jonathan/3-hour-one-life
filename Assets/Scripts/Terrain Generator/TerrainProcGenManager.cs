using UnityEngine;
using System.Collections.Generic;

[SerializeField]
public class BiomeConfig
{
    public BiomeConfig Biome;

    [Range(0, 1)] public float Weight = 1f;

}

[CreateAssetMenu(fileName = "GenProcConfig", menuName = "Gen/TerrainProcConfig", order = -1)]
public class TerrainProcGenManager : MonoBehaviour
{

    public List<BiomeConfig> biomes;


}
