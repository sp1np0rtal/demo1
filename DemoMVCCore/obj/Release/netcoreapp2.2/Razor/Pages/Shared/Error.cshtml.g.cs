#pragma checksum "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9af0f309bc76df4498298963d2910d305e24c1e5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(DemoMVCCore.Pages.Shared.Pages_Shared_Error), @"mvc.1.0.view", @"/Pages/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Pages/Shared/Error.cshtml", typeof(DemoMVCCore.Pages.Shared.Pages_Shared_Error))]
namespace DemoMVCCore.Pages.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\_ViewImports.cshtml"
using DemoMVCCore;

#line default
#line hidden
#line 2 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\_ViewImports.cshtml"
using DemoMVCCore.ViewModels;

#line default
#line hidden
#line 3 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\_ViewImports.cshtml"
using DemoMVCCore.Model;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9af0f309bc76df4498298963d2910d305e24c1e5", @"/Pages/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"529af02efbbe6d7bd980874a7970cc34272131b7", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(127, 263, true);
            WriteLiteral(@"    <h3>AN error occured. The Support team has been notified</h3>
    <h5>Please contact the Help Desk x5600</h5>
    <hr />
    <h3>Exception Details:    </h3>
    <div class=""alert alert-danger"">
        <h5>Exception Path</h5>
        <hr />
        <p>");
            EndContext();
            BeginContext(391, 21, false);
#line 12 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.ExceptionPath);

#line default
#line hidden
            EndContext();
            BeginContext(412, 119, true);
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"alert alert-danger\">\r\n        <h5>Exception Message</h5>\r\n        <hr />\r\n        <p>");
            EndContext();
            BeginContext(532, 24, false);
#line 17 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.ExceptionMEssage);

#line default
#line hidden
            EndContext();
            BeginContext(556, 123, true);
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"alert alert-danger\">\r\n        <h5>Exception Stack Trace</h5>\r\n        <hr />\r\n        <p>");
            EndContext();
            BeginContext(680, 18, false);
#line 22 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.Stacktrace);

#line default
#line hidden
            EndContext();
            BeginContext(698, 18, true);
            WriteLiteral("</p>\r\n    </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
