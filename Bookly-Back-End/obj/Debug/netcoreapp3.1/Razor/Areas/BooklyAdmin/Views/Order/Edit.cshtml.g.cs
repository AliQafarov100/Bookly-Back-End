#pragma checksum "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d12a027d566a20a883f26d26b55eeaf67aa9ecc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_BooklyAdmin_Views_Order_Edit), @"mvc.1.0.view", @"/Areas/BooklyAdmin/Views/Order/Edit.cshtml")]
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
#nullable restore
#line 1 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\_ViewImports.cshtml"
using Bookly_Back_End.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\_ViewImports.cshtml"
using Bookly_Back_End.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d12a027d566a20a883f26d26b55eeaf67aa9ecc", @"/Areas/BooklyAdmin/Views/Order/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7cea33a8ea27813dba5d3789188c77b8e066208b", @"/Areas/BooklyAdmin/Views/_ViewImports.cshtml")]
    public class Areas_BooklyAdmin_Views_Order_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Order>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 650px; width: 90%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Order", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Accept", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success accept"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Reject", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger reject"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    <div class=\"d-flex\">\r\n        <p>Id: </p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 8 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Delivery Address:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 12 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.Address);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Client Fullname:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 16 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                     Write(Model.AppUser.FirstName + " " + Model.AppUser.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Client Email:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 20 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.AppUser.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Delivery City:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 24 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.City.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Order Date:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 28 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.OrderDate.ToString("HH:mm dd,MMMM,yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <div class=\"d-flex\">\r\n        <p>Order TotalPrice:</p>\r\n        <h4 class=\"ms-3\">");
#nullable restore
#line 32 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                    Write(Model.TotalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n    </div>\r\n    <h4>Order Product Item:</h4>\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 36 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
         foreach (OrderProduct item in Model.OrderProducts)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-lg-4 mt-2\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "2d12a027d566a20a883f26d26b55eeaf67aa9ecc8973", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1182, "~/assets/Image/Library/", 1182, 23, true);
#nullable restore
#line 39 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
AddHtmlAttributeValue("", 1205, item.Book.BookImages.FirstOrDefault(i => i.IsMain == true)?.ImagePath, 1205, 70, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" <br />\r\n                <strong>");
#nullable restore
#line 40 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                   Write(item.Book.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> <br />\r\n                <strong>\r\n                    $\r\n                    ");
#nullable restore
#line 43 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                Write(item.Book.DiscountId != null ? (item.Book.Price - ((item.Book.Price * item.Book.Discount.DiscountPercent) / 100)) : (item.Book.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </strong> <br />\r\n                <strong>\r\n                    Count:\r\n                    ");
#nullable restore
#line 47 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                Write(item.Book.DiscountId != null ? (Model.TotalPrice / (item.Book.Price - ((item.Book.Price * item.Book.Discount.DiscountPercent) / 100))) : (Model.TotalPrice / item.Book.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </strong>\r\n            </div>\r\n");
#nullable restore
#line 50 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n    <div>\r\n        <strong>");
#nullable restore
#line 54 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
           Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n\r\n        <input class=\"message\" cols=\"45\" rows=\"10\" />\r\n\r\n        <div class=\"mt-5\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2d12a027d566a20a883f26d26b55eeaf67aa9ecc12674", async() => {
                WriteLiteral("\r\n                <i class=\"mdi mdi-file-check btn-icon-append\"></i>\r\n                Accept\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 59 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                                                            WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2d12a027d566a20a883f26d26b55eeaf67aa9ecc15272", async() => {
                WriteLiteral("\r\n                <i class=\"mdi mdi-file-check btn-icon-append\"></i>\r\n                Reject\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 63 "C:\Users\M S I\Desktop\Bookly-Back-End\Bookly-Back-End\Areas\BooklyAdmin\Views\Order\Edit.cshtml"
                                                            WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $("".accept"").click(function (e) {
                e.preventDefault();
                var message = $("".message"").val();
                var link = $(this).attr(""href"") + ""/?message="" + message
                fetch(link).then(end => end.json()).then(datas => {
                    if (datas.status == 200) {
                        location.href = ""https://localhost:44339/BooklyAdmin/Order/Orders""
                    }
                })
            })

        })
        $(document).ready(function () {
            $("".reject"").click(function (e) {
                e.preventDefault();
                var message = $("".message"").val();
                var link = $(this).attr(""href"") + ""/?message="" + message
                fetch(link).then(end => end.json()).then(datas => {
                    if (datas.status == 200) {
                        location.href = ""https://localhost:44339/BooklyAdmin/Order/Orders""
               ");
                WriteLiteral("     }\r\n                })\r\n            })\r\n\r\n        })\r\n    </script>\r\n");
            }
            );
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Order> Html { get; private set; }
    }
}
#pragma warning restore 1591
