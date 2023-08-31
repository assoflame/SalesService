using AutoMapper;
using DataAccess.Interfaces;
using Entities.Exceptions;
using SalesService.Entities.Models;
using Services.Interfaces;
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
        
        //public async Task<ChatDto> SendMessage(int userId, MessageForCreationDto messageDto)
        //{
        //    var seller = await _unitOfWork.Users.GetUserByIdAsync(messageDto.UserId);

        //    if (seller is null)
        //        throw new UserNotFoundException(messageDto.UserId);

        //    var chat = await _unitOfWork.Chats
        //        .GetChatByUsersAsync(userId, messageDto.UserId, trackChanges: false);

        //    if (chat is null)
        //    {
        //        chat = new Chat
        //        {
        //            CustomerId = userId,
        //            SellerId = messageDto.UserId,
        //            CreationDate = DateTime.UtcNow
        //        };
        //        _unitOfWork.Chats.Create(chat);
        //        await _unitOfWork.SaveAsync();

        //        chat = await _unitOfWork.Chats
        //            .GetChatByUsersAsync(userId, messageDto.UserId, trackChanges: false);
        //    }

        //    var message = _mapper.Map<Message>(messageDto);
        //    message.ChatId = chat.Id;
        //    message.UserId = userId;
        //    message.CreationDate = DateTime.UtcNow;

        //    _unitOfWork.Messages.Create(message);
        //    await _unitOfWork.SaveAsync();

        //}
    }
}
