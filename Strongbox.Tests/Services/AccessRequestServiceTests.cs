using AutoMapper;
using Moq;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Application.Services;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Tests.Services
{
    public class AccessRequestServiceTests
    {
        private readonly Mock<IAccessRequestRepository> _reqRepoMock;
        private readonly Mock<IDocumentRepository> _docRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IAccessRequestService _service;

        public AccessRequestServiceTests()
        {
            _reqRepoMock = new Mock<IAccessRequestRepository>();
            _docRepoMock = new Mock<IDocumentRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new AccessRequestService(
                _reqRepoMock.Object,
                _docRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CreateAccessRequestAsync_DocumentNotFound_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = new AccessRequestDto
            {
                DocumentId = Guid.NewGuid(),
                Reason = "Test reason",
                Type = AccessType.Read
            };
            _docRepoMock
                .Setup(r => r.GetDocumentAsync(dto.DocumentId))
                .ReturnsAsync((Document?)null);

            // Act
            var result = await _service.CreateAccessRequestAsync(userId, dto);

            // Assert
            Assert.Null(result);
            _reqRepoMock.Verify(r => r.CreateAccessRequestAsync(It.IsAny<AccessRequest>()), Times.Never);
        }

        [Fact]
        public async Task CreateAccessRequestAsync_DocumentExists_CreatesAndReturnsId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var doc = new Document { Id = Guid.NewGuid(), Name = "Doc1", Content = "..." };
            var dto = new AccessRequestDto
            {
                DocumentId = doc.Id,
                Reason = "Need access",
                Type = AccessType.Edit
            };
            _docRepoMock
                .Setup(r => r.GetDocumentAsync(doc.Id))
                .ReturnsAsync(doc);

            var mappedRequest = new AccessRequest
            {
                DocumentId = dto.DocumentId,
                Reason = dto.Reason,
                Type = dto.Type,
                UserId = Guid.Empty,
                Status = RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            _mapperMock
                .Setup(m => m.Map<AccessRequest>(dto))
                .Returns(mappedRequest);

            var expectedId = Guid.NewGuid();
            _reqRepoMock
                .Setup(r => r.CreateAccessRequestAsync(It.IsAny<AccessRequest>()))
                .ReturnsAsync(expectedId)
                .Callback<AccessRequest>(req =>
                {
                    Assert.Equal(userId, req.UserId);
                    Assert.Equal(dto.DocumentId, req.DocumentId);
                    Assert.Equal(dto.Reason, req.Reason);
                    Assert.Equal(dto.Type, req.Type);
                    Assert.Equal(RequestStatus.Pending, req.Status);
                    Assert.True((DateTime.UtcNow - req.CreatedAt).TotalSeconds < 5);
                });

            // Act
            var result = await _service.CreateAccessRequestAsync(userId, dto);

            // Assert
            Assert.Equal(expectedId, result);
            _reqRepoMock.Verify(r => r.CreateAccessRequestAsync(It.IsAny<AccessRequest>()), Times.Once);
        }

        [Fact]
        public async Task GetMyRequestsAsync_ReturnsMappedDtos()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var domainList = new List<AccessRequest>
            {
                new AccessRequest
                {
                    Id          = Guid.NewGuid(),
                    UserId      = userId,
                    DocumentId  = Guid.NewGuid(),
                    Reason      = "Reason",
                    Type        = AccessType.Read,
                    Status      = RequestStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                }
            };
            var dtoList = new List<AccessRequestResultDto>
            {
                new AccessRequestResultDto
                {
                    AccessRequestId = domainList[0].Id,
                    UserId          = userId,
                    UserName        = "User",
                    DocumentId      = domainList[0].DocumentId,
                    DocumentName    = "Doc1",
                    Reason          = "Reason",
                    Type            = AccessType.Read,
                    Status          = RequestStatus.Pending
                }
            };

            _reqRepoMock
                .Setup(r => r.GetAccessRequestsByUserAsync(userId))
                .ReturnsAsync(domainList);
            _mapperMock
                .Setup(m => m.Map<List<AccessRequestResultDto>>(domainList))
                .Returns(dtoList);

            // Act
            var result = await _service.GetMyRequestsAsync(userId);

            // Assert
            Assert.Equal(dtoList, result);
        }

        [Fact]
        public async Task GetPendingRequestsAsync_ReturnsMappedDtos()
        {
            // Arrange
            var domainList = new List<AccessRequest>
            {
                new AccessRequest
                {
                    Id          = Guid.NewGuid(),
                    UserId      = Guid.NewGuid(),
                    DocumentId  = Guid.NewGuid(),
                    Reason      = "Reason",
                    Type        = AccessType.Edit,
                    Status      = RequestStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                }
            };
            var dtoList = new List<AccessRequestResultDto>
            {
                new AccessRequestResultDto
                {
                    AccessRequestId = domainList[0].Id,
                    UserId          = domainList[0].UserId,
                    UserName        = "Approver",
                    DocumentId      = domainList[0].DocumentId,
                    DocumentName    = "Doc2",
                    Reason          = "Reason",
                    Type            = AccessType.Edit,
                    Status          = RequestStatus.Pending
                }
            };

            _reqRepoMock
                .Setup(r => r.GetPendingAccessRequestsAsync())
                .ReturnsAsync(domainList);
            _mapperMock
                .Setup(m => m.Map<List<AccessRequestResultDto>>(domainList))
                .Returns(dtoList);

            // Act
            var result = await _service.GetPendingRequestsAsync();

            // Assert
            Assert.Equal(dtoList, result);
        }
    }
}

