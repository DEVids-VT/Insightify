using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Insightify.SPA.Shared
{
    public partial class LoginDisplay
    {

        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        private bool isDropdownVisible = false;

        public void OpenCloseDropdown()
        {
            isDropdownVisible = !isDropdownVisible;
        }
        
        private ElementReference header;
        private string headerClass = "";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dotNetRef = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("addScrollListener", dotNetRef);
            }
        }

        [JSInvokable]
        public void MakeHeaderSticky()
        {
            headerClass = "login-sticky";
            StateHasChanged();
        }

        [JSInvokable]
        public void MakeHeaderUnSticky()
        {
            headerClass = "";
            StateHasChanged();
        }

        public void Dispose()
        {
            JSRuntime.InvokeVoidAsync("removeScrollListener");
        }
    }
}

