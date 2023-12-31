﻿using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Services
{
    public class ChatService : IChatService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<ChatDto> chatsDto, MetaData metaData)> 
            GetUserChatsAsync(int userId, ChatParameters chatParams)
        {
            var chatsWithMetaData = await _unitOfWork.Chats
                .GetUserChatsAsync(userId, chatParams);

            var chatsDto = _mapper.Map<IEnumerable<ChatDto>>(chatsWithMetaData);

            return (chatsDto: chatsDto, metaData: chatsWithMetaData.MetaData);
        }

        public async Task<ChatDto> GetUserChatAsync(int userId, int chatId)
        {
            var chat = await _unitOfWork.Chats.GetChatByIdAsync(chatId);

            if (chat is null || (chat.FirstUserId != userId && chat.SecondUserId != userId))
                throw new ChatNotFoundException(chatId);

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<MessageDto> SendMessageAsync(int userWhoSendsId, int userId, MessageCreationDto messageCreationDto)
        {
            var user = await _unitOfWork.Users.GetUserByIdAsync(userId);

            if (user is null)
                throw new UserNotFoundException(userId);

            var chat = await _unitOfWork.Chats
                .GetChatByUsersAsync(userWhoSendsId, userId);

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

            return _mapper.Map<MessageDto>(message);
        }
    }
}
