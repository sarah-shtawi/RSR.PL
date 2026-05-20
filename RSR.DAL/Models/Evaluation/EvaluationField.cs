using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSR.DAL.Models.Evaluation
{
    public class EvaluationField
    {
        public int Id { get; set; }

        // اسم معيار التقييم
        public string FieldName { get; set; } = string.Empty;

        // أقل علامة
        public int MinValue { get; set; }

        // أعلى علامة
        public int MaxValue { get; set; }

  
        // هل الحقل مطلوب؟
        public bool IsRequired { get; set; }

        // Foreign Key
        public int EvaluationFormId { get; set; }

        // Navigation Property
        public EvaluationForm EvaluationForm { get; set; }
    }
}