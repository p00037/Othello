using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Othello.Blazor.Shared;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Othello.Blazor.Client.Pages
{
    public partial class AiFileUpload : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        private UploadFile uploadFile = new UploadFile();
        private bool isLoading;
        private string errorMessage;
        private string saveMassage;

        async Task LoadFiles(InputFileChangeEventArgs e)
        {
            this.isLoading = true;
            this.errorMessage = string.Empty;
            this.saveMassage = string.Empty;

            try
            {
                foreach (var file in e.GetMultipleFiles(1))
                {
                    StateHasChanged();
                    var buffers = new byte[file.Size];
                    await file.OpenReadStream().ReadAsync(buffers);
                    this.uploadFile.FileName = file.Name;
                    this.uploadFile.ContentType = file.ContentType;
                    this.uploadFile.Size = file.Size;
                    this.uploadFile.LastModified = file.LastModified;
                    this.uploadFile.Content = buffers;
                }
            }
            catch (Exception ex)
            {
                this.errorMessage = ex.Message;
            }
            finally
            {
                this.isLoading = false;
            }
        }

        public async Task ButtonUploadClickAsync()
        {
            await Http.PostAsJsonAsync("AiFileUpload", uploadFile);
            this.saveMassage = "登録完了";
        }
    }
}
