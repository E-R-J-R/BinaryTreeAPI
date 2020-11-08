using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BladeportBinaryTreeManager.DTO.Enums;
using System;

namespace BladeportBinaryTreeManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinaryTreeController : ControllerBase
    {
        private readonly IBinaryTreeBL _binaryTreeBl;
        private readonly IHierarchyBL _hierarchyBL;

        public BinaryTreeController(IBinaryTreeBL binaryTreeBL, IHierarchyBL hierarchyBL)   
        {
            _binaryTreeBl = binaryTreeBL;
            _hierarchyBL = hierarchyBL;
        }

        [Route("hierarchy")]
        [HttpGet]
        public List<TreeStructureDTO> GetHierarchy()
        {
            return _binaryTreeBl.GetBinaryTreeData();
        }

        [Route("subtree")]
        [HttpGet]
        public List<SubTreeDTO> GetSubTreeDepth(int parentId)
        {
            return _hierarchyBL.GetSubTreeDepth(parentId, HierarchyType.BinaryTree);
        }

        [Route("treeschema")]
        [HttpGet]
        public TreeSchemaDTO GetFullTreeSchema()
        {
            return _binaryTreeBl.GetFullTreeSchema();
        }

        [HttpPost("insert")]
        public void InsertNode([FromBody]NodeDTO node)        
        {
            var parentNode = new ParentNodeDTO();

            try
            {
                //Fetch the subtree of the Sponsor
                //A sponsor cannot be the direct parent of the new user when max number of children has been reached
                var subTreeList = _hierarchyBL.GetSubTreeDepth(node.SponsorId, HierarchyType.BinaryTree);

                if (subTreeList.Count > 1)
                {
                    //Traverse the tree and search for the appropriate parent node
                    parentNode = _binaryTreeBl.GetLogicalParentNode(subTreeList);
                }
                else if (subTreeList.Count == 1)
                {
                    parentNode.NodeId = subTreeList.First().UserID;
                }
                else
                {
                    //Master Parent Node Insert
                    _binaryTreeBl.InsertMasterParentNode(node);
                    return;
                }

                //Insert Node
                _binaryTreeBl.InsertNode(node, parentNode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
