using RSR.DAL.Models.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Repository.EvaluationRepository
{
    public interface IEvaluationFieldRepository
    {
        Task<EvaluationField> CreateAsync(EvaluationField field);

        Task<EvaluationField?> GetByIdAsync(int id);
        Task<EvaluationField?> UpdateAsync(EvaluationField field);

        Task<bool> DeleteAsync(int id);
    }
}