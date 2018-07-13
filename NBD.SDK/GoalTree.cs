using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBD.SDK
{
    public class GoalTree
    {
        public GoalTree()
        {
            this.Id = Guid.NewGuid();
            this.PrivateId = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PrivateId { get; set; }

        [StringLength(512)]
        [Required]
        public string Title { get; set; }

        [ForeignKey("Root")]
        public Guid RootId { get; set; }

        public virtual Goal Root { get; set; }
    }
}
