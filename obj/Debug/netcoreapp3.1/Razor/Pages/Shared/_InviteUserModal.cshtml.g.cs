#pragma checksum "/Users/miguel/Desktop/code/chatcat/Pages/Shared/_InviteUserModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec0c1e7408842eb479f8a707b88e56264af29118"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(chatcat.Pages.Shared.Pages_Shared__InviteUserModal), @"mvc.1.0.view", @"/Pages/Shared/_InviteUserModal.cshtml")]
namespace chatcat.Pages.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/miguel/Desktop/code/chatcat/Pages/_ViewImports.cshtml"
using chatcat;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec0c1e7408842eb479f8a707b88e56264af29118", @"/Pages/Shared/_InviteUserModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bec20d3ad64adf69e634cb7d2561517cc871989f", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared__InviteUserModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""inviteUserModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""inviteUserModalLabel"" aria-hidden=""true"">
  <div class=""modal-dialog"" role=""document"">
    <div class=""modal-content"">
      <div class=""modal-header"">
        <h5 class=""modal-title"" id=""inviteUserModalLabel""><img style=""width: 1.5rem; height: 1.5rem;"" src=""https://i7.uihere.com/icons/280/167/689/cat-77d66f0a4a50e34849199b98f127c5b4.png""/> invite to room</h5>
        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
          <span aria-hidden=""true"">&times;</span>
        </button>
      </div>
      <div class=""modal-body"">
        <p>would you like to invite <span id=""inviteConnectionID""></span> to '<span id=""inviteRoomName""></span>'?</p>
      </div>
      <div class=""modal-footer"">
        <button type=""button"" class=""catButton"" data-dismiss=""modal"">cancel</button>
        <button type=""button"" id=""inviteUserConfirmButton"" class=""catButton"" >invite</button>
      </div>
    </div>
  </div");
            WriteLiteral(">\n</div>");
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
