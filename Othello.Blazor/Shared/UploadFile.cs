using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Blazor.Shared
{
    public class UploadFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }
}
