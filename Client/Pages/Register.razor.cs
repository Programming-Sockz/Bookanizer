using Blazored.LocalStorage;
using Bookanizer.Shared.DTO;
using Bookanizer.Shared.Libraries;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bookanizer.Client.Pages
{
    public partial class Register
    {
        [Inject] public HttpClient Http { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        private string loginStorageKey = "loginStamp";
        private RegisterDTO register = new();
        private LoginResponseDTO? responseDTO;
        //simple method to stop spamming the loggin method
        private bool isSubmitting = false;

        private async Task ValidSubmit()
        {
            if (!isSubmitting)
            {
                isSubmitting = true;
                var response = await Http.PostAsJsonAsync(ApiRoutes.User.POST_Register(), register);
                if (response.IsSuccessStatusCode)
                {
                    responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                    if (responseDTO.Success)
                    {
                        await LocalStorageService.SetItemAsync(loginStorageKey, responseDTO);
                        NavigationManager.NavigateTo("");
                    }
                    else
                    {
                        isSubmitting = false;
                    }
                }
            }
        }
    }
}