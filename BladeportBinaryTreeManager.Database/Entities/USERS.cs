using System;

namespace BladeportBinaryTreeManager.Database.Entities
{
    public partial class USERS
    {
        public int USERID { get; set; }
        public string USERNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public DateTime JOINDATE { get; set; }
    }
}
