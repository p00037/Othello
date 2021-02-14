using Othello.Blazor.Server.Shared;
using Othello.Blazor.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Blazor.Server.Repository
{
    public class AiInfoRepository
    {
        public List<AiInfo> GetEntitys()
        {
            return JsonFileIO.ReadAllLine<List<AiInfo>>(AppPath.GetAiInfosFilePath(), Encoding.UTF8);
        }

        public void Save(AiInfo saveEntity)
        {
            var entitys = GetEntitys();
            var entity = entitys.Where(m => m.FileName == saveEntity.FileName).FirstOrDefault();
            if(entity != null)
            {
                entity.DisplayName = saveEntity.DisplayName;
            }
            else
            {
                entitys.Add(saveEntity);
            }

            JsonFileIO.WriteFile(AppPath.GetAiInfosFilePath(), Encoding.UTF8, entitys);
        }
    }
}
