namespace BladeportBinaryTreeManager.Database.Entities
{
    public partial class BINARYTREE
    {
        public int USERID { get; set; }
        public int LFT { get; set; }
        public int RGT { get; set; }
        public int PARENTID { get; set; }
        public int? SPONSORID { get; set; }
        public bool ISMASTERPARENT { get; set; }
    }
}
