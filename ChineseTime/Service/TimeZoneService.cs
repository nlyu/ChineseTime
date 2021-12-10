using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ChineseTime.Service
{
    public sealed class TimeZoneService
    {
        private readonly IJSRuntime _jsRuntime;

        private TimeSpan? _userOffset;

        public TimeZoneService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async ValueTask<DateTimeOffset> GetLocalDateTime(DateTimeOffset dateTime)
        {
            if (_userOffset == null)
            {
                int offsetInMinutes = await _jsRuntime.InvokeAsync<int>("blazorGetTimezoneOffset");
                _userOffset = TimeSpan.FromMinutes(-offsetInMinutes);
            }

            // TODO: Require server render mode while instantiating the component to execute JavaScript in OnInitializedAsync.
            // In _Host.cshtml: <component type="typeof(App)" render-mode="Server" />
            return dateTime.ToOffset(_userOffset.Value);
        }
    }
}