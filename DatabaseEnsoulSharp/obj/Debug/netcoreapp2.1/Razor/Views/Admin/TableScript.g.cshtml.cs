#pragma checksum "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7365637bb33328744c1b1f415840525c20592bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_TableScript), @"mvc.1.0.view", @"/Views/Admin/TableScript.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/TableScript.cshtml", typeof(AspNetCore.Views_Admin_TableScript))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/_ViewImports.cshtml"
using DatabaseEnsoulSharp;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7365637bb33328744c1b1f415840525c20592bc", @"/Views/Admin/TableScript.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00e3e87a1c508b5b7d901bfea93ef2b4264a20e9", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_TableScript : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<DatabaseEnsoulSharp.Models.Database.ScriptInfo>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(61, 332, true);
            WriteLiteral(@"<div>
    <table id=""TableList"" class=""display table table-striped table-bordered"" cellspacing=""0"" width=""100%"">
        <thead>
            <tr>
                <th>Id</th>
                <th>Author</th>
                <th>Link</th>
                <th>Name</th>

            </tr>
        </thead>

        <tbody>
");
            EndContext();
#line 15 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(450, 46, true);
            WriteLiteral("                <tr>\r\n                    <td>");
            EndContext();
            BeginContext(497, 7, false);
#line 18 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
                   Write(item.Id);

#line default
#line hidden
            EndContext();
            BeginContext(504, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(536, 11, false);
#line 19 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
                   Write(item.Author);

#line default
#line hidden
            EndContext();
            BeginContext(547, 49, true);
            WriteLiteral("</td>\r\n                    <td><a target=\"_blank\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 596, "\"", 633, 2);
            WriteAttributeValue("", 603, "https://github.com", 603, 18, true);
#line 20 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
WriteAttributeValue("", 621, item.Link, 621, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(634, 42, true);
            WriteLiteral(">Github</a></td>\r\n                    <td>");
            EndContext();
            BeginContext(677, 9, false);
#line 21 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
                   Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(686, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 23 "/home/DatabaseEnsoulSharp/DatabaseEnsoulSharp/Views/Admin/TableScript.cshtml"
            }

#line default
#line hidden
            BeginContext(731, 38, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<DatabaseEnsoulSharp.Models.Database.ScriptInfo>> Html { get; private set; }
    }
}
#pragma warning restore 1591