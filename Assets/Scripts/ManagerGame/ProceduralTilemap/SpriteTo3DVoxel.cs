using UnityEngine;

public class SpriteTo3DVoxel : MonoBehaviour
{
    [Header("Sprite to Convert")]
    public Sprite SourceSprite; // O sprite 2D do Tommy

    public Material VoxelMaterial; // Material para os voxels (pixel art)

    public float VoxelSize = 0.1f; // Tamanho de cada voxel (cube)

    public bool UseColorsFromSprite = true; 
    // Se true, usa cores dos pixels; senão, usa material único

    [Header("Output")]
    public GameObject VoxelParent; // GameObject pai para os voxels (opcional)

    public void ConvertSpriteTo3D()
    {
        if (SourceSprite == null) 
        {
            Debug.LogError("Sprite de origem não atribuído!");
            
            return;
        }

        Texture2D texture = SourceSprite.texture;

        if (texture == null) 
        {
            Debug.LogError("Textura não encontrada!");
            
            return;
        }

        // Criar GameObject pai se não existir
        if (VoxelParent == null)
        {
            VoxelParent = new GameObject("VoxelTommy");
        
            VoxelParent.transform.position = Vector3.zero;
        }

        // Obter pixels da textura
        Color[] pixels = texture.GetPixels();

        int width = texture.width;
        
        int height = texture.height;

        // Pivot do sprite (assumindo bottom-left)
        Vector2 pivot = SourceSprite.pivot / SourceSprite.pixelsPerUnit;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = pixels[y * width + x];

                // Só criar voxel se o pixel for opaco (alpha > 0.5)
                if (pixelColor.a > 0.5f)
                {
                    // Posição do voxel
                    Vector3 position = new Vector3((x - pivot.x) * VoxelSize,
                    (height - 1 - y - pivot.y) * VoxelSize, 0.0f);
                    // Inverter Y para coordenadas Unity
                    // Z = 0 para sprite 2D convertido

                    // Criar cube
                    GameObject voxel = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    voxel.transform.position = position;

                    voxel.transform.localScale = Vector3.one * VoxelSize;
                    
                    voxel.transform.parent = VoxelParent.transform;

                    // Aplicar material/cor
                    Renderer renderer = voxel.GetComponent<Renderer>();

                    if (UseColorsFromSprite)
                    {
                        // Criar material temporário com cor do pixel
                        Material tempMat = new Material(Shader.Find("Unlit/Color"));

                        tempMat.color = pixelColor;

                        renderer.material = tempMat;
                    }

                    else if (VoxelMaterial != null)
                    {
                        renderer.material = VoxelMaterial;
                    }

                    voxel.name = $"Voxel_{x}_{y}";
                }
            }
        }

        Debug.Log("Conversão de sprite para 3D voxel concluída!");
    }

    // Método para limpar voxels
    public void ClearVoxels()
    {
        if (VoxelParent != null)
        {
            DestroyImmediate(VoxelParent);
        }
    }
}