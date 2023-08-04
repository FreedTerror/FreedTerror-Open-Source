#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace UFE2FTE
{
    public static class SpriteAtlasTextureExporter
    {
        [MenuItem("Assets/Export Sprite Atlases As PNG", false, -100)]
        static void ExportSpriteAtlasesAsPNG()
        {
            string exportPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Sprite Atlases";

            foreach (UnityEngine.Object obj in Selection.objects)
            {
                SpriteAtlas atlas = (SpriteAtlas)obj;

                if (atlas == null) continue;

                Debug.LogWarning("Exporting selected atlas: " + atlas);

                // Use reflection to run this internal editor method.
                // UnityEditor.U2D.SpriteAtlasExtensions.GetPreviewTextures
                // internal static extern Texture2D[] GetPreviewTextures(this SpriteAtlas spriteAtlas);
                Type type = typeof(UnityEditor.U2D.SpriteAtlasExtensions);
                MethodInfo methodInfo = type.GetMethod("GetPreviewTextures", BindingFlags.Static | BindingFlags.NonPublic);
                if (methodInfo == null)
                {
                    Debug.LogWarning("Failed to get UnityEditor.U2D.SpriteAtlasExtensions");

                    return;
                }

                Texture2D[] textures = (Texture2D[])methodInfo.Invoke(null, new object[] { atlas });
                if (textures == null)
                {
                    Debug.LogWarning("Failed to get texture results");

                    continue;
                }

                string finalExportPath = exportPath + "/" + atlas.name;
                int textureNumber = 0;
                foreach (Texture2D texture in textures)
                {
                    // These textures in memory are not saveable so copy them to a RenderTexture first.
                    Texture2D textureCopy = DuplicateTexture(texture);

                    if (!Directory.Exists(finalExportPath))
                    {
                        Directory.CreateDirectory(finalExportPath);
                    }

                    string filename = finalExportPath + "/" + atlas.name + " " + textureNumber.ToString() + ".png";

                    FileStream fs = new FileStream(filename, FileMode.Create);

                    BinaryWriter bw = new BinaryWriter(fs);

                    bw.Write(textureCopy.EncodeToPNG());

                    bw.Close();

                    fs.Close();

                    Debug.LogWarning("Saved texture to " + filename);

                    textureNumber++;
                }
            }
        }

        [MenuItem("Assets/Export Sprite Atlases As PNG", true)]
        static bool ExportSpriteAtlasesValidation()
        {
            return Selection.activeObject && Selection.activeObject.GetType() == typeof(SpriteAtlas);
        }

        private static Texture2D DuplicateTexture(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                source.width,
                source.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }
    }
}
#endif