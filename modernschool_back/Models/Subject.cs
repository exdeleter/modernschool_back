using System.ComponentModel.DataAnnotations;

namespace modernschool_back.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; }
    }
}
