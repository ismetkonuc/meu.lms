using meu.lms.api.Models;
using meu.lms.business.Interfaces;
using meu.lms.dataaccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using meu.lms.api.Models.MessageModels;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Message = meu.lms.entities.Concrete.Message;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IAppUserDal _appUserDal;
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;

        public ChatController(IAppUserDal appUserDal, IMessageService messageService, UserManager<AppUser> userManager)
        {
            _appUserDal = appUserDal ?? throw new ArgumentNullException(nameof(appUserDal));
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        //private readonly IHubContext<MessageHub> _hub;

        //public ChatController(IHubContext<MessageHub> hub)
        //{
        //    _hub = hub;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index(Message msg)
        //{
        //    MessageHub hub = new MessageHub();


        //    await _hub.Clients.All.SendAsync("MessageReceived", msg);
        //    return Ok();
        //}

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetMessages(int targetUserId)
        {

            var clientUserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);


            var messages = _appUserDal.GetMessages(clientUserId, targetUserId);

            var rightSide = messages.Where(I => I.MessageTo == targetUserId).OrderBy(I => I.Id).AsEnumerable();
            var leftSide = messages.Where(I => I.MessageTo == clientUserId).OrderBy(I=>I.Id).AsEnumerable();


            MessageListModel messageListModel = new MessageListModel();

            foreach (var sended in rightSide)
            {
                   messageListModel.Messages.Add(new MessageModel(){Id = sended.Id, Content = sended.Content, SentTime = sended.SendedDate, Type = "Sended"});
            }

            foreach (var received in leftSide)
            {
                messageListModel.Messages.Add(new MessageModel() { Id = received.Id, Content = received.Content, SentTime = received.SendedDate, Type = "Received"});
            }


            return Ok(messageListModel.Messages.OrderBy(I=>I.Id).ToList());
        }

        [HttpGet("userList")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetMessageList()
        {
            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var appUser = _userManager.FindByIdAsync(appuserId.ToString()).Result;

            var roles = _userManager.GetRolesAsync(appUser).Result.ToList();
            //var userList = _appUserDal.GetUserList();

            var userList = new List<AppUser>();

            if (roles.Contains("Instructor"))
            {
                userList = _appUserDal.GetStundets();
            }
            else
            {
                userList = _appUserDal.GetInstructors();
            }


            userList.Remove(appUser);
            //var userList = _appUserDal.GetMessageList(appuserId);

            List<UserChat> chats = new List<UserChat>();
            foreach (var user in userList)
            {
                chats.Add(new UserChat(){AppUserId = user.Id, Name = user.Name, Surname = user.Surname});
            }

            //var list = messageList.Select(I => I.Id).ToList();

            return Ok(chats);
        }


        [HttpGet("lastMessage", Name = "GetUserLastMessage")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetUserLastMessage(int targetUserId)
        {
            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var userMessages = _appUserDal.GetMessages(appuserId, targetUserId);

            if (userMessages.Count == 0)
            {
                return Ok(0);
            }

            return Ok(userMessages.Last().Id);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult SendMessage(SendMessageModel message)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);
            var userList = _appUserDal.GetMessageList(appuserId);

            _messageService.Save(new Message()
            {
                MessageTo = message.MessageTo,
                AppUserId = appuserId,
                Content = message.Content
            });


            return Ok();

            //_messageDal.Save(message);

            //return Ok(_messageDal.Save(message));
        }

    }
}
