using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using YinzHere.Services;

namespace YinzHere.Pages
{
    public class ReadyCheckBase : ComponentBase
    {
        [ParameterAttribute]
        public string Key { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IReadyCheckService ReadyCheckService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] ClipboardService ClipboardService { get; set; }

        protected string nameToAdd { get; set; }
        protected Models.ReadyCheck CurrentReadyCheck { get; set; }

        public void Dispose()
        {
            if (this.CurrentReadyCheck == null)
            {
                this.CurrentReadyCheck.OnChange -= StateHasChanged;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            base.OnParametersSet();
            this.CurrentReadyCheck = ReadyCheckService.GetReadyCheck(this.Key);
            if (CurrentReadyCheck == null)
            {
                if (!string.IsNullOrWhiteSpace(this.Key))
                {
                    this.Key = null;
                    NavigationManager.NavigateTo("/", true);
                }
            }
            else
            {
                this.CurrentReadyCheck.OnChange += StateHasChangedAsync;
            }
        }

        protected void StateHasChangedAsync()
        {
            InvokeAsync(StateHasChanged);
        }

        protected void AddName()
        {
            this.CurrentReadyCheck.AddUser(nameToAdd);
            this.nameToAdd = null;
        }

        protected void NewReadyCheck()
        {
            var key = ReadyCheckService.NewReadyCheck();
            NavigationManager.NavigateTo($"/readycheck/{key}");
        }

        protected void CopyUrl()
        {
            ClipboardService.WriteTextAsync(NavigationManager.Uri);
        }
    }
}
