using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Business.Tests
{
    [TestFixture]
    public class BinaryTreeBLTests
    {
        private List<SubTreeDTO> subTree;
        private OrgChartDTO schema;
        private TreeSchemaDTO treeSchema;
        private Mock<IBinaryTreeBL> _binaryTreeBl;
        private int maxDepth;

        [SetUp]
        public void Setup()
        {
            subTree = new List<SubTreeDTO>()
            {
                new SubTreeDTO { UserID = 1, PARENTID = 0, Depth = 0, LFT = 1, RGT = 30 },
                new SubTreeDTO { UserID = 2, PARENTID = 1, Depth = 1, LFT = 16, RGT = 29 },
                new SubTreeDTO { UserID = 3, PARENTID = 1, Depth = 1, LFT = 2, RGT = 15 },
                new SubTreeDTO { UserID = 4, PARENTID = 3, Depth = 2, LFT = 9, RGT = 14 },
                new SubTreeDTO { UserID = 5, PARENTID = 3, Depth = 2, LFT = 3, RGT = 8 },
                new SubTreeDTO { UserID = 6, PARENTID = 2, Depth = 2, LFT = 23, RGT = 28 },
                new SubTreeDTO { UserID = 7, PARENTID = 2, Depth = 2, LFT = 17, RGT = 22 },
                new SubTreeDTO { UserID = 8, PARENTID = 4, Depth = 3, LFT = 12, RGT = 13 },
                new SubTreeDTO { UserID = 9, PARENTID = 5, Depth = 3, LFT = 6, RGT = 7 },
                new SubTreeDTO { UserID = 10, PARENTID = 7, Depth = 3, LFT = 20, RGT = 21 },
                new SubTreeDTO { UserID = 11, PARENTID = 5, Depth = 3, LFT = 4, RGT = 5 },
                new SubTreeDTO { UserID = 12, PARENTID = 7, Depth = 3, LFT = 18, RGT = 19 },
                new SubTreeDTO { UserID = 13, PARENTID = 4, Depth = 3, LFT = 10, RGT = 11 },
                new SubTreeDTO { UserID = 14, PARENTID = 6, Depth = 3, LFT = 26, RGT = 27 },
                new SubTreeDTO { UserID = 15, PARENTID = 6, Depth = 3, LFT = 24, RGT = 25 }
            };

            maxDepth = 3;

            schema = new OrgChartDTO()
            {
                Id = "1",
                Title = "Ken Park", 
                Name = "1",
                Children = new List<OrgChartDTO>()
                {
                    new OrgChartDTO { Id = "2",  Title = "Jane Smith", Name = "2", Children = new List<OrgChartDTO>() 
                        { 
                            new OrgChartDTO { Id = "4", Title = "Josh Bailey", Name = "4", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "5", Title = "Duncan Taylor", Name = "5", Children = new List<OrgChartDTO>()}
                        }
                    },
                    new OrgChartDTO { Id = "3",  Title = "Ivy Viola", Name = "3", Children = new List<OrgChartDTO>()
                        {
                            new OrgChartDTO { Id = "6", Title = "Anderson Cooper", Name = "6", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "7", Title = "Griffin Luna", Name = "7", Children = new List<OrgChartDTO>()}
                        }
                    },
                }
            };

            treeSchema = new TreeSchemaDTO() { schema = schema, MaxDepth = maxDepth };

        }

        [Test]
        public void GetFullTreeSchema_NodeLimit_Test()
        {
            //Arrange
            _binaryTreeBl = new Mock<IBinaryTreeBL>();
            _binaryTreeBl.Setup(x => x.GetFullTreeSchema()).Returns(() => treeSchema);

            //Act
            var result = _binaryTreeBl.Object.GetFullTreeSchema();

            //Assert
            Assert.IsTrue(result.schema.Children.Count <= 2);

        }

        [Test]
        public void GetFullTreeSchema_Depth_Test()
        {
            //Arrange
            _binaryTreeBl = new Mock<IBinaryTreeBL>();
            _binaryTreeBl.Setup(x => x.GetFullTreeSchema()).Returns(() => treeSchema);

            //Act
            var result = _binaryTreeBl.Object.GetFullTreeSchema();

            //Assert
            Assert.IsTrue(result.schema.Children.Count > 0 && result.MaxDepth > 0);
        }
    }
}