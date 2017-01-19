// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace UserServiceClient.Models
{
    using System.Linq;

    /// <summary>
    /// 学科主任
    /// </summary>
    public partial class SubjectDirector
    {
        /// <summary>
        /// Initializes a new instance of the SubjectDirector class.
        /// </summary>
        public SubjectDirector() { }

        /// <summary>
        /// Initializes a new instance of the SubjectDirector class.
        /// </summary>
        /// <param name="subject">科目</param>
        /// <param name="grade">年级</param>
        public SubjectDirector(string subject = default(string), System.Collections.Generic.IList<int?> grade = default(System.Collections.Generic.IList<int?>))
        {
            Subject = subject;
            Grade = grade;
        }

        /// <summary>
        /// Gets or sets 科目
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets 年级
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "grade")]
        public System.Collections.Generic.IList<int?> Grade { get; set; }

    }
}
