#pragma checksum "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3f333e4dce7191ab545ae89a5ce6b5cbe8ce2385"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Reports_MemberListReport), @"mvc.1.0.view", @"/Areas/Admin/Views/Reports/MemberListReport.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Reports/MemberListReport.cshtml", typeof(AspNetCore.Areas_Admin_Views_Reports_MemberListReport))]
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
#line 1 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\_ViewImports.cshtml"
using CVGS;

#line default
#line hidden
#line 2 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\_ViewImports.cshtml"
using CVGS.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3f333e4dce7191ab545ae89a5ce6b5cbe8ce2385", @"/Areas/Admin/Views/Reports/MemberListReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50662a4b06b34ddc6a7692a13ac4f749ea9cc64c", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Reports_MemberListReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CVGS.Models.ApplicationUser>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_ReportLayout.cshtml";

#line default
#line hidden
            BeginContext(143, 551, true);
            WriteLiteral(@"<h1>Member List Report</h1>
<div class=""row"">
    <div class=""col-12"">
        <table class=""table table-striped border"">
            <tr class=""table-info"">
                <th>
                    First Name
                </th>
                <th>
                    Last Name
                </th>
                <th>
                    Email
                </th>
                <th>
                    Role
                </th>
                <th>
                    Gender
                </th>
            </tr>
");
            EndContext();
#line 27 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(751, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(824, 36, false);
#line 31 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
                   Write(Html.DisplayFor(m => item.FirstName));

#line default
#line hidden
            EndContext();
            BeginContext(860, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(940, 35, false);
#line 34 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
                   Write(Html.DisplayFor(m => item.LastName));

#line default
#line hidden
            EndContext();
            BeginContext(975, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1055, 32, false);
#line 37 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
                   Write(Html.DisplayFor(m => item.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1087, 81, true);
            WriteLiteral("\r\n                    </td>\r\n\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1169, 31, false);
#line 41 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
                   Write(Html.DisplayFor(m => item.Role));

#line default
#line hidden
            EndContext();
            BeginContext(1200, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1280, 33, false);
#line 44 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
                   Write(Html.DisplayFor(m => item.Gender));

#line default
#line hidden
            EndContext();
            BeginContext(1313, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 47 "C:\Users\dk95g\Desktop\Iteration3\Construction\cvgs-iteration_3\CVGS\Areas\Admin\Views\Reports\MemberListReport.cshtml"
            }

#line default
#line hidden
            BeginContext(1380, 40, true);
            WriteLiteral("        </table>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CVGS.Models.ApplicationUser>> Html { get; private set; }
    }
}
#pragma warning restore 1591