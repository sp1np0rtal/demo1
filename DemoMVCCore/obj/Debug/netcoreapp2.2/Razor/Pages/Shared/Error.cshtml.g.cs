#pragma checksum "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0d92659af7982a4749284d8c9cc2ed72ac415f21"
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
#line 4 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0d92659af7982a4749284d8c9cc2ed72ac415f21", @"/Pages/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"853e474ae15a3d91692079552e4524d336813e22", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{

#line default
#line hidden
            BeginContext(160, 263, true);
            WriteLiteral(@"    <h3>An error occured. The Support team has been notified</h3>
    <h5>Please contact the Help Desk x5600</h5>
    <hr />
    <h3>Exception Details:    </h3>
    <div class=""alert alert-danger"">
        <h5>Exception Path</h5>
        <hr />
        <p>");
            EndContext();
            BeginContext(424, 21, false);
#line 13 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.ExceptionPath);

#line default
#line hidden
            EndContext();
            BeginContext(445, 119, true);
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"alert alert-danger\">\r\n        <h5>Exception Message</h5>\r\n        <hr />\r\n        <p>");
            EndContext();
            BeginContext(565, 24, false);
#line 18 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.ExceptionMessage);

#line default
#line hidden
            EndContext();
            BeginContext(589, 123, true);
            WriteLiteral("</p>\r\n    </div>\r\n    <div class=\"alert alert-danger\">\r\n        <h5>Exception Stack Trace</h5>\r\n        <hr />\r\n        <p>");
            EndContext();
            BeginContext(713, 18, false);
#line 23 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
      Write(ViewBag.Stacktrace);

#line default
#line hidden
            EndContext();
            BeginContext(731, 18, true);
            WriteLiteral("</p>\r\n    </div>\r\n");
            EndContext();
#line 25 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(761, 28, true);
            WriteLiteral("    <h1 class=\"text-danger\">");
            EndContext();
            BeginContext(790, 18, false);
#line 28 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
            EndContext();
            BeginContext(808, 35, true);
            WriteLiteral("</h1>\r\n    <h6 class=\"text-danger\">");
            EndContext();
            BeginContext(844, 20, false);
#line 29 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
                       Write(ViewBag.ErrorMessage);

#line default
#line hidden
            EndContext();
            BeginContext(864, 7, true);
            WriteLiteral("</h6>\r\n");
            EndContext();
#line 30 "C:\Users\mspinner\source\repos\DemoMVCCore-master\DemoMVCCore\Pages\Shared\Error.cshtml"
}

#line default
#line hidden
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
