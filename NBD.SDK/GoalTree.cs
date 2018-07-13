using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBD.SDK
{
    public class GoalTree
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(256)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string PublicHash { get; set; }

        [Required]
        public string PrivateHash { get; set; }

        [ForeignKey("Root")]
        public Guid RootId { get; set; }

        public virtual Goal Root { get; set; }
    }
}
