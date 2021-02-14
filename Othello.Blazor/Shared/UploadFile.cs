using System;

namespace Othello.Blazor.Shared
{
    public class UploadFile
    {
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public byte[] Content { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }
}
