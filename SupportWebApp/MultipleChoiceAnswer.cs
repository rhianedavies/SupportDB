//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupportWebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class MultipleChoiceAnswer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MultipleChoiceAnswer()
        {
            this.Answers = new HashSet<Answer>();
        }
    
        public System.Guid MultipleChoiceAnswersId { get; set; }
        public Nullable<System.Guid> QuestionId { get; set; }
        public string Answer { get; set; }
        public Nullable<int> AnswerNo { get; set; }
        public Nullable<System.Guid> QuestionnaireId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual question question { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
    }
}
