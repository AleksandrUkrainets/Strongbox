using AutoMapper;
using Strongbox.Application.DTOs;
using Strongbox.Application.Interfaces;
using Strongbox.Domain.Entities;
using Strongbox.Domain.Interfaces;

namespace Strongbox.Application.Services
{
    public class DocumentService(IDocumentRepository documentRepository, IUserRepository userRepository, IMapper mapper) : IDocumentService
    {
        public async Task<DocumentResultDto?> GetDocumentAsync(Guid documentId, Guid userId)
        {
            var user = await userRepository.GetUserAsync(userId);
            if (user == null) return null;

            var document = await documentRepository.GetDocumentAsync(documentId);
            if (document == null) return null;

            var hasViewAccess = user.Role != PersonRole.User
                || document.AccessRequests.Any(a => a.UserId == userId && a.Status == RequestStatus.Approved);
            if (!hasViewAccess) return null;

            return mapper.Map<DocumentResultDto>(document);
        }

        //public async Task<ICollection<DocumentResultDto>> GetDocumentsAsync(Guid userId)
        //{
        //    var user = await userRepository.GetUserAsync(userId);
        //    if (user == null) return [];

        //    var documents = await documentRepository.GetDocumentsAsync();

        //    return mapper.Map<ICollection<DocumentResultDto>>(documents);
        //}

        public async Task<ICollection<DocumentResultDto>> GetDocumentsAsync(Guid userId)
        {
            var user = await userRepository.GetUserAsync(userId);
            if (user == null) return [];

            var documents = await documentRepository.GetDocumentsAsync();

            var result = documents.Select(doc =>
            {
                var userAccess = doc.AccessRequests
                    .Where(a => a.UserId == userId && a.Status == RequestStatus.Approved)
                    .OrderByDescending(a => a.Type) // Edit > Read > Blocked
                    .Select(a => a.Type)
                    .FirstOrDefault();

                return new DocumentResultDto
                {
                    DocumentId = doc.Id,
                    DocumentName = doc.Name,
                    DocumentContent = doc.Content,
                    Access = userAccess
                };
            }).ToList();

            return result;
        }


        public async Task<ICollection<DocumentAttributesResultDto>> GetDocumentsAttributesAsync(Guid userId)
        {
            var user = await userRepository.GetUserAsync(userId);
            if (user == null) return [];

            var documents = await documentRepository.GetDocumentsAsync();

            return mapper.Map<ICollection<DocumentAttributesResultDto>>(documents);
        }

        public async Task<bool> UpdateDocumentAsync(DocumentDto documentDto)
        {
            var user = await userRepository.GetUserAsync(documentDto.UserId);
            if (user == null) return false;

            var document = await documentRepository.GetDocumentAsync(documentDto.DocumentId);
            if (document == null) return false;

            var hasEditAccess = user.Role != PersonRole.User ||
                document.AccessRequests.Any(a => a.UserId == documentDto.UserId && a.Type == AccessType.Edit && a.Status == RequestStatus.Approved);

            if (!hasEditAccess) return false;

            document.Content = documentDto.DocumentContent;
            return await documentRepository.UpdateDocumentAsync(document);
        }
    }
}