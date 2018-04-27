using DarkSide;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepApp.Models;

namespace WepApp.Controllers.Abstract
{
    public class BaseApiController : ApiController
    {
        protected IAuthenticationManager AuthManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }
        protected UserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<UserManager>();
            }
        }
        private List<ApiResponseWrap.Message> ResponseMessages = new List<ApiResponseWrap.Message>();


        public IHttpActionResult WrapResponse(object data, ApiResponseWrap.ResponseState state)
        {
            return WrapResponse(data, state, new ApiResponseWrap.Message[0]);
        }

        public IHttpActionResult WrapResponse(object data, ApiResponseWrap.ResponseState state, ApiResponseWrap.Message message)
        {
            return WrapResponse(data, state, message.IfNotNull(a => new ApiResponseWrap.Message[] { message }).IfNull(new ApiResponseWrap.Message[0]));
        }

        public IHttpActionResult WrapResponse(object data, ApiResponseWrap.ResponseState state, IEnumerable<ApiResponseWrap.Message> messages)
        {
            messages.Do(a => ResponseMessages.AddRange(a));
            return Json(new ApiResponseWrap(state, data, ResponseMessages));
        }

        public void SetResponseMessage(ApiResponseWrap.MessageType type, string body)
        {
            ResponseMessages.Add(new ApiResponseWrap.Message(type, body));
        }
        public IHttpActionResult WrapError(string message)
        {
            SetResponseMessage(ApiResponseWrap.MessageType.Error, message);
            return WrapResponse(null, ApiResponseWrap.ResponseState.Error);
        }

        public IHttpActionResult WrapSuccess(object data = null)
        {
            return WrapResponse(data, ApiResponseWrap.ResponseState.Success);
        }
    }
}
