namespace BladeportBinaryTreeManager.Database.Entities
{
    public partial class SUBTREE
    {
        public int USERID { get; set; }
        public int DEPTH { get; set; }
        public int LFT { get; set; }
        public int RGT { get; set; }
        public int PARENTID { get; set; }
    }
}
