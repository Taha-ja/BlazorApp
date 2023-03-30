using Microsoft.JSInterop;

namespace BlazorApp.Data.GeoLocation
{
    public class GeoLocation
    {
        private readonly IJSRuntime js;
        private Action<Position> OnGetPosition;
        public GeoLocation(IJSRuntime js)
        {
            this.js = js;
        }
        public async ValueTask<bool> HasGeolocationFeature() => await js.InvokeAsync<bool>("blazorGeolocation.hasGeolocaitonFeature");

        [JSInvokable]
        public void RaiseOnGetPosition(Position p) => OnGetPosition?.Invoke(p);

        private Action<PositionError> OnGetPositionError;
        [JSInvokable]
        public void RaiseOnGetPositionError(PositionError err) => OnGetPositionError?.Invoke(err);
        public async ValueTask GetCurrentPosition(Action<Position> onSuccess,
                                                  Action<PositionError> onError,
                                                  PositionOptions options = null)
        {
            OnGetPosition = onSuccess;
            OnGetPositionError = onError;
            await js.InvokeVoidAsync("blazorGeolocation.getCurrentPosition",
                                     DotNetObjectReference.Create(this), options);
        }
    }
}
