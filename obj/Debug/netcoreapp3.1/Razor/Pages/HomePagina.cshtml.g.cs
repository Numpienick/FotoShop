#pragma checksum "C:\Users\eelco_b0keash\OneDrive\Documenten\Week 3\FotoShop\Pages\HomePagina.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fe56914c57ee14303b583e91fae7c90a6de8ebd0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_HomePagina), @"mvc.1.0.razor-page", @"/Pages/HomePagina.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe56914c57ee14303b583e91fae7c90a6de8ebd0", @"/Pages/HomePagina.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/_ViewImports.cshtml")]
    public class Pages_HomePagina : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\eelco_b0keash\OneDrive\Documenten\Week 3\FotoShop\Pages\HomePagina.cshtml"
  
    ViewData["Title"] = "Homepagina";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fe56914c57ee14303b583e91fae7c90a6de8ebd02966", async() => {
                WriteLiteral("\r\n    <div class=\"container-fluid\">\r\n        <div class=\"titel\">\r\n            <h1>");
#nullable restore
#line 12 "C:\Users\eelco_b0keash\OneDrive\Documenten\Week 3\FotoShop\Pages\HomePagina.cshtml"
           Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h1>
        </div>
        <div class=""row"">
            <div class=""col-md-6 linker"">
                <h2 class=""LI"">Gecatogoriseerd in drie genres</h2>
                <img class=""logo-size"" src=""Images/HomeCollage.png"" alt=""logo"" />
            </div>
            <div class=""col-md-6 rechter"">
                <div class=""Rechterkant"">
                    <h2>Over de fotograaf</h2>
                    <img class=""logo-size1"" src=""Images/Foto.jpg"" alt=""logo"" /><br><br><br>
                    <span class=""NormaleT"">
                        Nog te verwerken Informatie/Nog te verwerken Informatie <br>Nog te verwerken InformatieNog te verwerken Informatie
                        Nog te verwerken InformatieNog te verwerken Informatie<br>Nog te verwerken Informatie
                    </span>
                </div><br><br><br>
            </div>
        </div>
    </div>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FotoShop.Pages.HomePagina> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FotoShop.Pages.HomePagina> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FotoShop.Pages.HomePagina>)PageContext?.ViewData;
        public FotoShop.Pages.HomePagina Model => ViewData.Model;
    }
}
#pragma warning restore 1591
