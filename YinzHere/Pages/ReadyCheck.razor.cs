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

        protected ElementReference addNameInput { get; set; }
        
        protected string _nameToAdd { get; set; }
        protected Models.ReadyCheck _currentReadyCheck { get; set; }

        public void Dispose()
        {
            if (_currentReadyCheck == null)
            {
                _currentReadyCheck.OnChange -= StateHasChanged;
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _currentReadyCheck = ReadyCheckService.GetReadyCheck(Key);
            if (_currentReadyCheck == null)
            {
                if (!string.IsNullOrWhiteSpace(Key))
                {
                    Key = null;
                    NavigationManager.NavigateTo("/", true);
                }
            }
            else
            {
                _currentReadyCheck.OnChange += StateHasChangedAsync;
            }
        }

        protected void StateHasChangedAsync()
        {
            InvokeAsync(StateHasChanged);
        }

        protected async Task AddNameAsync()
        {
            if(string.IsNullOrWhiteSpace(_nameToAdd))
            {
                await addNameInput.FocusAsync();
                return;
            }
            _currentReadyCheck.AddUser(_nameToAdd);
            _nameToAdd = null;
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
