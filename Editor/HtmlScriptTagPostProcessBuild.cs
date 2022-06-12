using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Gilzoide.WebGlJsInjector
{
    public static class HtmlScriptTagPostProcessBuild
    {
        [PostProcessBuild]
        static void InjectHtmlScriptTags(BuildTarget target, string path)
        {
            if (target != BuildTarget.WebGL)
            {
                return;
            }

            string indexHtmlPath = Path.Combine(path, "index.html");
            string html = File.ReadAllText(indexHtmlPath);
            int scriptInjectionPosition = FindScriptInjectionPoint(html);

            var htmlBuilder = new StringBuilder(html, 0, scriptInjectionPosition, 0);

            var scriptTags = AssetDatabase.FindAssets("t:" + nameof(HtmlScriptTag))
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadMainAssetAtPath)
                .Cast<HtmlScriptTag>();
            foreach (HtmlScriptTag tag in scriptTags)
            {
                AppendScriptTag(htmlBuilder, tag);
            }

            htmlBuilder.Append(html, scriptInjectionPosition, html.Length - scriptInjectionPosition);

            File.WriteAllText(indexHtmlPath, htmlBuilder.ToString());
        }

        static int FindScriptInjectionPoint(string html)
        {
            int scriptInjectionPosition = html.LastIndexOf("<script");
            if (scriptInjectionPosition < 0)
            {
                scriptInjectionPosition = html.LastIndexOf("</body");
            }
            if (scriptInjectionPosition < 0)
            {
                scriptInjectionPosition = html.LastIndexOf("</html");
            }
            if (scriptInjectionPosition < 0)
            {
                scriptInjectionPosition = html.Length;
            }
            return scriptInjectionPosition;
        }

        static void AppendScriptTag(StringBuilder htmlBuilder, HtmlScriptTag tag)
        {
            htmlBuilder.Append("<script");
            foreach (string attribute in tag.Attributes)
            {
                htmlBuilder.Append(' ');
                htmlBuilder.Append(attribute);
            }
            htmlBuilder.Append('>');
            if (tag.Code.text.Length != 0)
            {
                htmlBuilder.Append('\n');
                htmlBuilder.Append(tag.Code.text);
                htmlBuilder.Append('\n');
            }
            htmlBuilder.Append("</script>\n");
        }
    }
}
