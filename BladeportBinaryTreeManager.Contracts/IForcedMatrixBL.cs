using BladeportBinaryTreeManager.DTO;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Contracts
{
    public interface IForcedMatrixBL
    {
        ParentNodeDTO GetLogicalParentNode(List<SubTreeDTO> subTreeList, int childLimit, int levelLimit);
        void InsertNode(ForcedMatrixNodeDTO node, ParentNodeDTO parentNode);
        void InsertMasterParentNode(ForcedMatrixNodeDTO node);
        TreeSchemaDTO GetFullTreeSchema(int childLimit, int levelLimit);
    }
}
