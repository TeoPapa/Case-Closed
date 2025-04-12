using System.Collections.Generic;
using UnityEditor.U2D.Aseprite.Common;

namespace UnityEditor.U2D.Aseprite
{
    internal static class TextureImporterPlatformUtilities
    {
        public static readonly TextureImporterPlatformSettings defaultPlatformSettings = new ()
        {
            name = "Default",
            textureCompression = TextureImporterCompression.Uncompressed
        };
        
        public static TextureImporterPlatformSettings GetPlatformTextureSettings(BuildTarget buildTarget, IReadOnlyList<TextureImporterPlatformSettings> platformSettings)
        {
            var buildTargetName = TexturePlatformSettingsHelper.GetBuildTargetGroupName(buildTarget);
            TextureImporterPlatformSettings settings = null;
            foreach (var platformSetting in platformSettings)
            {
                if (platformSetting.name == buildTargetName && platformSetting.overridden)
                    settings = platformSetting;
                else if (platformSetting.name == TexturePlatformSettingsHelper.defaultPlatformName)
                    settings = platformSetting;
            }

            if (settings == null)
            {
                settings = defaultPlatformSettings.Clone();
                settings.name = buildTargetName;
                settings.overridden = false;
            }
            return settings;
        }
        
        public static TextureImporterPlatformSettings Clone(this TextureImporterPlatformSettings settings)
        {
            var clone = new TextureImporterPlatformSettings()
            {
                name = settings.name,
                overridden = settings.overridden,
                maxTextureSize = settings.maxTextureSize,
                resizeAlgorithm = settings.resizeAlgorithm,
                textureCompression = settings.textureCompression,
                compressionQuality = settings.compressionQuality,
                crunchedCompression = settings.crunchedCompression,
                allowsAlphaSplitting = settings.allowsAlphaSplitting,
                androidETC2FallbackOverride = settings.androidETC2FallbackOverride
            };
            return clone;
        }
    }
}
