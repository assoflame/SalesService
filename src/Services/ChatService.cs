using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ChatService : IChatService
    {
        private ILoggerManager _logger;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ChatDto>> GetUserChatsAsync(int userId)
        {
            var chats = await _unitOfWork.Chats.GetUserChatsAsync(userId, trackChanges: false);

            var chatsDto = _mapper.Map<IEnumerable<ChatDto>>(chats);

            return chatsDto;
        }

        public async Task<ChatDto> GetUserChatAsync(int userId, int chatId)
        {
            var chat = await _unitOfWork.Chats.GetChatByIdAsync(chatId, trackChanges: false);

            if (chat is null || (chat.FirstUserId != userId && chat.SecondUserId != userId))
                throw new ChatNotFoundException(chatId);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatDto> SendMessageAsync(int userWhoSendsId, int userId, MessageCreationDto messageCreationDto)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId, trackChanges: false);

            if (user is null)
                throw new UserNotFoundException(userId);

            var chat = await _unitOfWork.Chats
                .GetChatByUsersAsync(userWhoSendsId, userId, trackChanges: false);

            if (chat is null)
            {
                chat = new Chat
                {
                    FirstUserId = Math.Min(userWhoSendsId, userId),
                    SecondUserId = Math.Max(userWhoSendsId, userId),
                    CreationDate = DateTime.UtcNow
                };

                _unitOfWork.Chats.Create(chat);
                await _unitOfWork.SaveAsync();
            }

            var message = _mapper.Map<Message>(messageCreationDto);
            message.ChatId = chat.Id;
            message.UserId = userWhoSendsId;
            message.CreationDate = DateTime.UtcNow;

            _unitOfWork.Messages.Create(message);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ChatDto>(chat);
        }
    }
}
