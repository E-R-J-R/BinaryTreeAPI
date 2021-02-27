using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BladeportBinaryTreeManager.Database.Entities
{
    public partial class USERS
    {
        [Key]
        public int USERID { get; set; }
        
        [Column(TypeName="nvarchar(50)")]
        public string USERNAME { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string FIRSTNAME { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LASTNAME { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime JOINDATE { get; set; }
    }
}
