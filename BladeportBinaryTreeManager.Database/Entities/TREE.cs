namespace BladeportBinaryTreeManager.Database.Entities
{
    public class TREE
    {
        public int USERID { get; set; }
        public string USERNAME { get; set; }
        public string FULLNAME { get; set; }
        public int PARENTID { get; set; }
        public int? SPONSORID { get; set; }
        public int DEPTH { get; set; }
    }
}
