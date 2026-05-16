using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisVersionsRepo
{
    public  interface IThesisVersionsRepository
    {
        Task AddThesisVersion(ThesisVersions thesis);
        Task<ThesisVersions?> GetLastVersion(Guid ThesisId);
        Task UpdateThesisVersion(ThesisVersions version);
        Task<bool> HasFrozenThesis(Guid ThesisId);
        Task<ThesisVersions?> GetVersionById(Guid VersionId);
        Task<ThesisVersions?> GetVersionByIdWithSupervisor(Guid VersionId);
        Task<List<ThesisVersions>> GetPublishedThesis();
    }

}
