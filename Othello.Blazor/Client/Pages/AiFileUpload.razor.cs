using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Othello.Blazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Othello.Blazor.Client.Pages
{
    public partial class AiFileUpload : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        List<UploadFile> loadedFiles = new List<UploadFile>();
        bool isLoading;
        string errorMessage;

        async Task LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();
            errorMessage = string.Empty;

            try
            {
                foreach (var file in e.GetMultipleFiles(3))
                {
                    StateHasChanged();
                    var buffers = new byte[file.Size];
                    await file.OpenReadStream().ReadAsync(buffers);
                    var uploadFile = new UploadFile()
                    {
                        FileName = file.Name,
                        ContentType = file.ContentType,
                        Size = file.Size,
                        LastModified = file.LastModified,
                        Content = buffers
                    };

                    loadedFiles.Add(uploadFile);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }

        public async Task ButtonUploadClickAsync()
        {
            await Http.PostAsJsonAsync("AiFileUpload", loadedFiles);
        }
    }
}
