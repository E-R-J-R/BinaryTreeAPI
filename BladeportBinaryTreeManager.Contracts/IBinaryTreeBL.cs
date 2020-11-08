using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IBinaryTreeBL
    {
        List<TreeStructureDTO> GetBinaryTreeData();
        ParentNodeDTO GetLogicalParentNode(List<SubTreeDTO> subTreeList);
        void InsertNode(NodeDTO node, ParentNodeDTO parentNode);
        void InsertMasterParentNode(NodeDTO node);
        TreeSchemaDTO GetFullTreeSchema();
    }
}
