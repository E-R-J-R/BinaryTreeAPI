using BladeportBinaryTreeManager.Contracts;
using BladeportBinaryTreeManager.DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BladeportBinaryTreeManager.Business.Tests
{
    [TestFixture]
    public class ForcedMatrixBLTests
    {
        private List<SubTreeDTO> subTree;
        private OrgChartDTO schema;
        private TreeSchemaDTO treeSchema;
        private Mock<IForcedMatrixBL> _forcedMatrixBl;
        private int maxDepth;
        private int childLimit;
        private int levelLimit;

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
            childLimit = 3;
            levelLimit = 3;

            schema = new OrgChartDTO()
            {
                Id = "1",
                Title = "Ken Park",
                Name = "1",
                Children = new List<OrgChartDTO>()
                {
                    new OrgChartDTO { Id = "2",  Title = "Jane Smith", Name = "2", Children = new List<OrgChartDTO>()
                        {
                            new OrgChartDTO { Id = "5", Title = "Duncan Taylor", Name = "5", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "6", Title = "Anderson Cooper", Name = "6", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "7", Title = "Griffin Luna", Name = "7", Children = new List<OrgChartDTO>()},
                        }
                    },
                    new OrgChartDTO { Id = "3",  Title = "Ivy Viola", Name = "3", Children = new List<OrgChartDTO>()
                        {                           
                            new OrgChartDTO { Id = "8", Title = "Finley Gray", Name = "8", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "9", Title = "Jenny West", Name = "9", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "10", Title = "Shaw Avery", Name = "10", Children = new List<OrgChartDTO>()},
                        }
                    },
                    new OrgChartDTO { Id = "4", Title = "Josh Bailey", Name = "4", Children = new List<OrgChartDTO>()
                        {
                            new OrgChartDTO { Id = "11", Title = "Jolly Bee", Name = "11", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "12", Title = "Misha Ken", Name = "12", Children = new List<OrgChartDTO>()},
                            new OrgChartDTO { Id = "13", Title = "Henry Ford", Name = "13", Children = new List<OrgChartDTO>()}
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
            _forcedMatrixBl = new Mock<IForcedMatrixBL>();
            _forcedMatrixBl.Setup(x => x.GetFullTreeSchema(It.IsAny<int>(), It.IsAny<int>())).Returns(() => treeSchema);

            //Act
            var result = _forcedMatrixBl.Object.GetFullTreeSchema(childLimit, levelLimit);

            //Assert
            Assert.IsTrue(result.schema.Children.Count <= 3);

        }

        [Test]
        public void GetFullTreeSchema_Depth_Test()
        {
            //Arrange
            _forcedMatrixBl = new Mock<IForcedMatrixBL>();
            _forcedMatrixBl.Setup(x => x.GetFullTreeSchema(It.IsAny<int>(), It.IsAny<int>())).Returns(() => treeSchema);

            //Act
            var result = _forcedMatrixBl.Object.GetFullTreeSchema(childLimit, levelLimit);

            //Assert
            Assert.IsTrue(result.schema.Children.Count > 0 && result.MaxDepth > 0);
        }

    }
}
