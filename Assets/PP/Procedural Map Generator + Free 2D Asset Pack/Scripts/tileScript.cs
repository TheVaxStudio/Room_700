using UnityEngine;
using UnityEngine.UI;


namespace Tile.Utilities
{


public class tileScript : MonoBehaviour
{
    [Header("DON'T TOUCH")]
    //Tile image
    public Image tileImage;

    //Sprites
    [SerializeField] Sprite topLeftMapCornerGrass;
    [SerializeField] Sprite topLeftMapCornerSand;
    [SerializeField] Sprite leftMapCornerGrass;
    [SerializeField] Sprite leftMapCornerSand;
    [SerializeField] Sprite bottomLeftMapCornerGrass;
    [SerializeField] Sprite bottomLeftMapCornerSand;
    [SerializeField] Sprite bottomMapCornerGrass;
    [SerializeField] Sprite bottomMapCornerSand;
    [SerializeField] Sprite bottomRightMapCornerGrass;
    [SerializeField] Sprite bottomRightMapCornerSand;
    [SerializeField] Sprite rightMapCornerGrass;
    [SerializeField] Sprite rightMapCornerSand;
    [SerializeField] Sprite topRightMapCornerGrass;
    [SerializeField] Sprite topRightMapCornerSand;
    [SerializeField] Sprite topMapCornerGrass;
    [SerializeField] Sprite topMapCornerSand;

    [SerializeField] Sprite sandCoreSprite;

    [SerializeField] Sprite sandSprite_1;
    [SerializeField] Sprite sandSprite_2;
    [SerializeField] Sprite sandSprite_3;
    [SerializeField] Sprite sandSprite_4;
    [SerializeField] Sprite sandSprite_5;
    [SerializeField] Sprite sandSprite_6;
    [SerializeField] Sprite sandSprite_7;
    [SerializeField] Sprite sandSprite_8;
    [SerializeField] Sprite sandSprite_9;
    [SerializeField] Sprite sandSprite_10;

    [SerializeField] Sprite cactusSprite_1;
    [SerializeField] Sprite cactusSprite_2;
    [SerializeField] Sprite cactusSprite_3;
    [SerializeField] Sprite cactusSprite_4;
    [SerializeField] Sprite cactusSprite_5;
    [SerializeField] Sprite cactusSprite_6;
    [SerializeField] Sprite cactusSprite_7;
    [SerializeField] Sprite cactusSprite_8;
    [SerializeField] Sprite cactusSprite_9;
    [SerializeField] Sprite cactusSprite_10;

    [SerializeField] Sprite palmTreeSprite_1;
    [SerializeField] Sprite palmTreeSprite_2;
    [SerializeField] Sprite palmTreeSprite_3;
    [SerializeField] Sprite palmTreeSprite_4;
    [SerializeField] Sprite palmTreeSprite_5;
    [SerializeField] Sprite palmTreeSprite_6;
    [SerializeField] Sprite palmTreeSprite_7;
    [SerializeField] Sprite palmTreeSprite_8;
    [SerializeField] Sprite palmTreeSprite_9;
    [SerializeField] Sprite palmTreeSprite_10;

    [SerializeField] Sprite grassCoreSprite;

    [SerializeField] Sprite grassSprite_1;
    [SerializeField] Sprite grassSprite_2;
    [SerializeField] Sprite grassSprite_3;
    [SerializeField] Sprite grassSprite_4;
    [SerializeField] Sprite grassSprite_5;
    [SerializeField] Sprite grassSprite_6;
    [SerializeField] Sprite grassSprite_7;
    [SerializeField] Sprite grassSprite_8;
    [SerializeField] Sprite grassSprite_9;
    [SerializeField] Sprite grassSprite_10;

    [SerializeField] Sprite grassWaterSprite_1;
    [SerializeField] Sprite grassWaterSprite_2;
    [SerializeField] Sprite grassWaterSprite_3;
    [SerializeField] Sprite grassWaterSprite_4;
    [SerializeField] Sprite grassWaterSprite_5;
    [SerializeField] Sprite grassWaterSprite_6;
    [SerializeField] Sprite grassWaterSprite_7;
    [SerializeField] Sprite grassWaterSprite_8;
    [SerializeField] Sprite grassWaterSprite_9;
    [SerializeField] Sprite grassWaterSprite_10;
    [SerializeField] Sprite grassWaterSprite_11;
    [SerializeField] Sprite grassWaterSprite_12;
    [SerializeField] Sprite grassWaterSprite_13;
    [SerializeField] Sprite grassWaterSprite_14;
    [SerializeField] Sprite grassWaterSprite_15;
    [SerializeField] Sprite grassWaterSprite_16;
    [SerializeField] Sprite grassWaterSprite_17;
    [SerializeField] Sprite grassWaterSprite_18;
    [SerializeField] Sprite grassWaterSprite_19;
    [SerializeField] Sprite grassWaterSprite_20;
    [SerializeField] Sprite fullWaterSprite;
    [SerializeField] Sprite grassWaterSprite_22;
    [SerializeField] Sprite grassWaterSprite_23;
    [SerializeField] Sprite grassWaterSprite_24;
    [SerializeField] Sprite grassWaterSprite_25;
    [SerializeField] Sprite grassWaterSprite_26;
    [SerializeField] Sprite grassWaterSprite_27;
    [SerializeField] Sprite grassWaterSprite_28;
    [SerializeField] Sprite grassWaterSprite_29;
    [SerializeField] Sprite grassWaterSprite_30;
    [SerializeField] Sprite grassWaterSprite_31;
    [SerializeField] Sprite grassWaterSprite_32;
    [SerializeField] Sprite grassWaterSprite_33;
    [SerializeField] Sprite grassWaterSprite_34;
    [SerializeField] Sprite grassWaterSprite_35;
    [SerializeField] Sprite grassWaterSprite_36;
    [SerializeField] Sprite grassWaterSprite_37;
    [SerializeField] Sprite grassWaterSprite_38;
    [SerializeField] Sprite grassWaterSprite_39;
    [SerializeField] Sprite grassWaterSprite_40;
    [SerializeField] Sprite grassWaterSprite_41;
    [SerializeField] Sprite grassWaterSprite_42;
    [SerializeField] Sprite grassWaterSprite_43;
    [SerializeField] Sprite grassWaterSprite_44;
    [SerializeField] Sprite grassWaterSprite_45;
    [SerializeField] Sprite grassWaterSprite_46;
    [SerializeField] Sprite grassWaterSprite_47;

    [SerializeField] Sprite sandWaterSprite_1;
    [SerializeField] Sprite sandWaterSprite_2;
    [SerializeField] Sprite sandWaterSprite_3;
    [SerializeField] Sprite sandWaterSprite_4;
    [SerializeField] Sprite sandWaterSprite_5;
    [SerializeField] Sprite sandWaterSprite_6;
    [SerializeField] Sprite sandWaterSprite_7;
    [SerializeField] Sprite sandWaterSprite_8;
    [SerializeField] Sprite sandWaterSprite_9;
    [SerializeField] Sprite sandWaterSprite_10;
    [SerializeField] Sprite sandWaterSprite_11;
    [SerializeField] Sprite sandWaterSprite_12;
    [SerializeField] Sprite sandWaterSprite_13;
    [SerializeField] Sprite sandWaterSprite_14;
    [SerializeField] Sprite sandWaterSprite_15;
    [SerializeField] Sprite sandWaterSprite_16;
    [SerializeField] Sprite sandWaterSprite_17;
    [SerializeField] Sprite sandWaterSprite_18;
    [SerializeField] Sprite sandWaterSprite_19;
    [SerializeField] Sprite sandWaterSprite_20;
    [SerializeField] Sprite sandWaterSprite_21;
    [SerializeField] Sprite sandWaterSprite_22;
    [SerializeField] Sprite sandWaterSprite_23;
    [SerializeField] Sprite sandWaterSprite_24;
    [SerializeField] Sprite sandWaterSprite_25;
    [SerializeField] Sprite sandWaterSprite_26;
    [SerializeField] Sprite sandWaterSprite_27;
    [SerializeField] Sprite sandWaterSprite_28;
    [SerializeField] Sprite sandWaterSprite_29;
    [SerializeField] Sprite sandWaterSprite_30;
    [SerializeField] Sprite sandWaterSprite_31;
    [SerializeField] Sprite sandWaterSprite_32;
    [SerializeField] Sprite sandWaterSprite_33;
    [SerializeField] Sprite sandWaterSprite_34;
    [SerializeField] Sprite sandWaterSprite_35;
    [SerializeField] Sprite sandWaterSprite_36;
    [SerializeField] Sprite sandWaterSprite_37;
    [SerializeField] Sprite sandWaterSprite_38;
    [SerializeField] Sprite sandWaterSprite_39;
    [SerializeField] Sprite sandWaterSprite_40;
    [SerializeField] Sprite sandWaterSprite_41;
    [SerializeField] Sprite sandWaterSprite_42;
    [SerializeField] Sprite sandWaterSprite_43;
    [SerializeField] Sprite sandWaterSprite_44;
    [SerializeField] Sprite sandWaterSprite_45;
    [SerializeField] Sprite sandWaterSprite_46;
    [SerializeField] Sprite sandWaterSprite_47;

    [SerializeField] Sprite treeSprite_1;
    [SerializeField] Sprite treeSprite_2;
    [SerializeField] Sprite treeSprite_3;
    [SerializeField] Sprite treeSprite_4;
    [SerializeField] Sprite treeSprite_5;
    [SerializeField] Sprite treeSprite_6;
    [SerializeField] Sprite treeSprite_7;
    [SerializeField] Sprite treeSprite_8;
    [SerializeField] Sprite treeSprite_9;
    [SerializeField] Sprite treeSprite_10;

    [SerializeField] Sprite sandRocksSprite;

    [SerializeField] Sprite jungleRocksSprite_1;
    [SerializeField] Sprite jungleRocksSprite_2;
    [SerializeField] Sprite jungleRocksSprite_3;
    [SerializeField] Sprite jungleRocksSprite_4;
    [SerializeField] Sprite jungleRocksSprite_5;

    [SerializeField] Sprite flowersSprite_1;
    [SerializeField] Sprite flowersSprite_2;
    [SerializeField] Sprite flowersSprite_3;
    [SerializeField] Sprite flowersSprite_4;
    [SerializeField] Sprite flowersSprite_5;
    [SerializeField] Sprite flowersSprite_6;
    [SerializeField] Sprite flowersSprite_7;
    [SerializeField] Sprite flowersSprite_8;
    [SerializeField] Sprite flowersSprite_9;
    [SerializeField] Sprite flowersSprite_10;

    [SerializeField] private GameObject bottomSquarePalmTree;
    [SerializeField] private GameObject topSquarePalmTree;
    [SerializeField] private GameObject topSquareForestTree;
    [SerializeField] private GameObject bottomSquareForestTree;

    [SerializeField] Sprite leftGrassRightSandSprite;
    [SerializeField] Sprite leftSandRightGrassSprite;
    [SerializeField] Sprite topSandBottomGrassSprite;
    [SerializeField] Sprite topGrassBottomSandSprite;
    [SerializeField] Sprite topLeftGrassSprite;
    [SerializeField] Sprite topRightGrassSprite;
    [SerializeField] Sprite bottomRightGrassSprite;
    [SerializeField] Sprite bottomLeftGrassSprite;

    [SerializeField] Sprite transitionLTopLeft;
    [SerializeField] Sprite transitionLTopRight;
    [SerializeField] Sprite transitionLBottomRight;
    [SerializeField] Sprite transitionLBottomLeft;


    [Header("AUTOMATIC")]
    //Tile ID 
    public int tileId;

    //adjacentTiles
    public tileScript topAdjacentTile;
    public tileScript leftAdjacentTile;
    public tileScript bottomAdjacentTile;
    public tileScript rightAdjacentTile;

    public tileScript topLeftAdjacentTile;
    public tileScript topRightAdjacentTile;
    public tileScript bottomLeftAdjacentTile;
    public tileScript bottomRightAdjacentTile;

    public int biomeIdOfThisTile;

    public int randomNumToChooseTileVariant;

    [SerializeField] private GameObject gameManagerGameObject;
    [SerializeField] private editModeScript editModeScript;

    //Tile types
    public enum TileType
    {
        EMPTY,
        TOP_LEFT_MAP_CORNER_GRASS,
        TOP_LEFT_MAP_CORNER_SAND,
        LEFT_MAP_CORNER_GRASS,
        LEFT_MAP_CORNER_SAND,
        BOTTOM_LEFT_MAP_CORNER_GRASS,
        BOTTOM_LEFT_MAP_CORNER_SAND,
        BOTTOM_MAP_CORNER_GRASS,
        BOTTOM_MAP_CORNER_SAND,
        BOTTOM_RIGHT_MAP_CORNER_GRASS,
        BOTTOM_RIGHT_MAP_CORNER_SAND,
        RIGHT_MAP_CORNER_GRASS,
        RIGHT_MAP_CORNER_SAND,
        TOP_RIGHT_MAP_CORNER_GRASS,
        TOP_RIGHT_MAP_CORNER_SAND,
        TOP_MAP_CORNER_GRASS,
        TOP_MAP_CORNER_SAND,
        SAND_CORE,
        SAND,
        GRASS_CORE,
        GRASS,
        WATER,
        SAND_WATER,
        TREE,
        SAND_ROCKS,
        CACTUS,
        PALM_TREE,
        JUNGLE_ROCKS,
        FLOWERS,
        LEFT_GRASS_RIGHT_SAND,
        LEFT_SAND_RIGHT_GRASS,
        TOP_GRASS_BOTTOM_SAND,
        TOP_SAND_BOTTOM_GRASS,
        TOP_LEFT_GRASS,
        TOP_RIGHT_GRASS,
        BOTTOM_LEFT_GRASS,
        BOTTOM_RIGHT_GRASS,
        TRANSITION_L_TOP_LEFT,
        TRANSITION_L_TOP_RIGHT,
        TRANSITION_L_BOTTOM_LEFT,
        TRANSITION_L_BOTTOM_RIGHT,
    };

    //Tiletype Initialization
    public TileType tileType;

        //Change Tile when selected in the editor mode
        public void ChangeTileSelected()
    {
        switch (editModeScript.selectedTypeOfTile)
        {
            default:
                break;

            case 0:
                tileType = TileType.GRASS;
                biomeIdOfThisTile = 9;
                break;

            case 1:
                tileType = TileType.WATER;
                biomeIdOfThisTile = 9;
                break;

            case 2:
                tileType = TileType.TREE;
                biomeIdOfThisTile = 9;
                break;

            case 3:
                tileType = TileType.JUNGLE_ROCKS;
                biomeIdOfThisTile = 9;
                break;

            case 4:
                tileType = TileType.FLOWERS;
                biomeIdOfThisTile = 9;
                break;

            case 5:
                tileType = TileType.SAND;
                biomeIdOfThisTile = 10;
                break;

            case 6:
                tileType = TileType.SAND_WATER;
                biomeIdOfThisTile = 10;
                break;

            case 7:
                tileType = TileType.SAND_ROCKS;
                biomeIdOfThisTile = 10;
                break;

            case 8:
                tileType = TileType.CACTUS;
                biomeIdOfThisTile = 10;
                break;

            case 9:
                tileType = TileType.PALM_TREE;
                biomeIdOfThisTile = 10;
                break;
        }
    }

    //Change tile when holding paint button in the editor mode
    public void ChangeTileSelectedWhenHolding()
    {
        if (editModeScript.isHoldingPaintButton)
        {
            switch (editModeScript.selectedTypeOfTile)
            {
                default:
                    break;

                case 0:
                    tileType = TileType.GRASS;
                    biomeIdOfThisTile = 9;
                    break;

                case 1:
                    tileType = TileType.WATER;
                    biomeIdOfThisTile = 9;
                    break;

                case 2:
                    tileType = TileType.TREE;
                    biomeIdOfThisTile = 9;
                    break;

                case 3:
                    tileType = TileType.JUNGLE_ROCKS;
                    biomeIdOfThisTile = 9;
                    break;

                case 4:
                    tileType = TileType.FLOWERS;
                    biomeIdOfThisTile = 9;
                    break;

                case 5:
                    tileType = TileType.SAND;
                    biomeIdOfThisTile = 10;
                    break;

                case 6:
                    tileType = TileType.SAND_WATER;
                    biomeIdOfThisTile = 10;
                    break;

                case 7:
                    tileType = TileType.SAND_ROCKS;
                    biomeIdOfThisTile = 10;
                    break;

                case 8:
                    tileType = TileType.CACTUS;
                    biomeIdOfThisTile = 10;
                    break;

                case 9:
                    tileType = TileType.PALM_TREE;
                    biomeIdOfThisTile = 10;
                    break;
            }
        }
    }
    }

}
