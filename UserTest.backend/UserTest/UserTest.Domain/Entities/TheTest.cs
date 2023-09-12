using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserTest.Domain.Entities
{
    public class TheTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TestName { get; set; } = string.Empty;

        public List<Question>? Questions { get; set; }

        public List<User>? AssignedUsers { get; set; }
    }
}
