using System.IO;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif
using UnityEngine;

namespace Gilzoide.WebGlJsInjector
{
    [ScriptedImporter(0, "jstag")]
    public class HtmlScriptTagImporter : ScriptedImporter
    {
        [Tooltip("These will be added to the script tag: `<script Attributes...>Code</script>`")]
        public string[] Attributes;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var code = new TextAsset(File.ReadAllText(ctx.assetPath))
            {
                name = "Code",
            };
            ctx.AddObjectToAsset("code", code);

            var scriptTag = ScriptableObject.CreateInstance<HtmlScriptTag>();
            scriptTag.Code = code;
            scriptTag.Attributes = Attributes;
            ctx.AddObjectToAsset("main", scriptTag);
            ctx.SetMainObject(scriptTag);
        }
    }
}
