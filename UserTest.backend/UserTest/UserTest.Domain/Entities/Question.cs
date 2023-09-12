using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTest.Domain.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }  = string.Empty;
        public TheTest? TheTest { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
