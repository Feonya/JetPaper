using UnityEditor;
using UnityEngine;

public class ImportFileAutoSetter : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        TextureImporter texture = (TextureImporter)assetImporter;

        texture.filterMode = FilterMode.Point;
        texture.textureCompression = TextureImporterCompression.Uncompressed;
        texture.spritePixelsPerUnit = 32.0f;
        // 根据需要加入内容
    }
}