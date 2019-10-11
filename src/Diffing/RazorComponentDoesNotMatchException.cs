﻿using System.Linq;
using System.Text;
using System.Xml;
using Org.XmlUnit.Diff;
using Xunit.Sdk;

namespace Egil.RazorComponents.Testing.Diffing
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "<Pending>")]
    public class RazorComponentDoesNotMatchException : AssertActualExpectedException
    {
        public RazorComponentDoesNotMatchException(XmlNode expectedHtml, XmlNode renderedHtml, Diff diffResult)
            : base(expectedHtml.PrettyXml(),
                  renderedHtml.PrettyXml(),
                  CreateDiffMessage(diffResult),
                  "Expected HTML",
                  "Rendered HTML")
        {
        }

        private static string CreateDiffMessage(Diff diffResult)
        {
            var diffs = diffResult.Differences.ToArray();
            var result = new StringBuilder();
            result.AppendLine();
            result.AppendLine($"Found {diffs.Length} differences between expected HTML and rendered HTML:");

            for (int i = 0; i < diffs.Length; i++)
            {
                result.AppendLine($"{i + 1}) {diffs[i].ToString()}");
            }

            return result.ToString();
        }
    }
}