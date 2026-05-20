using RSR.DAL.Models.ThesisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.ThesisRepo
{
    public interface IThesisRepository
    {
        Task<Thesis> CreateThesis(Thesis thesis);
        Task<Thesis> UpdateThesis(Thesis thesis);
        Task<Thesis?> GetThesisById(Guid ThesisId);
        Task<Thesis?> GetThesisByGroupId(Guid GroupId);
    }
}
