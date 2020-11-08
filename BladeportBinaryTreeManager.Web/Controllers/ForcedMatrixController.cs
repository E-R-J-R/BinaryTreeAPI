using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using BladeportBinaryTreeManager.DTO.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BladeportBinaryTreeManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForcedMatrixController : ControllerBase
    {
        private readonly IForcedMatrixBL _forcedMatrixBL;
        private readonly IHierarchyBL _hierarchyBL;

        public ForcedMatrixController(IForcedMatrixBL forcedMatrixBL, IHierarchyBL hierarchyBL)
        {
            _forcedMatrixBL = forcedMatrixBL;
            _hierarchyBL = hierarchyBL;
        }


        [Route("treeschema")]
        [HttpGet]
        public TreeSchemaDTO GetFullTreeSchema(int childLimit, int levelLimit)
        {
            return _forcedMatrixBL.GetFullTreeSchema(childLimit, levelLimit);
        }

        [HttpPost("insert")]
        public void InsertNode([FromBody] ForcedMatrixNodeDTO node)
        {
            var parentNode = new ParentNodeDTO();

            try
            {
                //Fetch the subtree of the Sponsor
                //A sponsor cannot be the direct parent of the new user when max number of children has been reached
                var subTreeList = _hierarchyBL.GetSubTreeDepth(node.SponsorId, HierarchyType.ForcedMatrix, node.ChildLimit, node.LevelLimit);

                if (subTreeList.Count != 0)
                {
                    //Traverse the tree and search for the appropriate parent node
                    parentNode = _forcedMatrixBL.GetLogicalParentNode(subTreeList, node.ChildLimit, node.LevelLimit);
                }
                else
                {
                    //Master Parent Node Insert
                    _forcedMatrixBL.InsertMasterParentNode(node);
                    return;
                }

                //Insert Node
                _forcedMatrixBL.InsertNode(node, parentNode);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
